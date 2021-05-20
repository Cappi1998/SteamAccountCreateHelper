using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAccountCreateSelenium
{
    class CreationID_DB_Check
    {
        private static readonly object locker = new object();

        public static bool creationid_JaUSADO(string creationid)
        {
            creationid_DB creationid_DB = JsonConvert.DeserializeObject<creationid_DB>(File.ReadAllText(Main.creationid_DB_READ));

            if (creationid_DB.Creationid.Contains(creationid))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static void creationid_ADD_TO_DB(string creationid)
        {

            lock (locker)
            {
                creationid_DB creationid_DB = JsonConvert.DeserializeObject<creationid_DB>(File.ReadAllText(Main.creationid_DB_READ));

                creationid_DB.Creationid.Add(creationid);

                File.WriteAllText(Main.creationid_DB_READ, JsonConvert.SerializeObject(creationid_DB, Formatting.Indented));
            }
        }
    }



    public class creationid_DB
    {
        public List<string> Creationid { get; set; }
    }
}
