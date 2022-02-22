using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using Wpf_CooperativeFarmers.Classes;

namespace Wpf_CooperativeFarmers
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ObservableCollection<Farmer> _farmers;

        public static ObservableCollection<Product> _products;

        public static ObservableCollection<Delivery> _deliveries;

        public static ObservableCollection<Sales> _sales;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            _farmers = MyStorage.ReadXML<ObservableCollection<Farmer>>("Farmers.xml");

            if (_farmers == null)
            {
                _farmers = new ObservableCollection<Farmer>();
            }

            _products = MyStorage.ReadXML<ObservableCollection<Product>>("Products.xml");

            if (_products == null)
            {
                _products = new ObservableCollection<Product>();

                _products.Add(new Product { id = 1, name = "Potato" });
            }


            _deliveries = MyStorage.ReadXML<ObservableCollection<Delivery>>("Delivery.xml");

            if (_deliveries == null)
            {
                _deliveries = new ObservableCollection<Delivery>();
            }

            _sales = MyStorage.ReadXML<ObservableCollection<Sales>>("Sales.xml");

            if (_sales == null)
            {
                _sales = new ObservableCollection<Sales>();
            }

            var lang = MySettings.Default.language;
            CultureInfo.CurrentUICulture = new CultureInfo(lang);
            CultureInfo.CurrentCulture = new CultureInfo(lang);

        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            MyStorage.WriteXml<ObservableCollection<Farmer>>(_farmers, "Farmers.xml");
        }
    }
}
