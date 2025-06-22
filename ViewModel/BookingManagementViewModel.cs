using Models;
using Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace ViewModel
{
    public class BookingManagementViewModel : INotifyPropertyChanged
    {
        private readonly IBookingService _bookingService;
        private readonly ICustomerService _customerService;
        private readonly IDialogService _dialogService;
        private ObservableCollection<Booking> _bookings;
        private Booking _selectedBooking;
        private string _statusMessage;
        private bool _isLoading;
        private string _searchText;

        public BookingManagementViewModel(IBookingService bookingService, ICustomerService customerService, IDialogService dialogService)
        {
            _bookingService = bookingService;
            _customerService = customerService;
            _dialogService = dialogService;
            
            // ViewDetailsCommand = new RelayCommand(ExecuteViewDetails, CanExecuteViewDetails);
            CancelBookingCommand = new RelayCommand(ExecuteCancelBooking, CanExecuteCancelBooking);
            RefreshCommand = new RelayCommand(ExecuteRefresh);
            SearchCommand = new RelayCommand(ExecuteSearch);
            
            LoadBookings();
        }

        public ObservableCollection<Booking> Bookings
        {
            get => _bookings;
            set
            {
                _bookings = value;
                OnPropertyChanged();
            }
        }

        public Booking SelectedBooking
        {
            get => _selectedBooking;
            set
            {
                _selectedBooking = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
            }
        }

        public ICommand ViewDetailsCommand { get; }
        public ICommand CancelBookingCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand SearchCommand { get; }

        private void LoadBookings()
        {
            try
            {
                IsLoading = true;
                StatusMessage = "Loading bookings...";
                
                var bookingList = _bookingService.GetAllBooking();
                Bookings = new ObservableCollection<Booking>(bookingList);
                
                StatusMessage = $"Loaded {Bookings.Count} bookings";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error loading bookings: {ex.Message}";
                Bookings = new ObservableCollection<Booking>();
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void ExecuteSearch(object parameter)
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                LoadBookings();
                return;
            }

            try
            {
                IsLoading = true;
                StatusMessage = "Searching bookings...";
                
                var allBookings = _bookingService.GetAllBooking();
                var filteredBookings = allBookings.Where(b => 
                    b.BookingId.ToString().Contains(SearchText) ||
                    b.CustomerId.ToString().Contains(SearchText) ||
                    (_customerService.GetCustomerById(b.CustomerId)?.Name?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    b.RoomId.ToString().Contains(SearchText)
                ).ToList();
                
                Bookings = new ObservableCollection<Booking>(filteredBookings);
                StatusMessage = $"Found {Bookings.Count} bookings";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error searching bookings: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void ExecuteRefresh(object parameter)
        {
            LoadBookings();
        }

        // private bool CanExecuteViewDetails(object parameter)
        // {
        //     return parameter is Booking && !IsLoading;
        // }
        //
        // private void ExecuteViewDetails(object parameter)
        // {
        //     if (parameter is Booking booking)
        //     {
        //         // Show booking details dialog
        //         _dialogService.ShowBookingDetailsDialog(booking);
        //     }
        // }

        private bool CanExecuteCancelBooking(object parameter)
        {
            if (parameter is Booking booking)
            {
                return !IsLoading && booking.BookingStatus != BookingStatus.Canceled;
            }
            return false;
        }

        private void ExecuteCancelBooking(object parameter)
        {
            if (parameter is Booking booking)
            {
                var result = MessageBox.Show(
                    $"Are you sure you want to cancel booking #{booking.BookingId}?",
                    "Confirm Cancellation", 
                    MessageBoxButton.YesNo, 
                    MessageBoxImage.Warning);
                
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        IsLoading = true;
                        StatusMessage = "Cancelling booking...";
                        
                        booking.BookingStatus = BookingStatus.Canceled;
                        _bookingService.UpdateBooking(booking);
                        
                        // Refresh the list
                        LoadBookings();
                        StatusMessage = $"Booking #{booking.BookingId} has been cancelled";
                    }
                    catch (Exception ex)
                    {
                        StatusMessage = $"Error cancelling booking: {ex.Message}";
                    }
                    finally
                    {
                        IsLoading = false;
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}