using System;
using System.ComponentModel;

namespace Wpf_CooperativeFarmers.Classes
{
    public class Sales : INotifyPropertyChanged
    {
        public int productId { get; set; }

        public int quantity_;
        public int quantity
        {
            get { return quantity_; }
            set
            {
                quantity_ = value;
                OnPropertyChanged(nameof(quantity));
            }
        }

        public int price_;

        public int price
        {
            get { return price_; }
            set
            {
                price_ = value;
                OnPropertyChanged(nameof(price));
            }
        }

        public DateTime sellingDate
        {
            get { return sellingDate_; }
            set
            {
                sellingDate_ = value;
                OnPropertyChanged(nameof(sellingDate));
            }
        }

        public DateTime sellingDate_;


        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
