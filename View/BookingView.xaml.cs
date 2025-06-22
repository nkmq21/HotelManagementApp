using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Models;
using Services;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for BookingView.xaml
    /// </summary>  
    public partial class BookingView : UserControl
    {
        private readonly BookingViewModel _viewModel;

        /// <summary>
        /// Default constructor - primarily for design-time support
        /// </summary>
        public BookingView()
        {
            InitializeComponent();
            // This constructor is mainly for design-time support
        }

        /// <summary>
        /// Constructor that accepts the customer and required services for booking
        /// </summary>
        /// <param name="customer">The current customer making the booking</param>
        /// <param name="roomService">Service for room operations</param>
        /// <param name="bookingService">Service for booking operations</param>
        /// <param name="roomTypeService">Service for room type operations</param>
        public BookingView(Customer customer, IRoomService roomService, IBookingService bookingService, IRoomTypeService roomTypeService)
        {
            InitializeComponent();

            // Create and initialize the ViewModel
            _viewModel = new BookingViewModel(customer, roomService, bookingService, roomTypeService);

            // Removed the call to InitializeCommands as it does not exist
            // Set the DataContext
            DataContext = _viewModel;

            // Register for events
            Loaded += BookingView_Loaded;
        }

        private void BookingView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set initial focus to the check-in date picker or another appropriate control
            if (checkInDatePicker != null)
            {
                checkInDatePicker.Focus();
            }
        }

        /// <summary>
        /// Handle double-click on a room to select it
        /// </summary>
        private void RoomsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGrid dataGrid && dataGrid.SelectedItem is RoomInformation room)
            {
                // Execute booking command if it's enabled
                if (_viewModel.BookRoomCommand.CanExecute(null))
                {
                    _viewModel.BookRoomCommand.Execute(null);
                }
            }
        }

        /// <summary>
        /// Handle button click to decrease number of guests
        /// </summary>
        private void DecrementGuests_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.NumberOfGuests > 1)
            {
                _viewModel.NumberOfGuests--;
            }
        }

        /// <summary>
        /// Handle button click to increase number of guests
        /// </summary>
        private void IncrementGuests_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.NumberOfGuests++;
        }
    }
}
