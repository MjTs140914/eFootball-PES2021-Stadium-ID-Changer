namespace eFootball_PES2021_Stadium_ID_Changer
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Label3 = new System.Windows.Forms.Label();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.btn_pack = new System.Windows.Forms.Button();
            this.btn_fpk = new System.Windows.Forms.Button();
            this.btn_finish = new System.Windows.Forms.Button();
            this.btn_change_id = new System.Windows.Forms.Button();
            this.lbl_info = new System.Windows.Forms.Label();
            this.btn_selectfolder = new System.Windows.Forms.Button();
            this.tx_new = new System.Windows.Forms.TextBox();
            this.tx_old = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(214, 150);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(164, 13);
            this.Label3.TabIndex = 23;
            this.Label3.Text = "Copyright © 2021 MjTs-140914™";
            // 
            // TextBox1
            // 
            this.TextBox1.Enabled = false;
            this.TextBox1.Location = new System.Drawing.Point(187, 15);
            this.TextBox1.Multiline = true;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = true;
            this.TextBox1.Size = new System.Drawing.Size(191, 46);
            this.TextBox1.TabIndex = 22;
            this.TextBox1.Text = "PES2021 Stadium ID Changer Tools\r\nDo not open folder if process in progress. (STI" +
    "D Changer: Version 1.0)";
            // 
            // btn_pack
            // 
            this.btn_pack.Enabled = false;
            this.btn_pack.Location = new System.Drawing.Point(286, 92);
            this.btn_pack.Name = "btn_pack";
            this.btn_pack.Size = new System.Drawing.Size(92, 23);
            this.btn_pack.TabIndex = 21;
            this.btn_pack.Text = "Pack Archive";
            this.btn_pack.UseVisualStyleBackColor = true;
            this.btn_pack.Click += new System.EventHandler(this.btn_pack_Click);
            // 
            // btn_fpk
            // 
            this.btn_fpk.Enabled = false;
            this.btn_fpk.Location = new System.Drawing.Point(111, 92);
            this.btn_fpk.Name = "btn_fpk";
            this.btn_fpk.Size = new System.Drawing.Size(88, 23);
            this.btn_fpk.TabIndex = 20;
            this.btn_fpk.Text = "Extract Archive";
            this.btn_fpk.UseVisualStyleBackColor = true;
            this.btn_fpk.Click += new System.EventHandler(this.btn_fpk_Click);
            // 
            // btn_finish
            // 
            this.btn_finish.Enabled = false;
            this.btn_finish.Location = new System.Drawing.Point(18, 121);
            this.btn_finish.Name = "btn_finish";
            this.btn_finish.Size = new System.Drawing.Size(360, 23);
            this.btn_finish.TabIndex = 19;
            this.btn_finish.Text = "I\'m Done";
            this.btn_finish.UseVisualStyleBackColor = true;
            this.btn_finish.Click += new System.EventHandler(this.btn_finish_Click);
            // 
            // btn_change_id
            // 
            this.btn_change_id.Enabled = false;
            this.btn_change_id.Location = new System.Drawing.Point(205, 92);
            this.btn_change_id.Name = "btn_change_id";
            this.btn_change_id.Size = new System.Drawing.Size(75, 23);
            this.btn_change_id.TabIndex = 18;
            this.btn_change_id.Text = "Change ID";
            this.btn_change_id.UseVisualStyleBackColor = true;
            this.btn_change_id.Click += new System.EventHandler(this.btn_change_id_Click);
            // 
            // lbl_info
            // 
            this.lbl_info.AutoSize = true;
            this.lbl_info.Location = new System.Drawing.Point(15, 71);
            this.lbl_info.Name = "lbl_info";
            this.lbl_info.Size = new System.Drawing.Size(46, 13);
            this.lbl_info.TabIndex = 17;
            this.lbl_info.Text = "New ID:";
            // 
            // btn_selectfolder
            // 
            this.btn_selectfolder.Location = new System.Drawing.Point(18, 92);
            this.btn_selectfolder.Name = "btn_selectfolder";
            this.btn_selectfolder.Size = new System.Drawing.Size(87, 23);
            this.btn_selectfolder.TabIndex = 16;
            this.btn_selectfolder.Text = "Select Folder";
            this.btn_selectfolder.UseVisualStyleBackColor = true;
            this.btn_selectfolder.Click += new System.EventHandler(this.btn_selectfolder_Click);
            // 
            // tx_new
            // 
            this.tx_new.Location = new System.Drawing.Point(78, 47);
            this.tx_new.Name = "tx_new";
            this.tx_new.Size = new System.Drawing.Size(100, 20);
            this.tx_new.TabIndex = 3;
            this.tx_new.TextChanged += new System.EventHandler(this.tx_new_TextChanged);
            // 
            // tx_old
            // 
            this.tx_old.Location = new System.Drawing.Point(78, 21);
            this.tx_old.Name = "tx_old";
            this.tx_old.Size = new System.Drawing.Size(100, 20);
            this.tx_old.TabIndex = 2;
            this.tx_old.TextChanged += new System.EventHandler(this.tx_old_TextChanged);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(15, 43);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(46, 13);
            this.Label2.TabIndex = 13;
            this.Label2.Text = "New ID:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(15, 18);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(40, 13);
            this.Label1.TabIndex = 12;
            this.Label1.Text = "Old ID:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 178);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.btn_pack);
            this.Controls.Add(this.btn_fpk);
            this.Controls.Add(this.btn_finish);
            this.Controls.Add(this.btn_change_id);
            this.Controls.Add(this.lbl_info);
            this.Controls.Add(this.btn_selectfolder);
            this.Controls.Add(this.tx_new);
            this.Controls.Add(this.tx_old);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "eFootball PES2021 Stadium ID Changer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox TextBox1;
        internal System.Windows.Forms.Button btn_pack;
        internal System.Windows.Forms.Button btn_fpk;
        internal System.Windows.Forms.Button btn_finish;
        internal System.Windows.Forms.Button btn_change_id;
        internal System.Windows.Forms.Label lbl_info;
        internal System.Windows.Forms.Button btn_selectfolder;
        internal System.Windows.Forms.TextBox tx_new;
        internal System.Windows.Forms.TextBox tx_old;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
    }
}

