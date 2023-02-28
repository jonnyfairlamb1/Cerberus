using CerberusAdminTool.Data;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CerberusAdminTool.Forms {

    public partial class DashboardForm : Form {
        private DashboardData _dashboardData = new();

        public DashboardForm() {
            InitializeComponent();
        }

        private void DashboardForm_Load(object sender, EventArgs e) {
            //TODO: Getupdate from database on values
            UpdateTextValues();
            AllocConsole();
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AllocConsole();

        private void UpdateTextValues() {
            NumberOfAccountsLbl.Text = _dashboardData._totalNumberOfAccounts.ToString("N0");
            LoggedInAccountsLbl.Text = _dashboardData._totalNumberOfLoggedInPlayers.ToString("N0");
            ActivePlayersProgressBar.Text = _dashboardData._currentLoggedInPlayersProgress + "%";
            ActivePlayersProgressBar.Value = (int)_dashboardData._currentLoggedInPlayersProgress;
            //NumberOfAccountsLbl.Text = _dashboardData._totalNumberOfAccounts.ToString();
        }

        private void updateTmr_Tick(object sender, EventArgs e) {
            _dashboardData.UpdateValues();
            UpdateTextValues();
            updateTmr.Stop();
            updateTmr.Start();
        }
    }
}