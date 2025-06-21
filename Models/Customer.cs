using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public enum CustomerStatus
    {
        Active = 1,
        Deleted = 2
    }
    public class Customer
    {
        public int CustomerId {  get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Password { get; set; } = null!;
        public CustomerStatus Status { get; set; } = CustomerStatus.Active;

        public Customer() { }
        public Customer(int id, string name, string email, string phone, string password)
        {
            CustomerId = id;
            Name = name;
            Email = email;
            Phone = phone;
            Password = password;
            Status = CustomerStatus.Active;
        }
    }
}
