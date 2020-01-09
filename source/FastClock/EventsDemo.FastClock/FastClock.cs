using System;
using System.Windows.Threading;

namespace EventsDemo.FastClock
{
    public class FastClock
    {
        private readonly DispatcherTimer _timer;

        public event EventHandler<DateTime> OneMinuteIsOver;
        public double Factor 
        {
            get
            {
                return _factor;
            }

            set
            {
                _factor = value;
                _timer.Interval = TimeSpan.FromMilliseconds(1000 / value);
            }
        }

        private static FastClock _instance = null;

        public static FastClock GetInstance()
        {
            if (_instance == null)
            {
                _instance =  new FastClock();
            }
            return _instance;
        }

        private bool _isRunning;
        private double _factor = 1;

        public bool IsRunning 
        {
            get => _isRunning;

            set
            {
                if (Factor > 0)
                {
                    _timer.Interval = TimeSpan.FromMilliseconds(1000 / Factor);
                }

                if (!_isRunning && value)
                {
                    _timer.Start();
                }
                else if(_isRunning && !value)
                {
                    _timer.Stop();
                }

                _isRunning = value;
            }
        }

        public DateTime Time { get; set; }

        private FastClock()
        {
            _timer = new DispatcherTimer();
            _timer.Tick += OnTimerTick;
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            Time = Time.AddMinutes(1);
            OnOneMinuteIsOver(Time);
        }

        protected virtual void OnOneMinuteIsOver(DateTime time)
        {
            OneMinuteIsOver?.Invoke(this, time);
        }
    }
}
