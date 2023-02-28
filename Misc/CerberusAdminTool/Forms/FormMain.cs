using CerberusAdminTool.Forms;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CerberusAdminTool {

    public partial class FormMain : Form {

        public FormMain() {
            InitializeComponent();

            NavPnl.Height = DashboardBtn.Height;
            NavPnl.Top = DashboardBtn.Top;
            NavPnl.Left = DashboardBtn.Left;
            DashboardBtn.BackColor = Color.FromArgb(46, 51, 73);
            OpenForm<DashboardForm>();
        }

        private void FormMain_Load(object sender, EventArgs e) {
        }

        #region Form functionalities

        private void ButtonClicked(object sender, EventArgs e) {
            Button? button = sender as Button;
            if (button == null) return;

            NavPnl.Height = button.Height;
            NavPnl.Top = button.Top;
            NavPnl.Left = button.Left;
            button.BackColor = Color.FromArgb(46, 51, 73);

            switch (button.Name) {
                case "DashboardBtn":
                    OpenForm<DashboardForm>();
                    PanelNameLabel.Text = "Dashboard";
                    break;

                case "AnalyticsBtn":
                    OpenForm<AnalyticsForm>();
                    PanelNameLabel.Text = "Analytics";
                    break;

                case "PlayerManagementBtn":
                    OpenForm<PlayerManagementForm>();
                    PanelNameLabel.Text = "Player Management";
                    break;

                case "SettingsBtn":
                    OpenForm<SettingsForm>();
                    PanelNameLabel.Text = "Settings";
                    break;

                default:
                    break;
            }
        }

        private void ButtonLeft(object sender, EventArgs e) {
            Button? button = sender as Button;
            if (button == null) return;
            button.BackColor = Color.FromArgb(24, 30, 54);
        }

        #endregion Form functionalities

        private void OpenForm<MyForm>() where MyForm : Form, new() {
            Form? newForm;

            newForm = FormsPanel.Controls.OfType<MyForm>().FirstOrDefault();//Busca en la colecion el formulario
            //si el formulario/instancia no existe
            if (newForm == null) {
                newForm = new MyForm();
                newForm.TopLevel = false;
                newForm.FormBorderStyle = FormBorderStyle.None;
                newForm.Dock = DockStyle.Fill;
                FormsPanel.Controls.Add(newForm);
                FormsPanel.Tag = newForm;
                newForm.Show();
                newForm.BringToFront();
            }
            //si el formulario/instancia existe
            else {
                newForm.BringToFront();
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e) {
            Application.Exit();
        }
    }
}