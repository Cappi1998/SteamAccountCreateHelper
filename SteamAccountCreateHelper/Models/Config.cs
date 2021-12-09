using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAccountCreateHelper.Models
{
    class Config
    {
        public string EmailFilePath { get; set; }
        public string AvatarImageFilePath { get; set; }

        public string SingleProxyText { get; set; }
        public bool SingleProxyChecked { get; set; }

        public string LoginGeneratorCustomFormat { get; set; }
        public bool UseCustomLoginGenerator { get; set; }

        public int AccountPerEmail { get; set; }
    }
}
