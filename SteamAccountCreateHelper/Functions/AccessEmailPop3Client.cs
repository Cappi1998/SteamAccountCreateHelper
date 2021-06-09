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
using System.Diagnostics;
using SteamAccountCreateHelper.Models;
using SteamAccountCreateHelper.Utils;
using SteamAccountCreateHelper;

namespace SteamAccountCreateHelper
{
    class AccessEmailPop3Client
    {
        private static readonly object locker = new object();
        public static Pop3Client Pop3Client;

        public static bool CheckEmailAccess(E_Mail mail)
        {
            string Pop3HostName = "";
            var splittogetdomain = mail.EMAIL.Split('@');
            string domainName = splittogetdomain[1];

            var popconfig = Main.pop3s.Where(a => a.SuportedDomains.Contains(domainName)).FirstOrDefault();
            
            if(popconfig != null)
            {
                Pop3HostName = popconfig.PoP3Server;
            }
            else
            {
                Log.error($"Pop3 HostName not defined for {domainName}");
                return false;
            }

            string username = mail.EMAIL, password = mail.PASS;

            using (var client = new Pop3Client())
            {
                try
                {
                    client.Connect(Pop3HostName, 995, true);

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

        public static void Get_URL_Confirm(E_Mail mail)
        {
            int tentativas_get_code = 0;

        INICIO:

            string Confirm_Link = "";

            string Pop3HostName = "";
            var splittogetdomain = mail.EMAIL.Split('@');
            string domainName = splittogetdomain[1];

            var popconfig = Main.pop3s.Where(a => a.SuportedDomains.Contains(domainName)).FirstOrDefault();

            if (popconfig != null)
            {
                Pop3HostName = popconfig.PoP3Server;
            }
            else
            {
                Log.error($"Pop3 HostName not defined for {domainName}");
                return;
            }

            string username = mail.EMAIL, password = mail.PASS;


            using (var client = new Pop3Client())
            {
                try
                {
                    client.Connect(Pop3HostName, 995, true);

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


                                if (Main._Form1.ck_OpenDefaultdw.Checked)
                                {
                                    Process myProcess = new Process();
                                    myProcess.StartInfo.UseShellExecute = true;
                                    myProcess.StartInfo.FileName = Confirm_Link;
                                    myProcess.Start();

                                    Main._Form1.Invoke(new Action(() => Main._Form1.btn_SaveAcc.Enabled = true));

                                    Thread th = new Thread(() => Main.CheckExistingAccountOnEmail());
                                    th.IsBackground = true;
                                    th.Start();

                                    
                                }
                                else
                                {
                                    var request = new RequestBuilder(Confirm_Link).GET().Execute();

                                    Main.CheckExistingAccountOnEmail();

                                    Main._Form1.Invoke(new Action(() => Main._Form1.btn_SaveAcc.Enabled = true));

                                    Thread th = new Thread(() => Main.CheckExistingAccountOnEmail());
                                    th.IsBackground = true;
                                    th.Start();
                                }
                                
                                lock (locker)
                                {
                                    CreationID_DB.ADD_TO_DB(creationid);
                                }
                            }
                        }
                        catch
                        {
                            Log.error("Erro To get Confirm URL. Try Again!!");
                            Thread.Sleep(TimeSpan.FromSeconds(15));
                            tentativas_get_code = tentativas_get_code + 1;
                            client.Disconnect(true);
                            goto INICIO;
                        }
                    }
                    else
                    {
                        Log.error("Erro To get Confirm URL From Email!");
                    }
                }
                client.Disconnect(true);
            }
        }
    }
}
