using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Jaka_to_melodia
{
    public partial class LobbyView : UserControl
    {
        private Profile _currentPlayer;

        public LobbyView(Profile player)
        {
            InitializeComponent();
            _currentPlayer = player;
            LoadData();
        }

        private void LoadData()
        {
            TxtWelcome.Text = $"Witaj, {_currentPlayer.Name}!";
            TxtHighscore.Text = $"Twój obecny rekord: {_currentPlayer.Highscore} pkt";

            ProfileManager pm = new ProfileManager();
            List<Profile> allProfiles = pm.LoadProfiles();

            var sortedProfiles = allProfiles.OrderByDescending(p => p.Highscore).ToList();

            ListLeaderboard.Items.Clear();
            int rank = 1;
            foreach (var profile in sortedProfiles)
            {
                ListLeaderboard.Items.Add($"{rank}. {profile.Name} - {profile.Highscore} pkt");
                rank++;
            }
        }

        private void BtnMusic_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFolderDialog()
            {
                Title = "Wybierz folder z muzyką"
            };

            if (dialog.ShowDialog() == true)
            {
                SettingsManager sm = new SettingsManager();

                AppSettings settings = new AppSettings { MusicFolderPath = dialog.FolderName };
                sm.SaveSettings(settings);

                MessageBox.Show("Folder z muzyką został zaktualizowany!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.ChangeView(new GameView(_currentPlayer));
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}