using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ShoppingHelperForms.Model
{
    public class Item : INotifyPropertyChanged
    {
        public string _id { get; set; }
        private bool _isChecked = false;
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Owner { get; set; }
        public bool IsChecked
        {
            get { return _isChecked; }
            set 
            { 
                _isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
