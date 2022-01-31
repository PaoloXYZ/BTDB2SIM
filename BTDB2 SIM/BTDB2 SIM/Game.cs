using System;

namespace BTDB2_SIM
{
    public class Game
    {
        public Game()
        {
            gameTime = new System.Diagnostics.Stopwatch();
            ecoTimer = new System.Timers.Timer(6000);

            Cash = 1000;
            Eco = 250;

            ecoTimer.Elapsed += EcoTimer_Elapsed;
            ecoTimer.AutoReset = true;
        }
        
        public Game(double cash, double eco) : this()
        {
            Cash = cash;
            Eco = eco;
        }

        public delegate void CashUpdate();

        public event CashUpdate CashUpdated;

        private void EcoTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Cash += Eco;
            CashUpdated?.Invoke();
            //suboptimal solution
            ecoTimer.Interval = 6000;
        }

        private System.Diagnostics.Stopwatch gameTime { get; set; }
        private System.Timers.Timer ecoTimer { get; set; }
        public double Eco { get; protected set; }
        //questinable name
        public double Cash { get; protected set; }
        public void Start()
        {
            gameTime.Start();
            ecoTimer.Start();
        }
        public void Pause()
        {
            gameTime.Stop();
            ecoTimer.Stop();
            ecoTimer.Interval = 6000 - (gameTime.ElapsedMilliseconds % 6000);
        }
        public void Reset()
        {
            gameTime.Reset();
            ecoTimer.Stop();
        }
        public TimeSpan ElapsedGameTime()
        {
            return gameTime.Elapsed;
        }
    }
}
