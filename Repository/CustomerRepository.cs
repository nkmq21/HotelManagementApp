using Models;
using DataAccessLayer;

namespace Repository


{
    public class CustomerRepository : IRepository<Customer>
    {
        public bool Add(Customer entity)
        {
            return CustomerDAO.AddCustomer(entity);
        }

        public void Delete(Customer entity)
        {
            CustomerDAO.DeleteCustomer(entity);
        }

        public IEnumerable<Customer> GetAll()
        {
            return CustomerDAO.GetListCustomer();
        }

        public List<RoomInformation> GetAvailableRooms(DateTime checkinDate, DateTime checkoutDate, RoomType selectedRoomType, int numberOfGuests)
        {
            throw new NotImplementedException();
        }

        public Customer GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Customer entity)
        {
            return CustomerDAO.UpdateCustomer(entity);
        }
    }
}
