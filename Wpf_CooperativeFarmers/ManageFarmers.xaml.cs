using Microsoft.Win32;
using System.Globalization;
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
    /// Interaction logic for ManageFarmers.xaml
    /// </summary>
    public partial class ManageFarmers : Window
    {
        public object MyResources { get; private set; }

        public ManageFarmers()
        {
            
            InitializeComponent();
        }

        private void Btn_AddFarmer_Click(object sender, RoutedEventArgs e)
        {
            var fId = ++MySettings.Default.FarmerId;
            MySettings.Default.Save();

            OpenFileDialog openFileDialog = new OpenFileDialog();

            var res = openFileDialog.ShowDialog();

            Farmer farmer = new Farmer { id = fId, firstName = "", lastName = "", imagePath = openFileDialog.FileName };
            App._farmers.Add(farmer);

            Lbx_farmers.SelectedItem = farmer;
            Lbx_farmers.ScrollIntoView(farmer);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Lbx_farmers.ItemsSource = App._farmers;
            Cbx_gender.ItemsSource = Resource.genderSelection.Split(",");
            Cbx_languages.ItemsSource = Resource.languages.Split(",");
        }

        private void Btn_removeFarmer_Click(object sender, RoutedEventArgs e)
        {
            var itm = Lbx_farmers.SelectedItem as Farmer;
            if (itm == null)
            {
                MessageBox.Show(Resource.msgSelectFarmer, "Error");
                return;
            }
            App._farmers.Remove(itm);
        }

        private void Tbx_filter_TextChanged(object sender, TextChangedEventArgs e)
        {
           var filter = Tbx_filter.Text.ToLower();

            var lst = from m in App._farmers
                      where 
                      m.firstName.ToLower().Contains(filter)
                      ||
                      m.lastName.ToLower().Contains(filter) 
                      select m;

            Lbx_farmers.ItemsSource = lst;
        }

        private void Lbx_farmers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


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
            DoubleAnimation da = new DoubleAnimation
            {
                To = to
            };
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

        private void Cbx_languages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lbx = sender as ComboBox;

            var lang=lbx.SelectedItem as string;

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

