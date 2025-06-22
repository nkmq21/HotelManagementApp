using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public class CustomerManagementViewmodel
    {
        public ObservableCollection<Customer> Customers { get; set; }

        private readonly ICustomerService _customerService;
        private readonly IDialogService _dialogService;

        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                _selectedCustomer = value;
            }
        }

        public CustomerManagementViewmodel(ICustomerService customerService, IDialogService dialogService)
        {
            _customerService = customerService;
            _dialogService = dialogService;
            LoadCustomer();
            ShowEditWindowCommand = new RelayCommand(ShowEditWindow, CanShowEditWindow);
            DeleteCustomerCommand = new RelayCommand(ShowDeleteWindow, CanShowDelWindow);
        }

        private void LoadCustomer()
        {
            try
            {
                var customerList = _customerService.GetAllCustomer();
                Customers = new ObservableCollection<Customer>(customerList);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading customers: {ex.Message}");
                Customers = new ObservableCollection<Customer>();
            }
        }

        public ICommand ShowEditWindowCommand { get; set; }
        public ICommand DeleteCustomerCommand { get; set; }

        private void ShowEditWindow(object parameter)
        {
            Customer customerToEdit = parameter as Customer;

            if (customerToEdit != null)
            {
                var result = _dialogService.ShowEditCustomerDialog(customerToEdit, _customerService);
                if (result == true)
                {
                    LoadCustomer();
                }
            }
        }

        private bool CanShowEditWindow(object parameter)
        {
            return parameter is Customer;
        }

        private void ShowDeleteWindow(object parameter)
        {
            Customer customerToDel = parameter as Customer;
            if (customerToDel != null)
            {
                _customerService.DeleteCustomer(customerToDel.CustomerId);
                LoadCustomer();
            }
        }

        private bool CanShowDelWindow(object parameter)
        {
            return parameter is Customer;
        }
    }
}
