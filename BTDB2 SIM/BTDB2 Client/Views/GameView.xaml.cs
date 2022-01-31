using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BTDB2_Client.Views
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : Page
    {
        public GameView()
        {
            InitializeComponent();
            DataContext = ViewModels.GameViewModel.Instance;
        }

        private void triggerResetFunctionality(object sender, MouseButtonEventArgs e)
        {
            // right clicking the reset game once will disable the button permanently to prevent missclicks
            ResetGameButton.IsEnabled = false;
        }
    }
}
