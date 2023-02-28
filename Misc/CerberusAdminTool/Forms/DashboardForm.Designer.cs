namespace CerberusAdminTool.Forms {
    partial class DashboardForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.panel3 = new System.Windows.Forms.Panel();
            this.NumberOfAccountsLbl = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ProgressPanel = new System.Windows.Forms.Panel();
            this.ActivePlayersProgressBar = new CircularProgressBar.CircularProgressBar();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.updateTmr = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.LoggedInAccountsLbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.ProgressPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(64)))));
            this.panel3.Controls.Add(this.NumberOfAccountsLbl);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(12, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(258, 130);
            this.panel3.TabIndex = 5;
            // 
            // NumberOfAccountsLbl
            // 
            this.NumberOfAccountsLbl.AutoSize = true;
            this.NumberOfAccountsLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.NumberOfAccountsLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(146)))), ((int)(((byte)(249)))));
            this.NumberOfAccountsLbl.Location = new System.Drawing.Point(14, 49);
            this.NumberOfAccountsLbl.Name = "NumberOfAccountsLbl";
            this.NumberOfAccountsLbl.Size = new System.Drawing.Size(99, 32);
            this.NumberOfAccountsLbl.TabIndex = 1;
            this.NumberOfAccountsLbl.Text = "$1234";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(151)))), ((int)(((byte)(176)))));
            this.label4.Location = new System.Drawing.Point(14, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(156, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Total number of accounts";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(14, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Total Accounts";
            // 
            // ProgressPanel
            // 
            this.ProgressPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(64)))));
            this.ProgressPanel.Controls.Add(this.ActivePlayersProgressBar);
            this.ProgressPanel.Controls.Add(this.label7);
            this.ProgressPanel.Controls.Add(this.label5);
            this.ProgressPanel.Location = new System.Drawing.Point(12, 209);
            this.ProgressPanel.Name = "ProgressPanel";
            this.ProgressPanel.Size = new System.Drawing.Size(258, 266);
            this.ProgressPanel.TabIndex = 6;
            // 
            // ActivePlayersProgressBar
            // 
            this.ActivePlayersProgressBar.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner;
            this.ActivePlayersProgressBar.AnimationSpeed = 500;
            this.ActivePlayersProgressBar.BackColor = System.Drawing.Color.Transparent;
            this.ActivePlayersProgressBar.Font = new System.Drawing.Font("Yu Gothic", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ActivePlayersProgressBar.ForeColor = System.Drawing.Color.White;
            this.ActivePlayersProgressBar.InnerColor = System.Drawing.Color.Transparent;
            this.ActivePlayersProgressBar.InnerMargin = 2;
            this.ActivePlayersProgressBar.InnerWidth = -1;
            this.ActivePlayersProgressBar.Location = new System.Drawing.Point(49, 61);
            this.ActivePlayersProgressBar.MarqueeAnimationSpeed = 2000;
            this.ActivePlayersProgressBar.Name = "ActivePlayersProgressBar";
            this.ActivePlayersProgressBar.OuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.ActivePlayersProgressBar.OuterMargin = -25;
            this.ActivePlayersProgressBar.OuterWidth = 26;
            this.ActivePlayersProgressBar.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(249)))));
            this.ActivePlayersProgressBar.ProgressWidth = 15;
            this.ActivePlayersProgressBar.SecondaryFont = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ActivePlayersProgressBar.Size = new System.Drawing.Size(150, 150);
            this.ActivePlayersProgressBar.StartAngle = 270;
            this.ActivePlayersProgressBar.SubscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.ActivePlayersProgressBar.SubscriptMargin = new System.Windows.Forms.Padding(10, -35, 0, 0);
            this.ActivePlayersProgressBar.SubscriptText = "";
            this.ActivePlayersProgressBar.SuperscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.ActivePlayersProgressBar.SuperscriptMargin = new System.Windows.Forms.Padding(10, 35, 0, 0);
            this.ActivePlayersProgressBar.SuperscriptText = "";
            this.ActivePlayersProgressBar.TabIndex = 1;
            this.ActivePlayersProgressBar.Text = "68%";
            this.ActivePlayersProgressBar.TextMargin = new System.Windows.Forms.Padding(5, 8, 0, 0);
            this.ActivePlayersProgressBar.Value = 68;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(72, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 25);
            this.label7.TabIndex = 0;
            this.label7.Text = "Progress";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(151)))), ((int)(((byte)(176)))));
            this.label5.Location = new System.Drawing.Point(72, 226);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Progress details";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // updateTmr
            // 
            this.updateTmr.Enabled = true;
            this.updateTmr.Interval = 1000;
            this.updateTmr.Tick += new System.EventHandler(this.updateTmr_Tick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.LoggedInAccountsLbl);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Location = new System.Drawing.Point(285, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(258, 130);
            this.panel1.TabIndex = 5;
            // 
            // LoggedInAccountsLbl
            // 
            this.LoggedInAccountsLbl.AutoSize = true;
            this.LoggedInAccountsLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LoggedInAccountsLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(146)))), ((int)(((byte)(249)))));
            this.LoggedInAccountsLbl.Location = new System.Drawing.Point(14, 49);
            this.LoggedInAccountsLbl.Name = "LoggedInAccountsLbl";
            this.LoggedInAccountsLbl.Size = new System.Drawing.Size(99, 32);
            this.LoggedInAccountsLbl.TabIndex = 1;
            this.LoggedInAccountsLbl.Text = "$1234";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(151)))), ((int)(((byte)(176)))));
            this.label3.Location = new System.Drawing.Point(14, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Total number of accounts";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(14, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(189, 25);
            this.label6.TabIndex = 0;
            this.label6.Text = "Logged in Accounts";
            // 
            // DashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(738, 509);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.ProgressPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DashboardForm";
            this.Text = "DashboardForm";
            this.Load += new System.EventHandler(this.DashboardForm_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ProgressPanel.ResumeLayout(false);
            this.ProgressPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label NumberOfAccountsLbl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel ProgressPanel;
        private CircularProgressBar.CircularProgressBar ActivePlayersProgressBar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Timer updateTmr;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label LoggedInAccountsLbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
    }
}