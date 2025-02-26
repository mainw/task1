using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfSession1.Control
{
    /// <summary>
    /// Логика взаимодействия для WindowLocker.xaml
    /// </summary>
    public partial class WindowLocker : UserControl
    {
        private Timer _timer;
        private TimeSpan _remainingTime;
        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }
        public WindowLocker()
        {
            InitializeComponent();
            descriptionText.Text = Description;
        }
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(WindowLocker), new PropertyMetadata(string.Empty));
        public void LockWindow(TimeSpan lockDuration)
        {
            _remainingTime = lockDuration;
            LockLayer.Visibility = Visibility.Visible;
            CountdownText.Text = _remainingTime.ToString(@"hh\:mm\:ss");
            _timer = new Timer(1000);
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_remainingTime > TimeSpan.Zero)
            {
                _remainingTime = _remainingTime.Subtract(TimeSpan.FromSeconds(1));
                Dispatcher.Invoke(() =>
                {
                    CountdownText.Text = _remainingTime.ToString(@"hh\:mm\:ss");
                });
            }
            else
            {
                _timer.Stop();
                Dispatcher.Invoke(() =>
                {
                    LockLayer.Visibility = Visibility.Collapsed;
                });
            }
        }
    }
}
