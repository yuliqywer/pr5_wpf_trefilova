using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace pr5
{
    public class ServiceModel : INotifyPropertyChanged
    {
        private string _name = "";
        private decimal _price;
        private int _count;

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged( ); }
        }
        
        public decimal Price
        {
            get => _price;
            set { _price = value; OnPropertyChanged( ); OnPropertyChanged(nameof(ServiceTotal)); }
        }
        public int Count
        {
            get => _count;
            set { _count = value; OnPropertyChanged( ); OnPropertyChanged(nameof(ServiceTotal)); }
        }

        public decimal ServiceTotal
        {
            get { return Price * Count; }
        }
        public ServiceModel ()
        { 
        
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged ([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
