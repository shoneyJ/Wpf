using System;
using System.ComponentModel;

namespace Wpf_CooperativeFarmers.Classes
{
    public class Delivery : INotifyPropertyChanged
    {
        public int productId { get; set; }
        public int farmerId { get; set; }

        public int quantity_;
        public int quantity { 
            get {return quantity_; } 
            set {
                quantity_=value;
                OnPropertyChanged(nameof(quantity));
            } 
        }

        public int cost_;

        public int cost {
            get {return cost_; }
            set {cost_=value;
                OnPropertyChanged(nameof (cost));
            }
        }
        public int price { get; set; }
        public DateTime deliveryDate {
            get { return deliveryDate_; }
            set { 
                deliveryDate_ = value;
                OnPropertyChanged(nameof(deliveryDate));            
            }         
        }

        public DateTime deliveryDate_;
       

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
