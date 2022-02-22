using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_CooperativeFarmers.Classes
{
    public class Farmer : INotifyPropertyChanged
    {
        public int id { get; set; }
        public string firstName_;




        public string firstName
        {
            get { return firstName_; }
            set { firstName_ = value;
                OnPropertyChanged(nameof(firstName)); 
            }
        }

        public string lastName_;
        public string lastName
        {
            get { return lastName_; }
            set
            {
                lastName_ = value;
                OnPropertyChanged(nameof(lastName));
            }
        }
        
        public int gender { get; set; }
        public string address { get; set; }
        public string emailid { get; set; }

        public string phoneNumber { get; set; }

        public DateTime birthDate_;

        public DateTime birthDate
        {
            get { return birthDate_; }
            set
            {
                birthDate_ = value;
                OnPropertyChanged(nameof(birthDate));
            }
        }

        public DateTime applicationDate { get; set; }

        public DateTime memberSince { get; set; }



        private string imagePath_;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string imagePath
        {
            get { return imagePath_; }
            set
            {
                imagePath_ = value;
                OnPropertyChanged(nameof(imagePath));
            }
        }

       // public string imagePath { get; set; }

        private void OnPropertyChanged(string propertyName)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
