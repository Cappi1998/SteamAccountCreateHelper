using Newtonsoft.Json;
using S22.Imap;
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
            int contas_ja_Vinculadas = 0;

            MAIL_DATABASE mAIL_DATABASE = JsonConvert.DeserializeObject<MAIL_DATABASE>(File.ReadAllText(Main.Used_Mail_DB_Path));

            if (mAIL_DATABASE.usados == null)
            {
                return contas_ja_Vinculadas;
            }
            foreach (Usados email in mAIL_DATABASE.usados)
            {

                if (email.EMAIL == e_mail)
                {
                    contas_ja_Vinculadas = email.ACCS_VINCULADAS;
                }
            }

            return contas_ja_Vinculadas;
        }

        public static E_Mail Get_Mail(int max_acc_Por_Email)
        {
            inicio:
            E_Mail mail = Main.EMAIl_LIST[RandomUtils.GetRandomInt(0, Main.EMAIl_LIST.Count)];

            int contas_Vinculada = ManageEmails.Check_amount_accs_on_Email(mail.EMAIL);


            while (contas_Vinculada > max_acc_Por_Email)
            {
                mail = Main.EMAIl_LIST[RandomUtils.GetRandomInt(0, Main.EMAIl_LIST.Count)];
                contas_Vinculada = ManageEmails.Check_amount_accs_on_Email(mail.EMAIL);
            }

            bool Email_funciona = Check_E_Mail(mail);

            if(Email_funciona == true)
            {
                return mail;
            }
            else
            {
                goto inicio;
            }
        }

        public static bool Check_E_Mail(E_Mail mail)
        {
            string hostname = "imap.gmail.com";

            var devide = mail.EMAIL.Split('@');

            if (devide[1] == "rambler.ru")
            {
                hostname = "imap.rambler.ru";
            }

            if (devide[1] == "ro.ru")
            {
                hostname = "imap.rambler.ru";
            }

            if (devide[1] == "yandex.ru")
            {
                hostname = "imap.yandex.ru";
            }

            string username = mail.EMAIL, password = mail.EMAIL_PASS;


            try
            {
                // The default port for IMAP over SSL is 993.
                ImapClient client = new ImapClient(hostname, 993, username, password, AuthMethod.Login, true);

                if (client.Authed == true)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.error("Error to acess E-mail: " + username);
                Log.error(ex.Message);
                return false;
            }

            return false;
        }

        public static void Add_Mail_To_DB(string mail)
        {
            lock (locker)
            {
                MAIL_DATABASE mAIL_DATABASE = JsonConvert.DeserializeObject<MAIL_DATABASE>(File.ReadAllText(Main.Used_Mail_DB_Path));

                bool ja_no_DB = false;

                foreach (var conta in mAIL_DATABASE.usados)
                {
                    if (conta.EMAIL == mail)
                    {
                        conta.ACCS_VINCULADAS = conta.ACCS_VINCULADAS + 1;
                        ja_no_DB = true;
                    }
                }

                if (ja_no_DB == false)
                {
                    Usados usado = new Usados { ACCS_VINCULADAS = 1, EMAIL = mail };
                    mAIL_DATABASE.usados.Add(usado);

                }

                File.WriteAllText(Main.Used_Mail_DB_Path, JsonConvert.SerializeObject(mAIL_DATABASE, Formatting.Indented));
            }
        }

        public static string Get_Mail_Pass(string mail)
        {
            string senha = "";

            foreach (var email in Main.EMAIl_LIST)
            {

                if (email.EMAIL == mail)
                {
                    senha = email.EMAIL_PASS;
                }
            }

            return senha;
        }


        public class MAIL_DATABASE
        {
            public List<Usados> usados { get; set; }
        }
        public class Usados
        {
            public string EMAIL { get; set; }

            public int ACCS_VINCULADAS { get; set; }
        }
    }
}
