using Models;

namespace DataAccessLayer
{
    public class CustomerDAO
    {
        private static readonly List<Customer> listCustomer = new List<Customer>();

        static CustomerDAO()
        {
            Customer customer1 = new Customer(1, "John Smith", "john.smith@gmail.com", "123-456-7890", "password123");
            Customer customer2 = new Customer(2, "Emma Johnson", "emma.j@outlook.com", "234-567-8901", "secure456");
            Customer customer3 = new Customer(3, "Michael Brown", "mbrown@yahoo.com", "345-678-9012", "brownie789");
            Customer customer4 = new Customer(4, "Sophia Williams", "sophia.w@gmail.com", "456-789-0123", "sophiapass");
            Customer customer5 = new Customer(5, "William Davis", "will.davis@hotmail.com", "567-890-1234", "willd2023");
            Customer customer6 = new Customer(6, "Olivia Miller", "olivia.m@gmail.com", "678-901-2345", "oliv345m");
            Customer customer7 = new Customer(7, "James Wilson", "jwilson@business.com", "789-012-3456", "wilson789j");
            Customer customer8 = new Customer(8, "Ava Moore", "ava.moore@gmail.com", "890-123-4567", "avamoore22");
            Customer customer9 = new Customer(9, "Alexander Taylor", "alex.t@outlook.com", "901-234-5678", "alex9876");
            Customer customer10 = new Customer(10, "Charlotte Anderson", "charlotte.a@yahoo.com", "012-345-6789", "charlotte123");

            listCustomer = new List<Customer> {
                customer1, customer2, customer3, customer4, customer5,
                customer6, customer7, customer8, customer9, customer10
            };
        }

        public static List<Customer> GetListCustomer()
        {
            return listCustomer;
        }

        public static void AddCustomer(Customer customer)
        {
            if (!listCustomer.Any(c => customer.CustomerId == c.CustomerId))
            {
                listCustomer.Add(customer);
            }
        }

        public static void DeleteCustomer(Customer customer)
        {
            if (listCustomer.Any(c => customer.CustomerId == c.CustomerId))
            {
                listCustomer.Remove(customer);
            }
        }

        public static bool UpdateCustomer(Customer customer)
        {
            Customer? cus = listCustomer.FirstOrDefault(predicate: c => c.CustomerId == customer.CustomerId);
            if (cus != null)
            {
                cus.Phone = customer.Phone;
                cus.Name = customer.Name;
                cus.Email = customer.Email;
                cus.Password = customer.Password;
                cus.Status = customer.Status;
                return true;
            }
            return false;
        }
    }
}
