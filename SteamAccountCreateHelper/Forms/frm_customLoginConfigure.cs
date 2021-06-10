using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteamAccountCreateHelper.Forms
{
    public partial class frm_customLoginConfigure : Form
    {

        public static frm_customLoginConfigure frm;

        public frm_customLoginConfigure()
        {
            frm = this;
            InitializeComponent();

            if (!string.IsNullOrWhiteSpace(Main._Form1.txt_CustomLoginGeneratorFormat.Text))
            {
                txtFormat.Text = Main._Form1.txt_CustomLoginGeneratorFormat.Text;
            }

            Thread th = new Thread(() => GenVariables());
            th.IsBackground = true;
            th.Start();
        }

        private void btn_Generate_Click(object sender, EventArgs e)
        {
            Main._Form1.txt_CustomLoginGeneratorFormat.Text = txtFormat.Text;
            Main.SaveConfig();
            this.Close();
        }

        public void GenVariables()
        {
            while (true)
            {
                Thread.Sleep(500);

                try
                {
                    frm_customLoginConfigure.frm.Invoke(new Action(() =>
                    {
                        lbl_GenResult.Text = RandomUtils.RandomLoginCustomFormat(txtFormat.Text);
                        lbl_GenLetter.Text = RandomUtils.RandomString(1, true);
                        lbl_GenSingleNum.Text = RandomUtils.RandomSingleNumber().ToString();
                        lbl_GenNum.Text = RandomUtils.RandomNumber().ToString();
                    }));
                }
                catch
                {

                }
            }
        }
    }
}
