using Newtonsoft.Json;
using Steam_ACC_Create;
using SteamAccountCreateSelenium.Models;
using SteamAccountCreateSelenium.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAccountCreateSelenium
{
    class ManageEmails
    {
        private static readonly object locker = new object();
        public static int Check_amount_accs_on_Email(string e_mail)
        {

            UsedEmailDatabase mAIL_DATABASE = JsonConvert.DeserializeObject<UsedEmailDatabase>(File.ReadAllText(Main.Used_Mail_DB_Path));

            if (mAIL_DATABASE.AlreadyUsed == null)
            {
                return 0;
            }

            AlreadyUsed UsedMail = mAIL_DATABASE.AlreadyUsed.Where(a => a.EMAIL == e_mail).FirstOrDefault();

            if(UsedMail != null)
            {
                return UsedMail.BINDING_ACCS;
            }
            else
            {
                return 0;
            }
        }

        public static void Get_Mail(int max_acc_Por_Email)
        {
            inicio:
            E_Mail mail = Main.EMAIl_LIST[RandomUtils.GetRandomInt(0, Main.EMAIl_LIST.Count)];

            int contas_Vinculada = ManageEmails.Check_amount_accs_on_Email(mail.EMAIL);

            while (contas_Vinculada > max_acc_Por_Email)
            {
                // TODO - Atention Infinite Loop Error
                mail = Main.EMAIl_LIST[RandomUtils.GetRandomInt(0, Main.EMAIl_LIST.Count)];
                contas_Vinculada = ManageEmails.Check_amount_accs_on_Email(mail.EMAIL);
            }

            bool working = AccessEmailPop3Client.CheckEmailAccess(mail);

            if(working == true)
            {
                Main._Form1.Invoke(new Action(() => Main.email = mail));

                Main._Form1.Invoke(new Action(() => Main._Form1.lbl_Email.Text = mail.EMAIL));
                Main._Form1.Invoke(new Action(() => Main._Form1.lbl_EmailPass.Text = mail.PASS));
                Main._Form1.Invoke(new Action(() => Main._Form1.btn_GetEmail.Enabled = true));
            }
            else
            {
                goto inicio;
            }
        }

        public static void Add_Mail_To_DB(string mail)
        {
            lock (locker)
            {
                UsedEmailDatabase mAIL_DATABASE = JsonConvert.DeserializeObject<UsedEmailDatabase>(File.ReadAllText(Main.Used_Mail_DB_Path));
                AlreadyUsed UsedMail = mAIL_DATABASE.AlreadyUsed.Where(a => a.EMAIL == mail).FirstOrDefault();

                if(UsedMail != null)
                {
                    UsedMail.BINDING_ACCS ++;
                }
                else
                {
                    AlreadyUsed Used = new AlreadyUsed { BINDING_ACCS = 1, EMAIL = mail };
                    mAIL_DATABASE.AlreadyUsed.Add(Used);
                }
                File.WriteAllText(Main.Used_Mail_DB_Path, JsonConvert.SerializeObject(mAIL_DATABASE, Formatting.Indented));
            }
        }

        public static string Get_Mail_Pass(string mail)
        {
            E_Mail e_Mail =  Main.EMAIl_LIST.Where(a => a.EMAIL == mail).FirstOrDefault();

            if(e_Mail != null)
            {
                return e_Mail.PASS;
            }
            else
            {
                return null;
            }
        }

        public class UsedEmailDatabase
        {
            public List<AlreadyUsed> AlreadyUsed { get; set; }
        }
        public class AlreadyUsed
        {
            public string EMAIL { get; set; }

            public int BINDING_ACCS { get; set; }
        }
    }
}
