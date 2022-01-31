using System.Windows;

namespace BTDB2_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Views.GameView GameView;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStartGame(object sender, RoutedEventArgs e)
        {
            GameFrame.Content = GameView ?? (GameView = new Views.GameView());
        }
    }
}
