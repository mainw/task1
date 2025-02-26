using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
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
using WpfSession1.Db;
using WpfSession1.Utils;

namespace WpfSession1.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditProduct.xaml
    /// </summary>
    public partial class AddEditProduct : Page
    {
        private Product _product;
        public AddEditProduct()
        {
            InitializeComponent();
            Title = "Добавление товара";
            productCategoryComboBox.ItemsSource = App._context.Categories.ToList() ;
            _product = new Product();
        }
        public AddEditProduct(Product product)
        {
            InitializeComponent();
            Title = "Редактирование товара";
                productCategoryComboBox.ItemsSource = App._context.Categories.ToList();
            _product = product;
            LoadData(_product);
            App.pageUpdateEvent += PageUpdate;
        }

        private void PageUpdate(object sender, EventArgs e)
        {
            LoadData(_product);
        }

        private void LoadData(Product product)
        {
            productNameTextBox.Text = product.ProductName;
            productCategoryComboBox.SelectedIndex = productCategoryComboBox.Items.IndexOf(product.Category);
            productQuantityTextBox.Text = product.ProductQuantityInStock.ToString();
            productMeasureTextBox.Text = product.ProductMeasurementUnitName;
            productProviderTextBox.Text = product.ProductProviderName;
            productCostTextBox.Text = product.ProductCost.ToString();
            productDescriptionTextBox.Text = product.ProductDescription;
            if (product.ProductPhoto is byte[] array)
            {
                using (MemoryStream ms = new MemoryStream(array))
                {
                    ms.Position = 0;
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.StreamSource = ms;
                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.EndInit();
                    productImage.Source = bi;
                }
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            string name = productNameTextBox.Text;
            string category = productCategoryComboBox.Text;
            string quantity = productQuantityTextBox.Text;
            string measure = productMeasureTextBox.Text;
            string provider = productProviderTextBox.Text;
            string cost = productCostTextBox.Text;
            string description = productDescriptionTextBox.Text;

            if( string.IsNullOrEmpty(name)      ||
                string.IsNullOrEmpty(category)  ||
                string.IsNullOrEmpty(quantity)  ||
                string.IsNullOrEmpty(measure)   ||
                string.IsNullOrEmpty(provider)  ||
                string.IsNullOrEmpty(cost)      ||
                string.IsNullOrEmpty(description)
                )
            {
                MessageBox.Show("Заполните все поля");
                return;
            }
            if(int.TryParse(quantity, out int quantityValue))
            {
                if( quantityValue < 0)
                {
                    MessageBox.Show("Кол-во на складе не может быть меньше нуля");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Некорректный ввод");
                return;
            }
            if (decimal.TryParse(cost, out decimal costValue))
            {
                if (costValue <= 0)
                {
                    MessageBox.Show("Стоимость должна быть больше нуля");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Некорректный ввод");
                return;
            }
            SaveData(name, category, quantityValue, measure, provider, costValue, description);
            (App.Current.MainWindow as MainWindow).MainFrame.GoBack();
        }
        private void SaveData(string name, string category, int quantity, string measure, string provider, decimal cost,  string description)
        {
            _product.ProductName = name;
            _product.ProductCategory = category;
            _product.ProductQuantityInStock = quantity;
            if (!App._context.MeasurementUnits.Any(p => p.MeasurementUnitName == measure))
                App._context.MeasurementUnits.Add(new MeasurementUnit() { MeasurementUnitName = measure });
            _product.ProductMeasurementUnitName = measure;
            if (!App._context.Providers.Any(p => p.ProviderName == provider))
                App._context.Providers.Add(new Provider() { ProviderName = provider });
            _product.ProductProviderName = provider;
            _product.ProductCost = cost;
            _product.ProductDescription = description;

            if (string.IsNullOrEmpty(_product.ProductArticleNumber))
            {
                string str = App._context.Products.ToList().LastOrDefault().ProductArticleNumber;
                _product.ProductArticleNumber = StringIdendex.AddBitToString(str);
                App._context.Products.Add(_product);
            }
            App.SaveChanges();
        }

        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Изображения (*.jpg;*.jpeg;*.png;)|*.jpg;*.jpeg;*.png;"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;
                _product.ProductPhoto = File.ReadAllBytes(imagePath);
                LoadData(_product);
            }
        }

        private void DeleteImageButton_Click(object sender, RoutedEventArgs e)
        {
            _product.ProductPhoto = null;
            LoadData(_product);
        }
    }
}
