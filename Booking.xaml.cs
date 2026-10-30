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
    /// Логика взаимодействия для Booking.xaml
    /// </summary>
    public partial class Booking :Page
    {
        public ObservableCollection<BookingModel> Books { get; set; } = new ObservableCollection<BookingModel>( );

        private BookingModel _selectedBook;

        public BookingModel SelectedBook
        {
            get => _selectedBook;
            set { _selectedBook = value; }
        }
        public Booking ()
        {
            InitializeComponent( );
            Books = JsonServer.ReadFile( );
            //Books.Add(new BookingModel( ) { Id = 1, Client = "Иван", Date = DateTime.Now, RoomNumber = 11 });
            //Books.Add(new BookingModel( ) { Id = 2, Client = "Саша", Date = DateTime.Now, RoomNumber = 12 });
            DataContext = this;
        }

        private void Add_Click (object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BookingFormPage(Books));
        }

        private void Delete_Click (object sender, RoutedEventArgs e)
        {
            SelectedBook = List.SelectedItem as BookingModel;
            if (SelectedBook == null)
            {
                MessageBox.Show("Не выбран элемент", "Ошибка");
                return;
            }
            Books.Remove(SelectedBook);
            SelectedBook = null;
            JsonServer.WriteFile( Books);
        }

        private void Red_Click (object sender, RoutedEventArgs e)
        {
            SelectedBook = List.SelectedItem as BookingModel;
            if (SelectedBook == null)
            {
                MessageBox.Show("Не выбран элемент", "Ошибка");
                return;
            }
            NavigationService.Navigate(new BookingFormPage(Books, SelectedBook));
        }

        
    }
}
