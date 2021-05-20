using Newtonsoft.Json;
using Steam_ACC_Create;
using SteamAccountCreateHelper.Models;
using SteamAccountCreateSelenium.Models;
using SteamAccountCreateSelenium.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SteamAccountCreateSelenium.ManageEmails;

namespace SteamAccountCreateSelenium
{
    public partial class Main : Form
    {
        public static Main _Form1;

        public static string Database_Path = AppDomain.CurrentDomain.BaseDirectory + "Database\\";
        public static string Used_Mail_DB_Path = Database_Path + "Used_Email_DB.json";
        public static string creationid_DB_READ = Database_Path + "CreationID_DB.json";
        public static string Acc_Create_Path = Database_Path + "Created_Accounts\\";

        public static Pais paises = JsonConvert.DeserializeObject<Pais>(File.ReadAllText(Database_Path + "\\Steam_Country.json"));
        public static string[] Names = File.ReadAllLines(Database_Path + "Names_DataBase.txt");
        public static List<string> Avatar_URL_List = new List<string>();

        public static int Max_Acc_Por_Email = 10;
        public static List<E_Mail> EMAIl_LIST = new List<E_Mail>();

        public string AvatarImageFilePath = "";
        public string EmailFilePath = "";

        public static string Login = "";
        public static string Pass = "";
        public static E_Mail email = null;

        public Main()
        {
            _Form1 = this; 
            InitializeComponent();
        }

        private void btn_Open_Email_File_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog theDialog = new OpenFileDialog();
                theDialog.Title = "Open Text File";
                theDialog.Filter = "TXT files|*.txt";
                theDialog.InitialDirectory = Application.StartupPath;
                if (theDialog.ShowDialog() == DialogResult.OK)
                {
                    EmailFilePath = theDialog.FileName;

                    EMAIl_LIST.Clear();
                    var lista = File.ReadAllLines(theDialog.FileName);

                    foreach (var email in lista)
                    {
                        var split = email.Split(':');
                        E_Mail mail = new E_Mail { EMAIL = split[0], PASS = split[1] };
                        EMAIl_LIST.Add(mail);
                    }

                    lbl_Email_Load.Text = EMAIl_LIST.Count.ToString();
                    lbl_Email_Load.ForeColor = Color.DarkCyan;
                    lbl_Email_Load.Font = new Font("Arial", 10, FontStyle.Bold);
                    Log.info(EMAIl_LIST.Count + " E-Mails Load!");
                    SaveConfig();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR To Read File!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lbl_Email_Load.Text = "ERROR";
                lbl_Email_Load.ForeColor = Color.Red;
            }
        }


        private void Main_Load(object sender, EventArgs e)
        {
            #region File_create_IfNoExist

            if (!Directory.Exists(Database_Path))
            {
                Directory.CreateDirectory(Database_Path);
                    Directory.CreateDirectory(Database_Path + "Created_Accounts");
            }


            if (!File.Exists(Used_Mail_DB_Path))
            {
                AlreadyUsed usado = new AlreadyUsed { EMAIL = "No_delete@mail.cappi", BINDING_ACCS = 0 };
                List<AlreadyUsed> usados = new List<AlreadyUsed>();
                usados.Add(usado);

                UsedEmailDatabase usedEmailDatabase = new UsedEmailDatabase { AlreadyUsed = usados };

                File.WriteAllText(Used_Mail_DB_Path, JsonConvert.SerializeObject(usedEmailDatabase, Formatting.Indented));
            }

            if (!File.Exists(creationid_DB_READ))
            {
                List<string> tete = new List<string> { "123", "1234" };
                creationid_DB Creationid_DB = new creationid_DB { Creationid = tete };

                File.WriteAllText(creationid_DB_READ, JsonConvert.SerializeObject(Creationid_DB, Formatting.Indented));
            }

            #endregion

            Process[] procs = Process.GetProcessesByName("chromedriver");
            foreach (Process p in procs)
            {
                p.Kill();
                Log.info($"{p} process Kill");
            }

            LoadConfig();

        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void btn_ConfirLink_Click(object sender, EventArgs e)
        {
            if(email == null)
            {
                MessageBox.Show("E-Mail is empty!!", "Attention!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //Get_Email_Confirmation.GetLinkFromEmail(Main.email);
            string URL = AccessEmailPop3Client.Get_URL_Confirm(Main.email);

            if(!string.IsNullOrWhiteSpace(URL))
            Main._Form1.btn_SaveAcc.Enabled = true;
        }


        private void btn_GenLoginPass_Click(object sender, EventArgs e)
        {
            Main.Login = Get_Random.RandomLogin(15, false);
            Main.Pass = Get_Random.RandomPassword();

            lbl_Login.Text = Main.Login;
            lbl_Pass.Text = Main.Pass;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (EMAIl_LIST.Count == 0)
            {
                MessageBox.Show("E-Mail List is empty!!", "Attention!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Main.email = ManageEmails.Get_Mail(Main.Max_Acc_Por_Email);//pegar um e-mail funcionando e que esteja sem o limite de contas vinculadas ultrapassado no db

            lbl_Email.Text = email.EMAIL;
            lbl_EmailPass.Text = email.PASS;
        }

        private void btn_SaveAcc_Click(object sender, EventArgs e)
        {
            string path_to_save = Path.Combine(Main.Acc_Create_Path, $"{Main.Login}.txt");

            if (string.IsNullOrWhiteSpace(Main.Login))
            {
                MessageBox.Show("Login is empty!!", "Attention!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrWhiteSpace(Main.Pass))
            {
                MessageBox.Show("Pass is empty!!", "Attention!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!File.Exists(path_to_save))
            {
                StreamWriter sw;
                sw = File.AppendText(path_to_save);

                sw.WriteLine(Main.Login + ":" + Main.Pass +
                    "\r\n\r\nEmail: " + Main.email.EMAIL +
                    "\r\nEMail Password: " + Main.email.PASS);

                sw.Close();
                sw.Dispose();
            }

            Log.info("Account Create: " + Main.Login);

            ManageEmails.Add_Mail_To_DB(Main.email.EMAIL);

            lbl_Email.Text = "";
            lbl_EmailPass.Text = "";

            lbl_Login.Text = "";
            lbl_Pass.Text = "";

            lbl_OpenAccFile.Text = $"{Main.Login}.txt";
            lbl_OpenAccFile.Visible = true;

            Thread th = new Thread(() => Customize_profile.Login_An_Customize(path_to_save, Main.Login,Main.Pass));
            th.IsBackground = true;
            th.Start();

            btn_SaveAcc.Enabled = false;
        }

        private void lbl_OpenAccFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = Path.Combine(Main.Acc_Create_Path, lbl_OpenAccFile.Text);

            Process myProcess = new Process();
            myProcess.StartInfo.UseShellExecute = true;
            myProcess.StartInfo.FileName = path;
            myProcess.Start();
        }

        private void btn_CopyLogin_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lbl_Login.Text);
        }

        private void btn_CopyPass_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lbl_Pass.Text);
        }

        private void btn_CopyEmail_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lbl_Email.Text);
        }

        private void btn_open_File_avatarURL_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog theDialog = new OpenFileDialog();
                theDialog.Title = "Open Text File";
                theDialog.Filter = "TXT files|*.txt";
                theDialog.InitialDirectory = Application.StartupPath;
                if (theDialog.ShowDialog() == DialogResult.OK)
                {
                    AvatarImageFilePath = theDialog.FileName;

                    Avatar_URL_List.Clear();
                    var lista = File.ReadAllLines(theDialog.FileName);
                    foreach (var email in lista)
                    {
                        Avatar_URL_List.Add(email);
                    }

                    lbl_Avatar_Load.Text = Avatar_URL_List.Count.ToString();
                    lbl_Avatar_Load.ForeColor = Color.DarkCyan;
                    lbl_Avatar_Load.Font = new Font("Arial", 10, FontStyle.Bold);
                    Log.info(Avatar_URL_List.Count + " Avatars Load!");
                    SaveConfig();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR To Read File!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lbl_Email_Load.Text = "ERROR";
                lbl_Email_Load.ForeColor = Color.Red;
            }
        }

        private void ck_use_custom_avatar_CheckedChanged(object sender, EventArgs e)
        {
            if (ck_use_custom_avatar.Checked == true)
            {
                gp_box_avatar.Enabled = true;
            }
            else
            {
                gp_box_avatar.Enabled = false;
            }
        }

        public static void SaveConfig()
        {
            Config config = new Config();
            config.AvatarImageFilePath = Main._Form1.AvatarImageFilePath;
            config.EmailFilePath = Main._Form1.EmailFilePath;

            System.IO.File.WriteAllText(Database_Path + "Config.json", JsonConvert.SerializeObject(config, Formatting.Indented));//salvar o arquivo appids atualizado

            Log.info("Config Save..");
        }

        public static void LoadConfig()
        {
            if(File.Exists(Database_Path + "Config.json"))
            {
                try
                {
                    Config config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(Database_Path + "Config.json"));
                    if (config.AvatarImageFilePath != "")
                    {
                        Main._Form1.AvatarImageFilePath = config.AvatarImageFilePath;

                        Avatar_URL_List.Clear();
                        var lista = File.ReadAllLines(config.AvatarImageFilePath);
                        foreach (var email in lista)
                        {
                            Avatar_URL_List.Add(email);
                        }

                        Main._Form1.lbl_Avatar_Load.Text = Avatar_URL_List.Count.ToString();
                        Main._Form1.lbl_Avatar_Load.ForeColor = Color.DarkCyan;
                        Main._Form1.lbl_Avatar_Load.Font = new Font("Arial", 10, FontStyle.Bold);
                        Log.info(Avatar_URL_List.Count + " Avatars Load!");
                        Main._Form1.ck_use_custom_avatar.Checked = true;
                    }

                    if (config.EmailFilePath != "")
                    {

                        Main._Form1.EmailFilePath = config.EmailFilePath;

                        EMAIl_LIST.Clear();
                        var lista = File.ReadAllLines(config.EmailFilePath);

                        foreach (var email in lista)
                        {
                            var split = email.Split(':');
                            E_Mail mail = new E_Mail { EMAIL = split[0], PASS = split[1] };
                            EMAIl_LIST.Add(mail);
                        }

                        Main._Form1.lbl_Email_Load.Text = EMAIl_LIST.Count.ToString();
                        Main._Form1.lbl_Email_Load.ForeColor = Color.DarkCyan;
                        Main._Form1.lbl_Email_Load.Font = new Font("Arial", 10, FontStyle.Bold);
                        Log.info(EMAIl_LIST.Count + " E-Mails Load!");
                    }
                }
                catch(Exception ex)
                {
                    Log.error($"Error to load Config: {ex.Message}");
                }
            }
        }

        private void btn_OpenCriationPage_Click(object sender, EventArgs e)
        {
            string path = "https://store.steampowered.com/join/?l=english";

            Process myProcess = new Process();
            myProcess.StartInfo.UseShellExecute = true;
            myProcess.StartInfo.FileName = path;
            myProcess.Start();
        }
    }
}
