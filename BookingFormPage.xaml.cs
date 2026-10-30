using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pr5
{
    /// <summary>
    /// Логика взаимодействия для BookingFormPage.xaml
    /// </summary>
    public partial class BookingFormPage :Page
    {
        private ObservableCollection<BookingModel> _bookings;

        private BookingModel _booking;

        private BookingModel _originalBooking;

        public ServiceModel SelectedService { get; set; }
        public BookingFormPage (ObservableCollection<BookingModel> bookings, BookingModel booking = null)
        {
            _originalBooking = booking;
            if (booking == null)
            {
                _booking = new BookingModel( );
            }
            else
            {
                _booking = new BookingModel
                {
                    Id = booking.Id,
                    Client = booking.Client,
                    Date = booking.Date,
                    RoomNumber = booking.RoomNumber
                };

                foreach (var s in booking.Services)
                {
                    _booking.Services.Add(new ServiceModel { Name = s.Name, Price = s.Price, Count = s.Count });
                }
            }
            _bookings = bookings; 
            InitializeComponent( );
            DataContext = _booking;
        }

        private void AddService_Click (object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NewServiceName.Text)) return;
            decimal.TryParse(NewServicePrice.Text, out var price);
            int.TryParse(NewServiceCount.Text, out var count);

            _booking.Services.Add(new ServiceModel
            {
                Name = NewServiceName.Text,
                Price = price,
                Count = count
            });

            NewServiceName.Clear( );
            NewServicePrice.Clear( );
            NewServiceCount.Clear( );
        }

        private void DeleteService_Click (object sender, RoutedEventArgs e)
        {
            if (SelectedService == null)
            {
                MessageBox.Show("Не выбран элемент", "Ошибка");
                return;
            }
            _booking.Services.Remove(SelectedService);
        }

        private void SaveButton_Click (object sender, RoutedEventArgs e)
        {
            if (_originalBooking == null)
            {
                _bookings.Add(_booking);
            }
            else 
            {
                _originalBooking.Id = _booking.Id;
                _originalBooking.Client = _booking.Client;
                _originalBooking.Date = _booking.Date;
                _originalBooking.RoomNumber = _booking.RoomNumber;
                _originalBooking.Services.Clear( );
                foreach (var s in _booking.Services)
                {
                    _originalBooking.Services.Add(s);
                }
            }
            JsonServer.WriteFile(_bookings);
            BackButton_Click(sender, e);
        }

        private void BackButton_Click (object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack( );
        }
    }
}
