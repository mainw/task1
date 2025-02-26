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
using WpfSession1.Db;

namespace WpfSession1.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductList.xaml
    /// </summary>
    public partial class ProductList : Page, INotifyPropertyChanged
    {
        private bool _sortAsc = true;
        public string TextSortBy
        {
            get
            {
                if (_sortAsc)
                    return "Сортировать по убыванию цены";
                return "Сортировать по возрастанию цены";
            }
        }
        public Visibility AdminAccess => App.AdminAccess;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void PageUpdate(object sender, EventArgs e)
        {
            UploadData();
        }
        public ProductList()
        {
            InitializeComponent();
            UploadFilter();
            SortByCostButton.Content = TextSortBy;
            App.pageUpdateEvent += PageUpdate;
        }
        private void UploadFilter()
        {
            var list = new List<string>() { "Все производители" };
            list.AddRange(App._context.Manufacturers.Select(p => p.ManufacturerName).ToList());
            FilterComboBox.ItemsSource = list;
            FilterComboBox.SelectedIndex = 0;
        }
        private bool SearchInString(string str, string target) => str.ToLower().Contains(target.ToLower());

        private void UploadData()
        {
            int total, result;
            var list = App._context.Products.ToList();
            total = list.Count;
            list = list.Where(p => FilterComboBox.SelectedIndex == 0 || p.Manufacturer != null && p.Manufacturer.ManufacturerName == FilterComboBox.SelectedItem.ToString()).ToList();
            list = list.Where(p => string.IsNullOrEmpty(SearchTextBox.Text)                     ||
                SearchInString(p.ProductManufacturerName,                   SearchTextBox.Text) ||
                SearchInString(p.ProductCost                .ToString(),    SearchTextBox.Text) ||
                SearchInString(p.ProductMeasurementUnitName,                SearchTextBox.Text) ||
                SearchInString(p.ProductArticleNumber,                      SearchTextBox.Text) ||
                SearchInString(p.ProductCategory,                           SearchTextBox.Text) ||
                SearchInString(p.ProductDescription,                        SearchTextBox.Text) ||
                SearchInString(p.ProductDiscountAmount?     .ToString(),    SearchTextBox.Text) ||
                SearchInString(p.ProductDiscountMaxPossible?.ToString(),    SearchTextBox.Text) ||
                SearchInString(p.ProductName,                               SearchTextBox.Text) ||
                SearchInString(p.ProductProviderName,                       SearchTextBox.Text) ||
                SearchInString(p.ProductQuantityInStock     .ToString(),    SearchTextBox.Text)
                ).ToList();
            result = list.Count;
            ProductListView.ItemsSource = _sortAsc ? list.OrderBy(p=>p.ProductCost) : list.OrderByDescending(p => p.ProductCost);
            StatisticTextBlock.Text = $"{result} из {total}";
        }

        private void EditProductButton_Click(object sender, RoutedEventArgs e)
        {
            if((sender as Button).DataContext is Product product)
            {
                (App.Current.MainWindow as MainWindow).MainFrame.Content = new Pages.AddEditProduct(product);
                UploadData();
            }
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            (App.Current.MainWindow as MainWindow).MainFrame.Content = new Pages.AddEditProduct();
            UploadData();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UploadData();
        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UploadData();
        }

        private void SortByCostButton_Click(object sender, RoutedEventArgs e)
        {
            _sortAsc = !_sortAsc;
            if(sender is Button btn)
                btn.Content = TextSortBy;
            UploadData();
        }

        private void DropProductButton_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).DataContext is Product product)
            {
                App._context.Products.Remove(product);
                App.SaveChanges();
                UploadData();
            }
        }
    }
}
