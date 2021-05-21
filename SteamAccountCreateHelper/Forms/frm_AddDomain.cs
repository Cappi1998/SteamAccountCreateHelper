using Newtonsoft.Json;
using SteamAccountCreateHelper.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteamAccountCreateHelper
{
    public partial class frm_AddDomain : Form
    {
        public frm_AddDomain()
        {
            InitializeComponent();
        }

        private void btn_Added_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_PopServer.Text))
            {
                MessageBox.Show("Error - Pop server is null!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_PopDomainsName.Text))
            {
                MessageBox.Show("Error - Pop Domains Name is null!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<Pop3> pop3s =  JsonConvert.DeserializeObject<List<Pop3>>(File.ReadAllText(Main.Pop3Domains_Path));

            string pop3server = txt_PopServer.Text;

            List<string> popDomainsName = txt_PopDomainsName.Text.Split(new char[] { ',' }).ToList();

            var existInConfig = pop3s.Where(a => a.PoP3Server == pop3server).FirstOrDefault();

            if(existInConfig != null)
            {
                foreach(var domainName in popDomainsName) 
                {
                    if (!existInConfig.SuportedDomains.Contains(domainName))
                        existInConfig.SuportedDomains.Add(domainName);
                }

                System.IO.File.WriteAllText(Main.Pop3Domains_Path, JsonConvert.SerializeObject(pop3s, Formatting.Indented));
            }
            else
            {
                Pop3 pop3 = new Pop3();
                pop3.PoP3Server = pop3server;
                pop3.SuportedDomains = popDomainsName;

                pop3s.Add(pop3);
                System.IO.File.WriteAllText(Main.Pop3Domains_Path, JsonConvert.SerializeObject(pop3s, Formatting.Indented));
            }

            Main._Form1.lbl_TotalDomainsConfig.Text = pop3s.Count().ToString();
            Main._Form1.lbl_TotalDomainsConfig.ForeColor = Color.DarkGreen;

            Main.pop3s = pop3s;

            this.Close();
        }
    }
}
