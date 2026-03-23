using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Jaka_to_melodia
{
    public partial class SettingsView : UserControl
    {
        private SettingsManager _settingsManager;

        public SettingsView()
        {
            InitializeComponent();
            _settingsManager = new SettingsManager();

            LoadSavedPath();
        }

        private void LoadSavedPath()
        {
            AppSettings settings = _settingsManager.LoadSettings();

            if (!string.IsNullOrEmpty(settings.MusicFolderPath))
            {
                TxtFolderPath.Text = settings.MusicFolderPath;
            }
        }

        private void BtnScan_Click(object sender, RoutedEventArgs e)
        {
            string path = TxtFolderPath.Text.Trim();

            if (string.IsNullOrEmpty(path))
            {
                MessageBox.Show("Wpisz ścieżkę do folderu!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SongManager songManager = new SongManager();
            List<Song> loadedSongs = songManager.LoadSongsFromDirectory(path);

            if (loadedSongs.Count == 0)
            {
                MessageBox.Show("Nie znaleziono żadnych plików MP3 w tym folderze lub ścieżka jest niepoprawna.", "Brak muzyki", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                ListSongs.ItemsSource = loadedSongs;

                AppSettings currentSettings = new AppSettings { MusicFolderPath = path };
                _settingsManager.SaveSettings(currentSettings);

                MessageBox.Show($"Sukces! Znaleziono {loadedSongs.Count} utworów.", "Gotowe", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.ChangeView(new MenuView());
        }
    }
}