using SF2022UserANLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WpfSession1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public string User => App._user?.UserFIO ?? "Гость";
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void PageUpdate(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(User));
        }
        public MainWindow()
        {
            InitializeComponent();
            App.pageUpdateEvent += PageUpdate;
            MainFrame.Navigated += (s, e) =>
            {
                OnPropertyChanged(nameof(User));
            };
            MainFrame.Navigate(new Pages.Authorization());
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if(MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
                if (!MainFrame.CanGoBack)
                    App._user = null;
                OnPropertyChanged(nameof(User));
                App.ResetChanges();
                App.UpdatePage();
            }
        }
    }
}
