using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ViewModel
{
    public class BookingViewModel : INotifyPropertyChanged
    {
        private readonly IRoomService _roomService;
        private readonly IBookingService _bookingService;
        private readonly IRoomTypeService _roomTypeService;

        private DateTime _checkInDate = DateTime.Today.AddDays(1);
        private DateTime _checkOutDate = DateTime.Today.AddDays(2);
        private int _numberOfGuests = 1;
        private RoomType _selectedRoomType;
        private RoomInformation _selectedRoom;
        private ObservableCollection<RoomType> _roomTypes;
        private ObservableCollection<RoomInformation> _availableRooms;
        private bool _isSearching;
        private decimal _totalPrice;
        private string _statusMessage;
        private Customer _currentCustomer;

        public BookingViewModel(Customer customer, IRoomService roomService, IBookingService bookingService, IRoomTypeService roomTypeService)
        {
            _currentCustomer = customer;
            _roomService = roomService;
            _bookingService = bookingService;
            _roomTypeService = roomTypeService;

            //commands
            SearchRoomAvailabilityCommand = new RelayCommand(ExecuteSearchAvailability, CanExecuteSearchAvailability);
            BookRoomCommand = new RelayCommand(ExecuteBookRoom, CanExecuteBookRoom);
            CancelCommand = new RelayCommand(ExecuteCancel);

            //collections
            _roomTypes = new ObservableCollection<RoomType>();
            _availableRooms = new ObservableCollection<RoomInformation>();
            
            // Load room types in a try-catch block
            try
            {
                var roomTypes = _roomTypeService.GetAllRoomType();
                foreach (var type in roomTypes)
                {
                    _roomTypes.Add(type);
                }
        
                if (_roomTypes.Count > 0)
                {
                    SelectedRoomType = _roomTypes[0];
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error loading room types: {ex.Message}";
            }
        }

        public DateTime CheckInDate
        {
            get => _checkInDate;
            set
            {
                if (_checkInDate != value)
                {
                    _checkInDate = value;
                    // Ensure checkout is after checkin
                    if (_checkOutDate <= _checkInDate)
                    {
                        CheckOutDate = _checkInDate.AddDays(1);
                    }
                    OnPropertyChanged();
                    CalculateTotalPrice();
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public DateTime CheckOutDate
        {
            get => _checkOutDate;
            set
            {
                if (_checkOutDate != value && value > _checkInDate)
                {
                    _checkOutDate = value;
                    OnPropertyChanged();
                    CalculateTotalPrice();
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public int NumberOfGuests
        {
            get => _numberOfGuests;
            set
            {
                if (_numberOfGuests != value && value > 0)
                {
                    _numberOfGuests = value;
                    OnPropertyChanged();
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public RoomType SelectedRoomType
        {
            get => _selectedRoomType;
            set
            {
                if (_selectedRoomType != value)
                {
                    _selectedRoomType = value;
                    OnPropertyChanged();
                    CalculateTotalPrice();
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public RoomInformation SelectedRoom
        {
            get => _selectedRoom;
            set
            {
                if (_selectedRoom != value)
                {
                    _selectedRoom = value;
                    OnPropertyChanged();
                    CalculateTotalPrice();
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public ObservableCollection<RoomInformation> AvailableRooms
        {
            get => _availableRooms;
            set
            {
                _availableRooms = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<RoomType> RoomTypes
        {
            get => _roomTypes;
        }

        public bool IsSearching
        {
            get => _isSearching;
            set
            {
                _isSearching = value;
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

        public decimal TotalPrice
        {
            get => _totalPrice;
            set
            {
                _totalPrice = value;
                OnPropertyChanged();
            }
        }

        public int StayDuration => (CheckOutDate - CheckInDate).Days;

        public ICommand SearchRoomAvailabilityCommand { get; }
        public ICommand BookRoomCommand { get; }
        public ICommand CancelCommand { get; }

        private bool CanExecuteSearchAvailability(object? parameter)
        {
            return !IsSearching && CheckInDate >= DateTime.Today && CheckOutDate > CheckInDate && SelectedRoomType != null && NumberOfGuests > 0;
        }

        private async void ExecuteSearchAvailability(object? parameter)
        {
            try
            {
                IsSearching = true;
                StatusMessage = "Searching for available rooms.";
                AvailableRooms.Clear();

                var rooms = await Task.Run(() => _roomService.GetAvailableRooms(CheckInDate, CheckOutDate, SelectedRoomType, NumberOfGuests));
                foreach (var room in rooms)
                {
                    AvailableRooms.Add(room);
                }
                if (AvailableRooms.Any())
                {
                    SelectedRoom = AvailableRooms.First();
                    StatusMessage = $"Found {AvailableRooms.Count} available rooms";
                }
                else
                {
                    StatusMessage = "Found none available rooms";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            finally
            {
                IsSearching = false;
            }
        }

        public bool CanExecuteBookRoom(object? parameter)
        {
            return !IsSearching &&
                   SelectedRoom != null &&
                   CheckInDate >= DateTime.Today &&
                   CheckOutDate > CheckInDate;
        }

        public async void ExecuteBookRoom(object? parameter)
        {
            try
            {
                IsSearching = true;
                StatusMessage = "Processing your booking...";

                var booking = new Booking
                {
                    CustomerId = _currentCustomer.CustomerId,
                    RoomId = _selectedRoom.RoomId,
                    CheckinTime = CheckInDate,
                    CheckoutTime = CheckOutDate,
                    totalPrice = TotalPrice,
                    BookingStatus = BookingStatus.Confirmed,
                    BookingDate = DateTime.Now,
                };

                var bookingResult = await Task.Run(() => _bookingService.AddBooking(booking));
                if (bookingResult != null)
                {
                    StatusMessage = $"Booking confirmed! Booking ID: {booking.BookingId}";
                    MessageBox.Show($"Your booking has been confirmed!\n\nBooking ID: {booking.BookingId}\nCheck-in: {CheckInDate:d}\nCheck-out: {CheckOutDate:d}\nTotal: {TotalPrice:C}",
                                   "Booking Confirmation",
                                   MessageBoxButton.OK,
                                   MessageBoxImage.Information);

                    // Reset form for new booking
                    ResetForm();
                }
                else
                {
                    StatusMessage = "Failed to process booking. Please try again";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            finally
            {
                IsSearching = false;
            }
        }

        public void ExecuteCancel(object? parameter)
        {
            ResetForm();
        }

        public void ResetForm()
        {
            CheckInDate = DateTime.Today.AddDays(1);
            CheckOutDate = DateTime.Today.AddDays(2);
            NumberOfGuests = 1;
            AvailableRooms.Clear();
            SelectedRoom = null;
            StatusMessage = string.Empty;
        }

        private void CalculateTotalPrice()
        {
            if (SelectedRoom != null)
            {
                TotalPrice = SelectedRoom.RoomPricePerDate * StayDuration;
            }
            else
            {
                TotalPrice = 0;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
