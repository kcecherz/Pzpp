using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private Stopwatch _timer;
        private int _currentScore;

        private int _currentRound;
        private const int MaxRounds = 10;

        public GameView(Profile player)
        {
            InitializeComponent();
            _currentPlayer = player;
            _currentScore = 0;
            _currentRound = 0; 

            _mediaPlayer = new MediaPlayer();
            _mediaPlayer.Volume = 0.2;
            _random = new Random();
            _timer = new Stopwatch();

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

            _currentRound++;
            TxtPlayerName.Text = $"Gra: {_currentPlayer.Name} | Runda: {_currentRound}/{MaxRounds} | Wynik: {_currentScore}";

            _mediaPlayer.Open(new Uri(_currentSong.FilePath));
            _mediaPlayer.Play();

            
        }

        private void EndGame()
        {
            _mediaPlayer.Stop();
            _timer.Stop();

            if (_currentScore > _currentPlayer.Highscore)
            {
                MessageBox.Show($"KONIEC GRY!\nNowy rekord życiowy!\nTwój stary wynik to {_currentPlayer.Highscore}, a teraz zdobyłeś {_currentScore} pkt!", "Gratulacje!");

                _currentPlayer.Highscore = _currentScore;

                ProfileManager pm = new ProfileManager();
                List<Profile> allProfiles = pm.LoadProfiles();

                for (int i = 0; i < allProfiles.Count; i++)
                {
                    if (allProfiles[i].Id == _currentPlayer.Id)
                    {
                        allProfiles[i].Highscore = _currentScore;
                        break;
                    }
                }
                pm.SaveProfiles(allProfiles);
            }
            else
            {
                MessageBox.Show($"KONIEC GRY!\nZdobyłeś {_currentScore} pkt.\nTwój rekord to nadal {_currentPlayer.Highscore} pkt.", "Podsumowanie");
            }

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.ChangeView(new MenuView());
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {

            if (_currentRound < MaxRounds)
            {
                PlayRandomSong();
            }
            else
            {
                EndGame();
            }
        }
    }
    
}