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
    public class RoomManagementViewModel : INotifyPropertyChanged
    {
        private readonly IRoomService _roomService;
        private readonly IRoomTypeService _roomTypeService;
        private readonly IDialogService _dialogService;
        
        private ObservableCollection<RoomInformation> _rooms;
        private RoomInformation _selectedRoom;
        private string _statusMessage;
        private bool _isLoading;
        private string _searchText;

        public RoomManagementViewModel(IRoomService roomService, IRoomTypeService roomTypeService, IDialogService dialogService)
        {
            _roomService = roomService;
            _roomTypeService = roomTypeService;
            _dialogService = dialogService;
            
            // Initialize commands
            AddRoomCommand = new RelayCommand(ExecuteAddRoom);
            EditRoomCommand = new RelayCommand(ExecuteEditRoom, CanEditOrDeleteRoom);
            DeleteRoomCommand = new RelayCommand(ExecuteDeleteRoom, CanEditOrDeleteRoom);
            RefreshCommand = new RelayCommand(ExecuteRefresh);
            SearchCommand = new RelayCommand(ExecuteSearch);
            
            // Load room data
            LoadRooms();
        }

        public ObservableCollection<RoomInformation> Rooms
        {
            get => _rooms;
            set
            {
                _rooms = value;
                OnPropertyChanged();
            }
        }

        public RoomInformation SelectedRoom
        {
            get => _selectedRoom;
            set
            {
                _selectedRoom = value;
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

        public ICommand AddRoomCommand { get; }
        public ICommand EditRoomCommand { get; }
        public ICommand DeleteRoomCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand SearchCommand { get; }

        private void LoadRooms()
        {
            try
            {
                IsLoading = true;
                StatusMessage = "Loading rooms...";
                
                var roomList = _roomService.GetRoomInformation().ToList();
                
                // Load room types for each room
                foreach (var room in roomList)
                {
                    if (room.RoomTypeID.HasValue)
                    {
                        room.RoomType = _roomTypeService.GetTypeById(room.RoomTypeID.Value);
                    }
                }
                
                Rooms = new ObservableCollection<RoomInformation>(roomList);
                StatusMessage = $"Loaded {Rooms.Count} rooms";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error loading rooms: {ex.Message}";
                Rooms = new ObservableCollection<RoomInformation>();
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
                LoadRooms();
                return;
            }

            try
            {
                IsLoading = true;
                StatusMessage = "Searching rooms...";
                
                var allRooms = _roomService.GetRoomInformation();
                var filteredRooms = allRooms.Where(r =>
                    r.RoomId.ToString().Contains(SearchText) ||
                    r.RoomName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    r.RoomDescription.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    (r.RoomType?.TypeName?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false)
                ).ToList();
                
                Rooms = new ObservableCollection<RoomInformation>(filteredRooms);
                StatusMessage = $"Found {Rooms.Count} rooms";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error searching rooms: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void ExecuteRefresh(object parameter)
        {
            LoadRooms();
        }

        private void ExecuteAddRoom(object parameter)
        {
            try
            {
                var newRoom = new RoomInformation
                {
                    RoomStatus = true
                };
                
                bool? result = _dialogService.ShowEditRoomDialog(newRoom, _roomTypeService);
                
                if (result == true)
                {
                    bool success = _roomService.AddRoom(newRoom);
                    if (success)
                    {
                        StatusMessage = $"Room #{newRoom.RoomId} has been added";
                        LoadRooms();
                    }
                    else
                    {
                        StatusMessage = "Failed to add room";
                    }
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error adding room: {ex.Message}";
            }
        }

        private bool CanEditOrDeleteRoom(object parameter)
        {
            return parameter is RoomInformation && !IsLoading;
        }

        private void ExecuteEditRoom(object parameter)
        {
            if (parameter is RoomInformation room)
            {
                try
                {
                    // Create a copy of the room to edit
                    var roomToEdit = new RoomInformation
                    {
                        RoomId = room.RoomId,
                        RoomName = room.RoomName,
                        RoomDescription = room.RoomDescription,
                        MaxCapacity = room.MaxCapacity,
                        RoomStatus = room.RoomStatus,
                        RoomPricePerDate = room.RoomPricePerDate,
                        RoomTypeID = room.RoomTypeID,
                        RoomType = room.RoomType
                    };
                    
                    bool? result = _dialogService.ShowEditRoomDialog(roomToEdit, _roomTypeService);
                    
                    if (result == true)
                    {
                        bool success = _roomService.UpdateRoom(roomToEdit);
                        if (success)
                        {
                            StatusMessage = $"Room #{roomToEdit.RoomId} has been updated";
                            LoadRooms();
                        }
                        else
                        {
                            StatusMessage = "Failed to update room";
                        }
                    }
                }
                catch (Exception ex)
                {
                    StatusMessage = $"Error updating room: {ex.Message}";
                }
            }
        }

        private void ExecuteDeleteRoom(object parameter)
        {
            if (parameter is RoomInformation room)
            {
                var result = MessageBox.Show(
                    $"Are you sure you want to delete room #{room.RoomId}?",
                    "Confirm Deletion",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        IsLoading = true;
                        StatusMessage = "Deleting room...";
                        
                        _roomService.RemoveRoom(room);
                        
                        // Refresh the list
                        LoadRooms();
                        StatusMessage = $"Room #{room.RoomId} has been deleted";
                    }
                    catch (Exception ex)
                    {
                        StatusMessage = $"Error deleting room: {ex.Message}";
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