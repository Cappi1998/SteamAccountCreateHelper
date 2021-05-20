using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Pop3;
using MailKit;
using MimeKit;
using System.Text.RegularExpressions;
using System.Threading;
using System.Drawing;
using SteamAccountCreateSelenium.Models;
using SteamAccountCreateSelenium.Utils;
using System.Diagnostics;

namespace SteamAccountCreateSelenium
{
    class AccessEmailPop3Client
    {
        private static readonly object locker = new object();
        public static Pop3Client Pop3Client;

        public static bool CheckEmailAccess(E_Mail mail)
        {
            string hostname = "pop.gmail.com";

            var devide = mail.EMAIL.Split('@');

            if (devide[1] == "rambler.ru")
            {
                hostname = "pop.rambler.ru";
            }

            if (devide[1] == "ro.ru")
            {
                hostname = "pop.rambler.ru";
            }

            if (devide[1] == "yandex.ru")
            {
                hostname = "pop.yandex.com";
            }

            string username = mail.EMAIL;
            string password = mail.PASS;

            using (var client = new Pop3Client())
            {
                try
                {
                    client.Connect(hostname, 995, true);

                    client.Authenticate(username, password);

                    if (client.IsAuthenticated == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Log.error("Error to acess E-mail: " + username);
                    Log.error(ex.Message);
                    return false;
                }
            }
        }

        public static string Get_URL_Confirm(E_Mail mail)
        {
            int tentativas_get_code = 0;

        INICIO:

            string Confirm_Link = "";

            string hostname = "pop.gmail.com";

            var devide = mail.EMAIL.Split('@');

            if (devide[1] == "rambler.ru")
            {
                hostname = "pop.rambler.ru";
            }

            if (devide[1] == "ro.ru")
            {
                hostname = "pop.rambler.ru";
            }

            if (devide[1] == "yandex.ru")
            {
                hostname = "pop.yandex.com";
            }

            string username = mail.EMAIL;
            string password = mail.PASS;


            using (var client = new Pop3Client())
            {


                try
                {
                    client.Connect(hostname, 995, true);

                    client.Authenticate(username, password);

                    if (client.IsAuthenticated == true)
                    {
                        Pop3Client = client;
                    }
                }
                catch (Exception ex)
                {
                    Log.error("Error to acess E-mail: " + username);
                    Log.error(ex.Message);
                }

                while (Confirm_Link == "")
                {

                    if (tentativas_get_code < 6)
                    {
                        try
                        {
                            var message1 = client.GetMessage(client.Count - 1);

                            var creationid = new Regex("creationid=(\\d+)").Match(message1.Body.ToString()).Groups[1].Value;

                            var stoken = new Regex("(?<=stoken\\=)\\w+").Match(message1.Body.ToString()).Value;

                            bool ja_usado = CreationID_DB.Check_AlreadyUsed(creationid);

                            if (ja_usado == true)
                            {
                                continue;
                            }
                            else
                            {
                                Confirm_Link = $"https://store.steampowered.com/account/newaccountverification?stoken={stoken}&creationid={creationid}";

                                Process myProcess = new Process();
                                myProcess.StartInfo.UseShellExecute = true;
                                myProcess.StartInfo.FileName = Confirm_Link;
                                myProcess.Start();

                                lock (locker)
                                {
                                    CreationID_DB.ADD_TO_DB(creationid);
                                }
                            }
                        }
                        catch
                        {
                            Log.error("Erro To get Guard Code. Try Again!!");
                            Thread.Sleep(TimeSpan.FromSeconds(15));
                            tentativas_get_code = tentativas_get_code + 1;
                            client.Disconnect(true);
                            goto INICIO;
                        }
                    }
                    else
                    {
                        Log.error("Erro To get Guard Code From Email!");
                        return Confirm_Link;
                    }
                }
                client.Disconnect(true);
            }

            return Confirm_Link;
        }
    }
}
