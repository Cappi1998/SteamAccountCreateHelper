
namespace SteamAccountCreateHelper.Forms
{
    partial class frm_customLoginConfigure
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_customLoginConfigure));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_GenNum = new System.Windows.Forms.Label();
            this.lbl_GenSingleNum = new System.Windows.Forms.Label();
            this.lbl_GenLetter = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFormat = new System.Windows.Forms.TextBox();
            this.btn_Save = new System.Windows.Forms.Button();
            this.lbl_GenResult = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Custom Format:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_GenNum);
            this.groupBox1.Controls.Add(this.lbl_GenSingleNum);
            this.groupBox1.Controls.Add(this.lbl_GenLetter);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 117);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(458, 116);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Available Variables";
            // 
            // lbl_GenNum
            // 
            this.lbl_GenNum.AutoSize = true;
            this.lbl_GenNum.Location = new System.Drawing.Point(170, 79);
            this.lbl_GenNum.Name = "lbl_GenNum";
            this.lbl_GenNum.Size = new System.Drawing.Size(12, 15);
            this.lbl_GenNum.TabIndex = 7;
            this.lbl_GenNum.Text = "-";
            // 
            // lbl_GenSingleNum
            // 
            this.lbl_GenSingleNum.AutoSize = true;
            this.lbl_GenSingleNum.Location = new System.Drawing.Point(236, 54);
            this.lbl_GenSingleNum.Name = "lbl_GenSingleNum";
            this.lbl_GenSingleNum.Size = new System.Drawing.Size(12, 15);
            this.lbl_GenSingleNum.TabIndex = 6;
            this.lbl_GenSingleNum.Text = "-";
            // 
            // lbl_GenLetter
            // 
            this.lbl_GenLetter.AutoSize = true;
            this.lbl_GenLetter.Location = new System.Drawing.Point(175, 28);
            this.lbl_GenLetter.Name = "lbl_GenLetter";
            this.lbl_GenLetter.Size = new System.Drawing.Size(12, 15);
            this.lbl_GenLetter.TabIndex = 5;
            this.lbl_GenLetter.Text = "-";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(148, 15);
            this.label5.TabIndex = 3;
            this.label5.Text = "{num} = Random Num =>";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(153, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "{letter} = Random Letter =>";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(214, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "{singlenum} = Random Single Num =>";
            // 
            // txtFormat
            // 
            this.txtFormat.Location = new System.Drawing.Point(28, 42);
            this.txtFormat.Name = "txtFormat";
            this.txtFormat.Size = new System.Drawing.Size(271, 23);
            this.txtFormat.TabIndex = 2;
            this.txtFormat.Text = "{singlenum}_fabiocapp{letter}_{num}";
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(305, 42);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(165, 44);
            this.btn_Save.TabIndex = 3;
            this.btn_Save.Text = " Save Format";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Generate_Click);
            // 
            // lbl_GenResult
            // 
            this.lbl_GenResult.AutoSize = true;
            this.lbl_GenResult.Location = new System.Drawing.Point(100, 82);
            this.lbl_GenResult.Name = "lbl_GenResult";
            this.lbl_GenResult.Size = new System.Drawing.Size(12, 15);
            this.lbl_GenResult.TabIndex = 4;
            this.lbl_GenResult.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Gen Result:";
            // 
            // frm_customLoginConfigure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 242);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbl_GenResult);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.txtFormat);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_customLoginConfigure";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Custom Login Format Config";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_Save;
        public System.Windows.Forms.Label lbl_GenResult;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFormat;
        public System.Windows.Forms.Label lbl_GenNum;
        public System.Windows.Forms.Label lbl_GenSingleNum;
        public System.Windows.Forms.Label lbl_GenLetter;
    }
}