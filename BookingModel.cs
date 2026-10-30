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
    public class BookingModel : INotifyPropertyChanged
    {
        private int _id;
        private string _client = "";
        private DateTime _date = DateTime.Now;
        private int _roomNumber;

        public int Id {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }
        public string Client
        {
            get => _client;
            set { _client = value; OnPropertyChanged( ); }
        }
        public DateTime Date
        {
            get => _date;
            set { _date = value; OnPropertyChanged( ); }
        }
        public int RoomNumber
        {
            get => _roomNumber;
            set { _roomNumber = value; OnPropertyChanged( ); }
        }

        public ObservableCollection<ServiceModel> Services { get; set; }

        public decimal BookingTotal
        {
            get {
                decimal sum = 0;
                foreach (var s in Services) sum += s.ServiceTotal;
                return sum;
            }
        }

        public BookingModel () {
            Services = new ObservableCollection<ServiceModel>( );
            Services.CollectionChanged += Services_CollectionChanged;
        }

        private void Services_CollectionChanged (object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(BookingTotal));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged ([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
