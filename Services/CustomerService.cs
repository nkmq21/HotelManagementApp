using Models;
using Repository;

namespace Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Booking> _bookingRepository;

        public CustomerService(IRepository<Customer> customerRepository, IRepository<Booking> bookingRepository)
        {
            _bookingRepository = bookingRepository;
            _customerRepository = customerRepository;
        }

        public void AddCustomer(Customer customer)
        {
            _customerRepository.Add(customer);
        }

        public void DeleteCustomer(int id)
        {
            _customerRepository.Delete(GetCustomerById(id));
        }

        public IEnumerable<Customer> GetAllCustomer()
        {
            return _customerRepository.GetAll();
        }

        public IEnumerable<Booking> GetCustomerBookings(int customerId)
        {
            return _bookingRepository.GetAll().Where(c => c.CustomerId == customerId);
        }

        public Customer GetCustomerById(int id)
        {
            return _customerRepository.GetById(id);
        }

        public bool UpdateCustomer(Customer customer)
        {
            return _customerRepository.Update(customer);
        }
    }
}
