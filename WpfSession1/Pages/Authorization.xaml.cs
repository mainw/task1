using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using WpfAutovokzalDb.Windows;
using WpfSession1.Db;

namespace WpfSession1.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        private bool EnableCaptcha = false;
        public Authorization()
        {
            InitializeComponent();
        }
        private void NavigateToWorkSpace()
        {
            (App.Current.MainWindow as MainWindow).MainFrame.Navigate(new Pages.ProductList());
        }

        private void authorizationButton_Click(object sender, RoutedEventArgs e)
        {
            if(EnableCaptcha)
            {
                if(!new InputWindow(4))
                {
                    MessageBox.Show("Неверный ввод капчи, интерфейс будет заблокирован на 10 секунд");
                    (App.Current.MainWindow as MainWindow).mainWindowLocker.LockWindow(TimeSpan.FromSeconds(10));
                    return;
                }
                EnableCaptcha = false;
                MessageBox.Show("Капча успешно пройдена");
            }
            App._user = App._context.Users.FirstOrDefault(p=>p.UserLogin == loginTextBox.Text && p.UserPassword == passwordPassBox.Password);
            if (App._user != null)
            {
                NavigateToWorkSpace();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
                EnableCaptcha = true;
            }
        }

        private void ghostButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToWorkSpace();
        }
    }
}
