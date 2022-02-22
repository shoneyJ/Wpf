using System.ComponentModel;

namespace Wpf_CooperativeFarmers.Classes
{
    public class Product : INotifyPropertyChanged
    {
        public int id { get; set; }
        public string name { get; set; }

        public string measuringUnit { get; set; }

        public int? currentQuantity_;

        public int? currentQuantity
        {
            get { return currentQuantity_; }
            set
            {
                currentQuantity_ = value;
                OnPropertyChanged(nameof(currentQuantity));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
