using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

            if (_songs.Count >= 4)
            {
                PlayRandomSong();
            }
            else
            {
                MessageBox.Show("Potrzebujesz minimum 4 piosenek w folderze, żeby zagrać!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.ChangeView(new MenuView());
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

            var wrongSongs = _songs.Where(s => s != _currentSong).OrderBy(x => _random.Next()).Take(3).ToList();
            var allChoices = new List<Song>(wrongSongs);
            allChoices.Add(_currentSong);
            var shuffledChoices = allChoices.OrderBy(x => _random.Next()).ToList();

            BtnAnswer1.Content = shuffledChoices[0].DisplayInfo;
            BtnAnswer2.Content = shuffledChoices[1].DisplayInfo;
            BtnAnswer3.Content = shuffledChoices[2].DisplayInfo;
            BtnAnswer4.Content = shuffledChoices[3].DisplayInfo;

            _timer.Restart();
        }

        private void BtnAnswer_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            string selectedAnswer = clickedButton.Content.ToString();

            _mediaPlayer.Stop();
            _timer.Stop();

            if (selectedAnswer == _currentSong.DisplayInfo)
            {
                int timeBonus = Math.Max(0, 10000 - (int)_timer.ElapsedMilliseconds);
                int points = 1000 + (timeBonus / 10);
                _currentScore += points;
                MessageBox.Show($"Dobrze!\nZdobywasz {points} punktów!", "Wynik");
            }
            else
            {
                MessageBox.Show($"Źle!\nPoprawna odpowiedź to: {_currentSong.DisplayInfo}", "Wynik");
            }

            if (_currentRound < MaxRounds)
            {
                PlayRandomSong();
            }
            else
            {
                EndGame();
            }
        }

        private void EndGame()
        {
            _mediaPlayer.Stop();
            _timer.Stop();

            if (_currentScore > _currentPlayer.Highscore)
            {
                MessageBox.Show($"KONIEC GRY!\nNowy rekord!\nTwój to {_currentScore} pkt!", "Gratulacje!");

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
            EndGame();
        }
    }
}
