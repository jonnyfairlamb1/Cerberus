namespace CerberusAdminTool {
    partial class FormMain {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.PlayerManagementBtn = new System.Windows.Forms.Button();
            this.SettingsBtn = new System.Windows.Forms.Button();
            this.AnalyticsBtn = new System.Windows.Forms.Button();
            this.DashboardBtn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.NavPnl = new System.Windows.Forms.Panel();
            this.UserInfoLbl = new System.Windows.Forms.Label();
            this.UsernameLbl = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.PanelNameLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.CloseBtn = new System.Windows.Forms.Button();
            this.FormsPanel = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.panel1.Controls.Add(this.PlayerManagementBtn);
            this.panel1.Controls.Add(this.SettingsBtn);
            this.panel1.Controls.Add(this.AnalyticsBtn);
            this.panel1.Controls.Add(this.DashboardBtn);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(186, 577);
            this.panel1.TabIndex = 0;
            // 
            // PlayerManagementBtn
            // 
            this.PlayerManagementBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.PlayerManagementBtn.FlatAppearance.BorderSize = 0;
            this.PlayerManagementBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PlayerManagementBtn.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.PlayerManagementBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.PlayerManagementBtn.Image = ((System.Drawing.Image)(resources.GetObject("PlayerManagementBtn.Image")));
            this.PlayerManagementBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PlayerManagementBtn.Location = new System.Drawing.Point(0, 228);
            this.PlayerManagementBtn.Name = "PlayerManagementBtn";
            this.PlayerManagementBtn.Size = new System.Drawing.Size(186, 42);
            this.PlayerManagementBtn.TabIndex = 7;
            this.PlayerManagementBtn.Text = "Player Management";
            this.PlayerManagementBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.PlayerManagementBtn.UseVisualStyleBackColor = true;
            this.PlayerManagementBtn.Click += new System.EventHandler(this.ButtonClicked);
            this.PlayerManagementBtn.Leave += new System.EventHandler(this.ButtonLeft);
            // 
            // SettingsBtn
            // 
            this.SettingsBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.SettingsBtn.FlatAppearance.BorderSize = 0;
            this.SettingsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SettingsBtn.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SettingsBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.SettingsBtn.Image = ((System.Drawing.Image)(resources.GetObject("SettingsBtn.Image")));
            this.SettingsBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SettingsBtn.Location = new System.Drawing.Point(0, 535);
            this.SettingsBtn.Name = "SettingsBtn";
            this.SettingsBtn.Size = new System.Drawing.Size(186, 42);
            this.SettingsBtn.TabIndex = 6;
            this.SettingsBtn.Text = "Settings";
            this.SettingsBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SettingsBtn.UseVisualStyleBackColor = true;
            this.SettingsBtn.Click += new System.EventHandler(this.ButtonClicked);
            this.SettingsBtn.Leave += new System.EventHandler(this.ButtonLeft);
            // 
            // AnalyticsBtn
            // 
            this.AnalyticsBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.AnalyticsBtn.FlatAppearance.BorderSize = 0;
            this.AnalyticsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AnalyticsBtn.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.AnalyticsBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.AnalyticsBtn.Image = ((System.Drawing.Image)(resources.GetObject("AnalyticsBtn.Image")));
            this.AnalyticsBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AnalyticsBtn.Location = new System.Drawing.Point(0, 186);
            this.AnalyticsBtn.Name = "AnalyticsBtn";
            this.AnalyticsBtn.Size = new System.Drawing.Size(186, 42);
            this.AnalyticsBtn.TabIndex = 3;
            this.AnalyticsBtn.Text = "Analytics";
            this.AnalyticsBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AnalyticsBtn.UseVisualStyleBackColor = true;
            this.AnalyticsBtn.Click += new System.EventHandler(this.ButtonClicked);
            this.AnalyticsBtn.Leave += new System.EventHandler(this.ButtonLeft);
            // 
            // DashboardBtn
            // 
            this.DashboardBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.DashboardBtn.FlatAppearance.BorderSize = 0;
            this.DashboardBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DashboardBtn.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.DashboardBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.DashboardBtn.Image = ((System.Drawing.Image)(resources.GetObject("DashboardBtn.Image")));
            this.DashboardBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DashboardBtn.Location = new System.Drawing.Point(0, 144);
            this.DashboardBtn.Name = "DashboardBtn";
            this.DashboardBtn.Size = new System.Drawing.Size(186, 42);
            this.DashboardBtn.TabIndex = 2;
            this.DashboardBtn.Text = "Dashboard";
            this.DashboardBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.DashboardBtn.UseVisualStyleBackColor = true;
            this.DashboardBtn.Click += new System.EventHandler(this.ButtonClicked);
            this.DashboardBtn.Leave += new System.EventHandler(this.ButtonLeft);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.NavPnl);
            this.panel2.Controls.Add(this.UserInfoLbl);
            this.panel2.Controls.Add(this.UsernameLbl);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(186, 144);
            this.panel2.TabIndex = 1;
            // 
            // NavPnl
            // 
            this.NavPnl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.NavPnl.Location = new System.Drawing.Point(0, 193);
            this.NavPnl.Name = "NavPnl";
            this.NavPnl.Size = new System.Drawing.Size(3, 100);
            this.NavPnl.TabIndex = 3;
            // 
            // UserInfoLbl
            // 
            this.UserInfoLbl.AutoSize = true;
            this.UserInfoLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.UserInfoLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(161)))), ((int)(((byte)(178)))));
            this.UserInfoLbl.Location = new System.Drawing.Point(50, 105);
            this.UserInfoLbl.Name = "UserInfoLbl";
            this.UserInfoLbl.Size = new System.Drawing.Size(74, 12);
            this.UserInfoLbl.TabIndex = 2;
            this.UserInfoLbl.Text = "User info text";
            // 
            // UsernameLbl
            // 
            this.UsernameLbl.AutoSize = true;
            this.UsernameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.UsernameLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(156)))), ((int)(((byte)(149)))));
            this.UsernameLbl.Location = new System.Drawing.Point(50, 89);
            this.UsernameLbl.Name = "UsernameLbl";
            this.UsernameLbl.Size = new System.Drawing.Size(78, 16);
            this.UsernameLbl.TabIndex = 1;
            this.UsernameLbl.Text = "Username";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(60, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(63, 63);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // PanelNameLabel
            // 
            this.PanelNameLabel.AutoSize = true;
            this.PanelNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.PanelNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(161)))), ((int)(((byte)(176)))));
            this.PanelNameLabel.Location = new System.Drawing.Point(201, 9);
            this.PanelNameLabel.Name = "PanelNameLabel";
            this.PanelNameLabel.Size = new System.Drawing.Size(162, 32);
            this.PanelNameLabel.TabIndex = 1;
            this.PanelNameLabel.Text = "Dashboard";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(79)))), ((int)(((byte)(99)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox1.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.textBox1.Location = new System.Drawing.Point(582, 21);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(292, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "Search for something";
            // 
            // CloseBtn
            // 
            this.CloseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseBtn.FlatAppearance.BorderSize = 0;
            this.CloseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseBtn.Image = ((System.Drawing.Image)(resources.GetObject("CloseBtn.Image")));
            this.CloseBtn.Location = new System.Drawing.Point(907, 9);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(32, 32);
            this.CloseBtn.TabIndex = 3;
            this.CloseBtn.UseVisualStyleBackColor = true;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // FormsPanel
            // 
            this.FormsPanel.BackColor = System.Drawing.Color.White;
            this.FormsPanel.Location = new System.Drawing.Point(201, 47);
            this.FormsPanel.Name = "FormsPanel";
            this.FormsPanel.Size = new System.Drawing.Size(738, 509);
            this.FormsPanel.TabIndex = 5;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(951, 577);
            this.Controls.Add(this.FormsPanel);
            this.Controls.Add(this.CloseBtn);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.PanelNameLabel);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button SettingsBtn;
        private System.Windows.Forms.Button AnalyticsBtn;
        private System.Windows.Forms.Button DashboardBtn;
        private System.Windows.Forms.Panel NavPnl;
        private System.Windows.Forms.Label UserInfoLbl;
        private System.Windows.Forms.Label UsernameLbl;
        private System.Windows.Forms.Label PanelNameLabel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button CloseBtn;
        private System.Windows.Forms.Panel FormsPanel;
        private System.Windows.Forms.Button PlayerManagementBtn;
    }
}
