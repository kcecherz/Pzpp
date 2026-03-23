using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media; 

namespace Jaka_to_melodia
{
    public partial class GameView : UserControl
    {
        private Profile _currentPlayer;

        private MediaPlayer _mediaPlayer;
        private List<Song> _songs;
        private Random _random;
        private Song _currentSong; 

        public GameView(Profile player)
        {
            InitializeComponent();
            _currentPlayer = player;
            TxtPlayerName.Text = "Gra: " + _currentPlayer.Name;

            _mediaPlayer = new MediaPlayer();
            _mediaPlayer.Volume = 0.2; 
            _random = new Random();

            LoadAndPlayMusic();
        }

        private void LoadAndPlayMusic()
        {
            SettingsManager settingsManager = new SettingsManager();
            AppSettings settings = settingsManager.LoadSettings();

            if (string.IsNullOrEmpty(settings.MusicFolderPath))
            {
                MessageBox.Show("Najpierw ustaw folder z muzyką w ustawieniach!", "Brak muzyki", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SongManager songManager = new SongManager();
            _songs = songManager.LoadSongsFromDirectory(settings.MusicFolderPath);

            if (_songs.Count > 0)
            {
                PlayRandomSong();
            }
            else
            {
                MessageBox.Show("Nie znaleziono piosenek we wskazanym folderze. Sprawdź ustawienia.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PlayRandomSong()
        {
            int index = _random.Next(_songs.Count);
            _currentSong = _songs[index];

            _mediaPlayer.Open(new Uri(_currentSong.FilePath));
            _mediaPlayer.Play();

            
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            
            _mediaPlayer.Stop();

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.ChangeView(new MenuView());
        }
        private void BtnAnswer_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}