using Models;
using Services;
using System.Windows;

namespace View
{
    public partial class BookingDetailsWindow : Window
    {
        public BookingDetailsWindow()
        {
            InitializeComponent();
        }

        public BookingDetailsWindow(Booking booking)
        {
            InitializeComponent();
            
            // Set the booking as the DataContext
            DataContext = booking;
            
            // Set the window title
            Title = $"Booking Details - #{booking.BookingId}";
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}