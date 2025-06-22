using Models;
using Services;
using System.Windows.Controls;
using ViewModel;

namespace View
{
    public partial class CustomerManagementView : UserControl
    {
        private CustomerManagementViewmodel _viewModel;

        public CustomerManagementView()
        {
            InitializeComponent();
        }

        public CustomerManagementView(ICustomerService customerService, IDialogService dialogService)
        {
            InitializeComponent();

            // Initialize the ViewModel with services
            _viewModel = new CustomerManagementViewmodel(customerService, dialogService);
            DataContext = _viewModel;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is Customer customer)
            {
                _viewModel.SelectedCustomer = customer;
            }
        }
    }
}