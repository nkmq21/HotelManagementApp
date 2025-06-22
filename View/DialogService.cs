using Models;
using Services;
using ViewModel;

namespace View
{
    public class DialogService : IDialogService
    {
        public bool? ShowEditCustomerDialog(Customer customer, ICustomerService customerService)
        {
            CustomerInfoViewmodel cusInfo = new CustomerInfoViewmodel(customerService, customer);
            EditCustomerInfo edit = new EditCustomerInfo();
            edit.DataContext = cusInfo;
            return edit.ShowDialog();
        }
        public void ShowBookingDetailsDialog(Booking booking)
        {
            var bookingDetailsWindow = new BookingDetailsWindow(booking);
            bookingDetailsWindow.ShowDialog();
        }
        public bool? ShowEditRoomDialog(RoomInformation room, IRoomTypeService roomTypeService)
        {
            var editRoomDialog = new EditRoomDialog(room, roomTypeService);
            return editRoomDialog.ShowDialog();
        }
    }
}