using System;

namespace CerberusAdminTool.Data {

    public class DashboardData {
        public Int64 _totalNumberOfAccounts;
        public Int64 _totalNumberOfLoggedInPlayers;
        public Int64 _currentLoggedInPlayersProgress;

        public DashboardData() {
            Random random = new Random();
            _totalNumberOfAccounts = random.Next(0, 90000000);
        }

        public void UpdateValues() {
            Random random = new();
            _totalNumberOfLoggedInPlayers = random.Next(0, (int)_totalNumberOfAccounts);
            _currentLoggedInPlayersProgress = (Int64)(100f * _totalNumberOfLoggedInPlayers) / _totalNumberOfAccounts;
            Console.WriteLine(_currentLoggedInPlayersProgress);
        }
    }
}