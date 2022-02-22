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
    /// Interaction logic for SalesReport.xaml
    /// </summary>
    public partial class SalesReport : Window
    {
        public SalesReport()
        {
            InitializeComponent();
        }

        private void Btn_addStorage_Click(object sender, RoutedEventArgs e)
        {



        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Lbx_farmers.ItemsSource = App._farmers;

            Cbx_languages.ItemsSource = Resource.languages.Split(",");


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

        }

        private void _calendar_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
            if (_calendar.DisplayMode == CalendarMode.Month)
                _calendar.DisplayMode = CalendarMode.Year;

        }

        private void _calendar_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {
            //var farmer = Lbx_farmers.SelectedItem as Farmer;
            var startDate = _calendar.DisplayDate;
            var endDate=new DateTime(startDate.Year, startDate.Month + 1, 1).AddDays(-1);

            var monthlySales = App._sales.Where(s => s.sellingDate >= startDate && s.sellingDate<=endDate).ToList();

            //var monthlyDeliveryByFarmer = App._deliveries.Where(s => (s.deliveryDate >= startDate && s.deliveryDate <= endDate)&& s.farmerId== farmer.id).ToList();

           var groupBySales= monthlySales.GroupBy(g => g.productId).Select(t => new ViewSalesReport
            {
                ProductId = t.Key,
                Quantity = t.Sum(ta => ta.quantity),
                Price=t.Sum(ta => ta.price),
            }).ToList();

            //var groupByDelivies = monthlyDeliveryByFarmer.GroupBy(g => g.productId).Select(t => new
            //{
            //    ProductId = t.Key,
            //    FarmerIds=t.Select(ta=>ta.farmerId),
            //    Quantity = t.Sum(ta => ta.quantity),
                
            //}).ToList();
            if(Lbx_sales!=null)
            Lbx_sales.ItemsSource = groupBySales;


        }
      
    }
}
