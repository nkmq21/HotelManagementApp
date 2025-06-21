using System.ComponentModel;
using System.Runtime.CompilerServices;
using Models;
using Services;

namespace ViewModel
{
    public class CustomerInfoViewmodel : INotifyPropertyChanged
    {
        private Customer _customer;
        private readonly ICustomerService _customerService;
        private readonly Dictionary<string, List<string>> _errors = new();

        public CustomerInfoViewmodel(ICustomerService customerService, Customer customer)
        {
            _customer = customer ?? new Customer();
            _customerService = customerService;
        }

        public int CustomerId
        {
            get => _customer.CustomerId;
            set
            {
                if (_customer.CustomerId != value)
                {
                    _customer.CustomerId = value;
                    OnPropertyChange();
                }
            }
        }

        public string Name
        {
            get => _customer.Name ?? string.Empty;
            set
            {
                if (_customer.Name != value)
                {
                    _customer.Name = value; OnPropertyChange();
                }
            }
        }

        public string Email
        {
            get => _customer?.Email ?? string.Empty;
            set
            {
                if (_customer.Email != value)
                {
                    _customer.Email = value; OnPropertyChange();
                    OnPropertyChange(nameof(IsValidEmail));
                }
            }
        }

        public string Phone
        {
            get => _customer.Phone ?? string.Empty;
            set
            {
                if (_customer.Phone != value)
                {
                    _customer.Phone = value; OnPropertyChange();
                }
            }
        }

        public string Password
        {
            get => _customer.Password ?? string.Empty;
            set
            {
                if (_customer.Password != value)
                {
                    _customer.Password = value; OnPropertyChange();
                }
            }
        }

        public CustomerStatus Status
        {
            get => _customer.Status;
            set
            {
                if (_customer.Status != value)
                {
                    _customer.Status = value; OnPropertyChange();
                }
            }
        }


        private bool IsValidEmail(string email)
        {
            if (!string.IsNullOrEmpty(email) && email.Contains("@"))
            {
                return true;
            }
            return false;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChange([CallerMemberName] string ? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
