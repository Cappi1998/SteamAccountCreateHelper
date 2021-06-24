using Newtonsoft.Json;
using SteamAccountCreateHelper.Models;
using System;
using System.Collections.Generic;
using System.IO;
using static SteamAccountCreateHelper.ManageEmails;

namespace SteamAccountCreateHelper.Utils
{
    class CheckDiretoryOnStartup
    {
        public static string BasePath = AppDomain.CurrentDomain.BaseDirectory;
        public static void CheckDiretory()
        {
            #region File_create_IfNoExist

            if (!Directory.Exists(Main.Database_Path))
            {
                Directory.CreateDirectory(Main.Database_Path);
                Directory.CreateDirectory(Main.Database_Path + "Created_Accounts");
            }


            if (!File.Exists(Main.Used_Mail_DB_Path))
            {
                AlreadyUsed usado = new AlreadyUsed { EMAIL = "No_delete@mail.cappi", LinkedAccounts = 0 };
                List<AlreadyUsed> usados = new List<AlreadyUsed>();
                usados.Add(usado);

                UsedEmailDatabase usedEmailDatabase = new UsedEmailDatabase { AlreadyUsed = usados };

                File.WriteAllText(Main.Used_Mail_DB_Path, JsonConvert.SerializeObject(usedEmailDatabase, Formatting.Indented));
            }

            if (!File.Exists(Main.CreationidDB_Path))
            {
                creationid_DB creationid_DB = new creationid_DB { Creationid = new List<string>() };

                File.WriteAllText(Main.CreationidDB_Path, JsonConvert.SerializeObject(creationid_DB, Formatting.Indented));
            }


            if (!File.Exists(Main.Pop3Domains_Path))
            {
                try
                {
                    var response = new RequestBuilder("https://raw.githubusercontent.com/Cappi1998/SteamAccountCreateHelper/master/SteamAccountCreateHelper/DatabaseFiles/Pop3Domains.json").GET()
                        .Execute();

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        List<Pop3> pop3 = JsonConvert.DeserializeObject<List<Pop3>>(response.Content);
                        File.WriteAllText(Main.Pop3Domains_Path, JsonConvert.SerializeObject(pop3, Formatting.Indented));
                        Log.info($"Pop3Domains.json automatically downloaded from the repository https://github.com/Cappi1998/SteamAccountCreateHelper");
                    }
                }
                catch
                {
                    List<Pop3> pop3 = new List<Pop3>();
                    File.WriteAllText(Main.Pop3Domains_Path, JsonConvert.SerializeObject(pop3, Formatting.Indented));
                }
            }

            #endregion
        }
    }
}
