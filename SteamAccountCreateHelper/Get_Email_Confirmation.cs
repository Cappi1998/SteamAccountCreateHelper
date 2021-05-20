using S22.Imap;
using SteamAccountCreateSelenium.Models;
using SteamAccountCreateSelenium.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SteamAccountCreateSelenium
{
    class Get_Email_Confirmation
    {
        private static readonly object locker = new object();
        public static ImapClient imapClient;
        public static void GetLinkFromEmail(E_Mail mail)
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

            string username = mail.EMAIL;
            string password = mail.EMAIL_PASS;
            // The default port for IMAP over SSL is 993.

            try
            {
                ImapClient client = new ImapClient(hostname, 993, username, password, AuthMethod.Login, true);

                if (client.Authed == true)
                {
                    imapClient = client;
                }
            }
            catch (Exception ex)
            {
                Log.error("Error to acess E-mail: " + username);
                Log.error(ex.Message);
            }

            // Get a list of unique identifiers (UIDs) of all unread messages in the mailbox.

            IEnumerable<uint> pesquisa_email = imapClient.Search(SearchCondition.All());//pegar lista de todos os email da steam

            List<uint> Ultimos_5 = Enumerable.Reverse(pesquisa_email).Take(5).Reverse().ToList();
            if (Ultimos_5.Count == 0)
            {
                Log.info("No Mail Confirmation Found!!");
            }
            foreach (var email in Ultimos_5)
            {
                MailMessage messages = imapClient.GetMessage(email, FetchOptions.Normal);

                if (messages.Body.Contains("steampowered.com/account/newaccountverification"))
                {

                    var creationid = new Regex("creationid=(\\d+)").Match(messages.Body).Groups[1].Value;

                    var stoken = new Regex("(?<=stoken\\=)\\w+").Match(messages.Body).Value;

                    bool ja_usado = CreationID_DB_Check.creationid_JaUSADO(creationid);

                    if (ja_usado == true)
                    {
                        continue;
                    }
                    else
                    {
                        string URL = $"https://store.steampowered.com/account/newaccountverification?stoken={stoken}&creationid={creationid}";

                        Process myProcess = new Process();
                        myProcess.StartInfo.UseShellExecute = true;
                        myProcess.StartInfo.FileName = URL;
                        myProcess.Start();


                        lock (locker)
                        {
                            CreationID_DB_Check.creationid_ADD_TO_DB(creationid);
                        }
                    }
                }
            }
        }
    }
}
