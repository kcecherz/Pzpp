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
            string newName = TxtPlayerName.Text.Trim();

            if (string.IsNullOrEmpty(newName))
            {
                MessageBox.Show("Nick nie może być pusty!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Profile newProfile = new Profile(newName);

            _profiles.Add(newProfile);
            _profileManager.SaveProfiles(_profiles);

            TxtPlayerName.Clear();
            RefreshProfilesList();
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (ListProfiles.SelectedItem == null)
            {
                MessageBox.Show("Wybierz profil z listy, aby zagrać!", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            Profile selectedPlayer = (Profile)ListProfiles.SelectedItem;

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.ChangeView(new GameView(selectedPlayer));
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
            selectedPlayer.Name = newName;

            _profileManager.SaveProfiles(_profiles);
            RefreshProfilesList();
            TxtPlayerName.Clear();

            MessageBox.Show("Nazwa profilu została zmieniona!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}