using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Services
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAllCustomer();
        Customer GetCustomerById(int id);
        bool UpdateCustomer(Customer customer);
        void DeleteCustomer(int id);
        void AddCustomer(Customer customer);
        IEnumerable<Booking> GetCustomerBookings(int customerId);
    }
}
