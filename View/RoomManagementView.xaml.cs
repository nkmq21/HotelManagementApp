using Models;
using Services;
using System.Windows.Controls;
using ViewModel;

namespace View
{
    public partial class RoomManagementView : UserControl
    {
        private RoomManagementViewModel _viewModel;

        public RoomManagementView()
        {
            InitializeComponent();
        }

        public RoomManagementView(IRoomService roomService, IRoomTypeService roomTypeService, IDialogService dialogService)
        {
            InitializeComponent();

            // Initialize the ViewModel with services
            _viewModel = new RoomManagementViewModel(roomService, roomTypeService, dialogService);
            DataContext = _viewModel;
        }
    }
}