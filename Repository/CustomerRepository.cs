using Models;
using DataAccessLayer;

namespace Repository


{
    public class CustomerRepository : IRepository<Customer>
    {
        public void Add(Customer entity)
        {
            CustomerDAO.AddCustomer(entity);
        }

        public void Delete(Customer entity)
        {
            CustomerDAO.DeleteCustomer(entity);
        }

        public IEnumerable<Customer> GetAll()
        {
            return CustomerDAO.GetListCustomer();
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
