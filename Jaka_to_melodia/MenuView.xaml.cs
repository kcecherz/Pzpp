using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Jaka_to_melodia
{
    public partial class MenuView : UserControl
    {
        private ProfileManager _profileManager;
        private List<Profile> _profiles;

        public MenuView()
        {
            InitializeComponent();

            _profileManager = new ProfileManager();
            RefreshProfilesList(); 
        }

        private void RefreshProfilesList()
        {
            _profiles = _profileManager.LoadProfiles();

            ListProfiles.ItemsSource = _profiles;
        }

        private void BtnAddProfile_Click(object sender, RoutedEventArgs e)
        {
            string newNick = TxtPlayerName.Text.Trim();

            if (string.IsNullOrWhiteSpace(newNick))
            {
                MessageBox.Show("Nick nie może być pusty!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ProfileManager pm = new ProfileManager();
            List<Profile> allProfiles = pm.LoadProfiles();

            bool nameExists = allProfiles.Any(p => p.Name.Equals(newNick, StringComparison.OrdinalIgnoreCase));

            if (nameExists)
            {
                MessageBox.Show("Gracz o takim nicku już istnieje! Wymyśl inną nazwę.", "Nick zajęty", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (ListProfiles.SelectedItem == null)
            {
                MessageBox.Show("Wybierz profil z listy, aby rozpocząć grę!", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            Profile selectedProfile = (Profile)ListProfiles.SelectedItem;

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.ChangeView(new LobbyView(selectedProfile));
        }
        private void BtnDeleteProfile_Click(object sender, RoutedEventArgs e)
        {
            if (ListProfiles.SelectedItem == null)
            {
                MessageBox.Show("Wybierz profil z listy, aby go usunąć!", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            Profile selectedPlayer = (Profile)ListProfiles.SelectedItem;

            MessageBoxResult result = MessageBox.Show(
                $"Czy na pewno chcesz usunąć profil gracza '{selectedPlayer.Name}'? Tej operacji nie można cofnąć.",
                "Potwierdzenie usunięcia",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                _profiles.Remove(selectedPlayer);

                _profileManager.SaveProfiles(_profiles);

                RefreshProfilesList();
            }
        }
        private void BtnEditProfile_Click(object sender, RoutedEventArgs e)
        {
            if (ListProfiles.SelectedItem == null)
            {
                MessageBox.Show("Wybierz profil z listy, który chcesz edytować!", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            string newName = TxtPlayerName.Text.Trim();

            if (string.IsNullOrEmpty(newName))
            {
                MessageBox.Show("Wpisz nową nazwę w polu tekstowym na górze ekranu, a następnie kliknij Edytuj!", "Brak nazwy", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Profile selectedPlayer = (Profile)ListProfiles.SelectedItem;

            bool nameExists = _profiles.Any(p => p.Id != selectedPlayer.Id && p.Name.Equals(newName, StringComparison.OrdinalIgnoreCase));

            if (nameExists)
            {
                MessageBox.Show("Inny gracz o takim nicku już istnieje! Wymyśl inną nazwę.", "Nick zajęty", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            selectedPlayer.Name = newName;

            _profileManager.SaveProfiles(_profiles);
            RefreshProfilesList();
            TxtPlayerName.Clear();

            MessageBox.Show("Nazwa profilu została zmieniona!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }


}