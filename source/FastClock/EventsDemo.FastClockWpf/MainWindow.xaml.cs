using System;
using System.Windows;

namespace EventsDemo.FastClockWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Initialized(object sender, EventArgs e)
        {
            DatePickerDate.SelectedDate = DateTime.Today;
            TextBoxTime.Text = DateTime.Now.ToShortTimeString();
            FastClock.FastClock clock = FastClock.FastClock.GetInstance();
            clock.Time = DateTime.Now;
            clock.OneMinuteIsOver += FastClockOneMinuteIsOver;
        }

        private void ButtonSetTime_Click(object sender, RoutedEventArgs e)
        {
            SetFastClockStartDateAndTime();
        }

        private void SetFastClockStartDateAndTime()
        {
            string[] timeText = TextBoxTime.Text.Split(':');
            FastClock.FastClock clock = FastClock.FastClock.GetInstance();

            DateTime input = new DateTime(DatePickerDate.SelectedDate.Value.Year, DatePickerDate.SelectedDate.Value.Month, DatePickerDate.SelectedDate.Value.Day, Convert.ToInt32(timeText[0]), Convert.ToInt32(timeText[1]), 0);
            clock.Time = input;
            TextBlockTime.Text = input.ToShortTimeString();
        }

        private void FastClockOneMinuteIsOver(object sender, DateTime fastClockTime)
        {
            TextBlockTime.Text = fastClockTime.ToShortTimeString();
        }

        private void CheckBoxClockRuns_Click(object sender, RoutedEventArgs e)
        {
            FastClock.FastClock clock = FastClock.FastClock.GetInstance();
            clock.Factor = SliderFactor.Value;
            clock.IsRunning = CheckBoxClockRuns.IsChecked == true;
        }

        private void ButtonCreateView_Click(object sender, RoutedEventArgs e)
        {
            DigitalClock digitalClock = new DigitalClock();
            digitalClock.Owner = this;
            digitalClock.Show();
        }

        private void SliderFactor_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            FastClock.FastClock.GetInstance().Factor = SliderFactor.Value;
        }
    }
}
