using Models;
using Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace View
{
    public partial class EditRoomDialog : Window
    {
        private readonly RoomInformation _room;
        private readonly IRoomTypeService _roomTypeService;
        private ObservableCollection<RoomType> _roomTypes;

        public EditRoomDialog()
        {
            InitializeComponent();
        }

        public EditRoomDialog(RoomInformation room, IRoomTypeService roomTypeService)
        {
            InitializeComponent();

            _room = room;
            _roomTypeService = roomTypeService;
            
            // Set title based on whether we're adding or editing
            Title = room.RoomId == 0 ? "Add New Room" : $"Edit Room #{room.RoomId}";
            
            // Load room types
            LoadRoomTypes();
            
            // Set the DataContext
            DataContext = _room;
        }

        private void LoadRoomTypes()
        {
            try
            {
                var roomTypeList = _roomTypeService.GetAllRoomType().ToList();
                _roomTypes = new ObservableCollection<RoomType>(roomTypeList);
                roomTypeComboBox.ItemsSource = _roomTypes;
                
                // Select the current room type if editing
                if (_room.RoomTypeID.HasValue)
                {
                    roomTypeComboBox.SelectedItem = _roomTypes.FirstOrDefault(rt => rt.TypeId == _room.RoomTypeID.Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading room types: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(_room.RoomName))
            {
                MessageBox.Show("Room name cannot be empty", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_room.MaxCapacity <= 0)
            {
                MessageBox.Show("Maximum capacity must be greater than zero", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_room.RoomPricePerDate <= 0)
            {
                MessageBox.Show("Room price must be greater than zero", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (roomTypeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a room type", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Update the room with the selected room type
            var selectedRoomType = roomTypeComboBox.SelectedItem as RoomType;
            _room.RoomTypeID = selectedRoomType.TypeId;
            _room.RoomType = selectedRoomType;

            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}