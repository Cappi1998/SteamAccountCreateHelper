using Newtonsoft.Json;
using SteamAccountCreateHelper.Models;
using SteamAccountCreateHelper.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteamAccountCreateHelper
{
    class ManageEmails
    {
        private static readonly object locker = new object();

        public static void Get_Mail(int MaxAccInEmail)
        {
            int trycount = 0;
            string OldEmailChecked = "";
            inicio:

            if(trycount >= 5)
            {
                Main._Form1.ShowMessageBox($"NO EMAIL AVAILABLE\r\n", "NO EMAIL AVAILABLE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AddedEmailToForm(new E_Mail { EMAIL = "NO EMAIL AVAILABLE" }, false);
                return;
            }
                

            var avaliableemails = Main.EMAIl_LIST.Where(a => a.LinkedAccounts < MaxAccInEmail).ToList();

            if(avaliableemails.Count == 0)
            {
                MessageBox.Show($"No email available, all emails already have the maximum number of accounts => {MaxAccInEmail}", "Attention!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Main._Form1.Invoke(new Action(() => Main._Form1.btn_GetEmail.Enabled = true));
                return;
            }

            E_Mail mail = avaliableemails[RandomUtils.GetRandomInt(0, avaliableemails.Count)];

            bool working = AccessEmailPop3Client.CheckEmailAccess(mail);

            if(working == true)
            {
                AddedEmailToForm(mail, true);
                Thread th = new Thread(() => Main.CheckConfirmEmailDialog());
                th.IsBackground = true;
                th.Start();
            }
            else
            {
                if(OldEmailChecked == mail.EMAIL) trycount++;

                OldEmailChecked = mail.EMAIL;
                goto inicio;
            }
        }

        public static void AddedEmailToForm(E_Mail mail, bool InputEmailInBrowser)
        {
            Main._Form1.Invoke(new Action(() => Main.email = mail));

            Main._Form1.Invoke(new Action(() => Main._Form1.lbl_Email.Text = mail.EMAIL));
            Main._Form1.Invoke(new Action(() => Main._Form1.btn_GetEmail.Enabled = true));
            Main._Form1.Invoke(new Action(() => Main._Form1.btn_ConfirLink.Enabled = true));

            if(InputEmailInBrowser) Main.AddedEmail(mail.EMAIL);
        }

        public static void Add_Mail_To_DB(string mail)
        {
            lock (locker)
            {
                UsedEmailDatabase mAIL_DATABASE = JsonConvert.DeserializeObject<UsedEmailDatabase>(File.ReadAllText(Main.Used_Mail_DB_Path));
                AlreadyUsed UsedMail = mAIL_DATABASE.AlreadyUsed.Where(a => a.EMAIL == mail).FirstOrDefault();

                if(UsedMail != null)
                {
                    UsedMail.LinkedAccounts++;
                }
                else
                {
                    AlreadyUsed Used = new AlreadyUsed { LinkedAccounts = 1, EMAIL = mail };
                    mAIL_DATABASE.AlreadyUsed.Add(Used);
                }
                File.WriteAllText(Main.Used_Mail_DB_Path, JsonConvert.SerializeObject(mAIL_DATABASE, Formatting.Indented));

                var ml = Main.EMAIl_LIST.Where(a => a.EMAIL == mail).FirstOrDefault();
                if(ml != null)
                {
                    ml.LinkedAccounts++;
                }
            }
        }

        public class UsedEmailDatabase
        {
            public List<AlreadyUsed> AlreadyUsed { get; set; }
        }
        public class AlreadyUsed
        {
            public string EMAIL { get; set; }

            public int LinkedAccounts { get; set; }
        }
    }
}
