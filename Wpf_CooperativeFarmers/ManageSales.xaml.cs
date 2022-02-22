using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Wpf_CooperativeFarmers.Classes;
using Wpf_CooperativeFarmers.MyResources;

namespace Wpf_CooperativeFarmers
{
    /// <summary>
    /// Interaction logic for ManageSales.xaml
    /// </summary>
    public partial class ManageSales : Window
    {
        public ManageSales()
        {
            InitializeComponent();
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            getCurrentStock();
          

            Lbx_products.ItemsSource = App._products;
            Lbx_storage.ItemsSource = App._sales;
            Cbx_languages.ItemsSource = Resource.languages.Split(",");


        }

        private void getCurrentStock()
        {
            var salesGroup = App._sales.GroupBy(x => x.productId)
               .Select(t => new
               {
                   ProductId = t.Key,
                   Count = t.Count(),
                   Quantity = t.Sum(ta => ta.quantity),
               }).ToList();

            var productQuantity = App._deliveries.GroupBy(x => x.productId)
        .Select(t => new
        {
            ProductId = t.Key,
            Count = t.Count(),
            Quantity = t.Sum(ta => ta.quantity),
        }).Select(diff => new
        {
            ProductId = diff.ProductId,
            Quantity = diff.Quantity - (salesGroup.FirstOrDefault(sg => sg.ProductId == diff.ProductId) != null ? salesGroup.FirstOrDefault(sg => sg.ProductId == diff.ProductId)?.Quantity : 0),
        });

            productQuantity.ToList().ForEach(x =>
                App._products.FirstOrDefault(p => p.id == x.ProductId)
                                                       .currentQuantity = x.Quantity);
        }

        private void Btn_deleteStorage_Click(object sender, RoutedEventArgs e)
        {
            var itm = Lbx_storage.SelectedItem as Sales;
            if (itm == null)
            {
                MessageBox.Show("Please select a record to delete!", "Error");
                return;
            }
            App._sales.Remove(itm);

        }



        private void Grd_Menu_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {

            ThicknessAnimation ta = new ThicknessAnimation();
            ta.To = new Thickness(10, 37, 0, 0);
            Grd_Menu.BeginAnimation(Grid.MarginProperty, ta);
        }

        private void Grd_Menu_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ThicknessAnimation ta = new ThicknessAnimation();
            ta.To = new Thickness(-200, 50, 0, 0);
            Grd_Menu.BeginAnimation(Grid.MarginProperty, ta);
        }

        private void AnimateTo(int to, bool isWidth)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.To = to;
            Grd_Menu.BeginAnimation(isWidth ? Grid.WidthProperty : Grid.HeightProperty, da);
        }

        private void Tbk_manageStorageMenuItem_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }

        private void Tbk_manageFarmerMenuItem_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ManageFarmers manageFarmers = new ManageFarmers();
            manageFarmers.Show();

            this.Close();


        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MyStorage.WriteXml<ObservableCollection<Sales>>(App._sales, "Sales.xml");
            MyStorage.WriteXml<ObservableCollection<Product>>(App._products, "Products.xml");

        }
        private void Tbx_productFilter_TextChanged(object sender, TextChangedEventArgs e)
        {


            var filter = Tbx_productFilter.Text.ToLower();

            var lst = from m in App._products
                      where m.name.ToLower().Contains(filter)
                      select m;

            Lbx_products.ItemsSource = lst;

        }

        private void Btn_productClearSelection_Click(object sender, RoutedEventArgs e)
        {
            Lbx_products.SelectedIndex = -1;

        }
        private void Cbx_languages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lbx = sender as ComboBox;

            var lang = lbx.SelectedItem as string;

            var langSplit = lang.Split(':');

            MySettings.Default.language = langSplit[0];
            MySettings.Default.Save();

            MessageBox.Show($"Restart app to apply {langSplit[1]} language");
        }

        private void Btn_addSales_Click(object sender, RoutedEventArgs e)
        {

            var product = (Product)Lbx_products.SelectedItem;
            Sales sales = new Sales
            {
                productId = product.id,
                quantity = 0,
                sellingDate = DateTime.Now,
                price = 0
            };
            App._sales.Add(sales);
            getCurrentStock();
            Lbx_storage.SelectedItem = sales;
            Lbx_storage.ScrollIntoView(sales);
        }

        private void Tbk_manageSalesMenuItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ManageSales mainWindow = new ManageSales();
            mainWindow.Show();

            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            getCurrentStock();
            Lbx_products.ItemsSource = App._products;
        }
        private void Tbk_manageSalesReportMenuItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SalesReport mainWindow = new SalesReport();
            mainWindow.Show();

            this.Close();

        }
    }
}
