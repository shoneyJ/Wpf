using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Wpf_CooperativeFarmers
{
    public class int2string : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var genders = MyResources.Resource.genderSelection.Split(',');
            switch ((int)value)
            {
                case 0: return genders[0];
                case 1: return genders[1];
                case 2: return genders[2];
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string res = (string)value;

            switch (res.Substring(0, 1))
            {
                case "m": return 0;
                case "f": return 1;
                case "d": return 2;
            }
            return null;
        }
    }

    public class String2ImagePath : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null)
            {

                return $"/Images/icon_add.png";
            }
            else
            {
                if (File.Exists(value.ToString()))
                    return value;
                else
                    return $"/Images/icon_add.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class Id2ProductName : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var id = (int)value;
            return App._products.FirstOrDefault(x=> x.id == id).name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class Id2MeasuingUnit : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var id = (int)value;
            return App._products.FirstOrDefault(x => x.id == id).measuringUnit;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class String2FlowDirection : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString().Contains("ToRight"))
                return FlowDirection.LeftToRight;
            else
                return FlowDirection.RightToLeft;
        }



        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
