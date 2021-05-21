
namespace SteamAccountCreateHelper
{
    partial class frm_AddDomain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Added = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_PopServer = new System.Windows.Forms.TextBox();
            this.txt_PopDomainsName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_Added
            // 
            this.btn_Added.Location = new System.Drawing.Point(315, 199);
            this.btn_Added.Name = "btn_Added";
            this.btn_Added.Size = new System.Drawing.Size(113, 39);
            this.btn_Added.TabIndex = 0;
            this.btn_Added.Text = "Added";
            this.btn_Added.UseVisualStyleBackColor = true;
            this.btn_Added.Click += new System.EventHandler(this.btn_Added_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "PoP3 Server";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Suported Domains";
            // 
            // txt_PopServer
            // 
            this.txt_PopServer.Location = new System.Drawing.Point(12, 27);
            this.txt_PopServer.Name = "txt_PopServer";
            this.txt_PopServer.Size = new System.Drawing.Size(416, 23);
            this.txt_PopServer.TabIndex = 3;
            // 
            // txt_PopDomainsName
            // 
            this.txt_PopDomainsName.Location = new System.Drawing.Point(12, 123);
            this.txt_PopDomainsName.Multiline = true;
            this.txt_PopDomainsName.Name = "txt_PopDomainsName";
            this.txt_PopDomainsName.Size = new System.Drawing.Size(416, 73);
            this.txt_PopDomainsName.TabIndex = 4;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(56, 217);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(184, 15);
            this.label12.TabIndex = 9;
            this.label12.Text = "yandex.ru,yandex.com,yandex.ua";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 199);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(96, 15);
            this.label13.TabIndex = 8;
            this.label13.Text = "Format Example:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "pop.yandex.ru";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "Format Example:";
            // 
            // frm_AddDomain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 248);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txt_PopDomainsName);
            this.Controls.Add(this.txt_PopServer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Added);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_AddDomain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add New Domain";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Added;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_PopServer;
        private System.Windows.Forms.TextBox txt_PopDomainsName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}