using Microsoft.Win32;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_addStorage_Click(object sender, RoutedEventArgs e)
        {
            var farmer = (Farmer)Lbx_farmers.SelectedItem;
            var product = (Product)Lbx_products.SelectedItem;

            if(product==null)
            {
                Product newProduct = new Product
                {
                    id = ++MySettings.Default.ProductId,
                    name = Tbx_Product.Text,
                    measuringUnit = Cbx_measutingUnit.Text,
                    currentQuantity=0,
                };
                App._products.Add(newProduct);
                MySettings.Default.Save();

                Lbx_products.SelectedItem = newProduct;
                Lbx_products.ScrollIntoView(newProduct);
            }

            if (farmer != null)
            {
                Delivery delivery = new Delivery
                {
                    farmerId = farmer.id,
                    productId = product != null ? product.id : MySettings.Default.ProductId,
                    cost = 0,
                    quantity = 0,
                    deliveryDate = DateTime.Now,
                    price = 0
                };


                App._deliveries.Add(delivery);

                Lbx_storage.ItemsSource = App._deliveries.Where(x => x.farmerId == farmer.id).ToList();

                Lbx_storage.SelectedItem = delivery;
                Lbx_storage.ScrollIntoView(delivery);

            }
            else
            {
                if(product==null)
                {

                    MessageBox.Show("New Product Added! \n Please select a Farmer to complete delivery.", "Info");
                    return;
                }

                MessageBox.Show("Please select a Farmer to complete delivery!", "Error");
                return;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Lbx_farmers.ItemsSource = App._farmers;
            Lbx_products.ItemsSource = App._products;          
            Cbx_measutingUnit.ItemsSource = MySettings.Default.MeasuringUnits.Split(',');
            Cbx_languages.ItemsSource = Resource.languages.Split(",");


        }
        private void Btn_deleteStorage_Click(object sender, RoutedEventArgs e)
        {
            var itm = Lbx_storage.SelectedItem as Delivery;
            if (itm == null)
            {
                MessageBox.Show("Please select a record to delete!", "Error");
                return;
            }
            App._deliveries.Remove(itm);
            Lbx_storage.ItemsSource = App._deliveries.Where(x => x.farmerId == itm.farmerId).ToList();
        }


        private void Lbx_farmers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;

            var farmer = listBox.SelectedItem as Farmer;
            if (farmer != null)
            {
                Lbx_storage.ItemsSource = App._deliveries.Where(x => x.farmerId == farmer.id).ToList().OrderByDescending(o=>o.deliveryDate);

                var delivery= App._deliveries.Where(x => x.farmerId == farmer.id).ToList().OrderByDescending(o => o.deliveryDate).FirstOrDefault();

                Lbx_storage.SelectedItem = delivery;
                Lbx_storage.ScrollIntoView(delivery);

            }

        }

        private void Grd_Menu_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
          
            ThicknessAnimation ta=new ThicknessAnimation();
            ta.To = new Thickness(10, 37, 0,0);
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




        private void Tbx_farmerFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filter = Tbx_farmerFilter.Text.ToLower();

            var lst = from m in App._farmers
                      where
                      m.firstName.ToLower().Contains(filter)
                      ||
                      m.lastName.ToLower().Contains(filter)
                      select m;

            Lbx_farmers.ItemsSource = lst;

        }

      

        private void Tbk_manageFarmerMenuItem_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ManageFarmers manageFarmers = new ManageFarmers();
            manageFarmers.Show();

            this.Close();


        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MyStorage.WriteXml<ObservableCollection<Delivery>>(App._deliveries, "Delivery.xml");
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

        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {

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

        private void Tbk_manageSalesMenuItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ManageSales mainWindow = new ManageSales();
            mainWindow.Show();

            this.Close();
        }

        private void Tbk_manageSalesReportMenuItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SalesReport mainWindow = new SalesReport();
            mainWindow.Show();

            this.Close();

        }
    }
}
