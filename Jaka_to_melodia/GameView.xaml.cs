using System.Windows;
using System.Windows.Controls;

namespace Jaka_to_melodia
{
    public partial class GameView : UserControl
    {
        private Profile _currentPlayer;

        public GameView(Profile player)
        {
            InitializeComponent();

            _currentPlayer = player;

            TxtPlayerName.Text = _currentPlayer.Name;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.ChangeView(new MenuView());
        }
    }
}