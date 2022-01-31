using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using BTDB2_SIM;

namespace BTDB2_Client.ViewModels
{
    class GameViewModel : INotifyPropertyChanged
    {
        #region Singleton
        //padlock implementation from https://csharpindepth.com/articles/singleton#lock
        //for more details on the choice read https://csharpindepth.com/articles/singleton#exceptions
        private static GameViewModel _instance = null;
        private static readonly object padlock = new object();

        public static GameViewModel Instance
        {
            get
            {
                lock (padlock)
                    if (_instance == null)
                        _instance = new GameViewModel();
                return _instance;
            }
        }
        #endregion

        private Game game { get; set; }

        private bool isGameOngoing { get; set; }
        public bool IsGameOngoing
        {
            get => isGameOngoing;
            set
            {
                if (value == isGameOngoing) return;
                isGameOngoing = value;
                OnPropertyChanged();
            }
        }
        private string _gameTime;

        public string GameTime
        {
            get => _gameTime;
            set
            {
                //if (value == _gameTime) return;
                _gameTime = value;
                OnPropertyChanged();
            }
        }
        private double _cash;
        public double Cash
        {
            get => _cash;
            set
            {
                if (value == _cash) return;
                _cash = value;
                OnPropertyChanged();
            }
        }

        private double _eco;
        public double Eco
        {
            get => _eco;
            set
            {
                if (value == _eco) return;
                _eco = value;
                OnPropertyChanged();
            }
        }

        public Utility.DelegateCommand<object> StartGameCommand { get; }
        public Utility.DelegateCommand<object> PauseGameCommand { get; }
        public Utility.DelegateCommand<object> ResetGameCommand { get; }

        public GameViewModel()
        {

            game = new Game();
            IsGameOngoing = false;

            Cash = game.Cash;
            Eco = game.Eco;

            game.CashUpdated += Game_CashUpdated;

            System.Windows.Media.CompositionTarget.Rendering += DrawEveryFrameEvent;

            #region Commands

            StartGameCommand = new Utility.DelegateCommand<object>(
                context =>
                {
                    new System.Threading.Thread(() =>
                    {
                        game.Start();
                    }).Start();

                    IsGameOngoing = true;
                }, e => !IsGameOngoing);

            PauseGameCommand = new Utility.DelegateCommand<object>(
                context =>
                {
                    game.Pause();
                    IsGameOngoing = false;
                }, e => IsGameOngoing);

            ResetGameCommand = new Utility.DelegateCommand<object>(
                context =>
                {
                    game.Reset();
                    IsGameOngoing = false;
                }, e => true);
            #endregion
        }
        private void DrawEveryFrameEvent(object sender, EventArgs e)
        {
            GameTime = game.ElapsedGameTime().ToString(@"mm\:ss\:fff");
        }

        private void Game_CashUpdated()
        {
            Eco = game.Eco;
            Cash = game.Cash;
        }

        #region ProprietyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            switch (propertyName)
            {
                case nameof(IsGameOngoing):
                    PauseGameCommand.RaiseCanExecuteChanged();
                    StartGameCommand.RaiseCanExecuteChanged();
                    break;
                default:
                    break;
            }

        }
        #endregion
    }
}
