using Models;
using Services;

namespace ViewModel
{
    public interface IDialogService
    {
        bool? ShowEditCustomerDialog(Customer customer, ICustomerService customerService);
        void ShowBookingDetailsDialog(Booking booking);
        bool? ShowEditRoomDialog(RoomInformation room, IRoomTypeService roomTypeService);

    }
}