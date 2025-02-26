using System;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WpfSession1.Utils;
namespace WpfAutovokzalDb.Windows
{
    /// <summary>
    /// Логика взаимодействия для InputWindow.xaml
    /// </summary>
    public partial class InputWindow : Window
    {
        private string _captchaString;
        public InputWindow()
        {
            InitializeComponent();
        }
        public InputWindow(string str)
        {
            InitializeComponent();
            _captchaString = str;
            UploadCaptcha();
        }
        public InputWindow(int length)
        {
            InitializeComponent();
            _captchaString = CaptchaCreater.GetRandomText(length);
            UploadCaptcha();
        }
        private void UploadCaptcha()
        {
            responceTextBox.MaxLength = _captchaString.Length;
            using (MemoryStream ms = new MemoryStream())
            {
                CaptchaCreater.GetSimpleCaptcha(_captchaString).Save(ms, ImageFormat.Bmp);
                ms.Position = 0;
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = ms;
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.EndInit();
                img.Source = bi;
            }
        }
        public string ResponceValue
        {
            get { return responceTextBox.Text; }
        }
        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if(responceTextBox.Text.Length == responceTextBox.MaxLength)
            {
                DialogResult = true;
            }
            else
            {
                MessageBox.Show($"Должно быть {responceTextBox.MaxLength} символов");
            }
        }
        public static implicit operator string(InputWindow value)
        {
            if(value.ShowDialog() == true)
            return value.ResponceValue;
            return null;
        }
        public static implicit operator bool(InputWindow value)
        {
            if (value.ShowDialog() == true)
                return value._captchaString.ToLower() == value.ResponceValue.ToLower();
            return false;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }
    }
}
