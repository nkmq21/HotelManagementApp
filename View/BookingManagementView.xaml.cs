using Models;
using Services;
using System.Windows.Controls;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for BookingManagementView.xaml
    /// </summary>
    public partial class BookingManagementView : UserControl
    {
        private BookingManagementViewModel _viewModel;

        public BookingManagementView()
        {
            InitializeComponent();
        }

        public BookingManagementView(IBookingService bookingService, ICustomerService customerService, IDialogService dialogService)
        {
            InitializeComponent();

            // Initialize the ViewModel with services
            _viewModel = new BookingManagementViewModel(bookingService, customerService, dialogService);
            DataContext = _viewModel;
        }
    }
}