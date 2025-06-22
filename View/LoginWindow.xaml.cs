using System.Windows;
using System.Windows.Input;
using Repository;
using Services;
using ViewModel;

namespace View;

/// <summary>
///     Interaction logic for LoginWindow.xaml
/// </summary>
public partial class LoginWindow : Window
{
    private readonly LoginViewModel _viewModel;

    public LoginWindow()
    {
        try
        {
            InitializeComponent();

            // Initialize ViewModel and set as DataContext
            _viewModel = new LoginViewModel();

            _viewModel.LoginSuccessful += OnLoginSuccessful;

            DataContext = _viewModel;

            // Set focus to username field when window loads
            Loaded += OnWindowLoaded;

            // Set up key event handlers for login controls
            SetupKeyEventHandlers();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error initializing login window: {ex.Message}",
                "Initialization Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void OnWindowLoaded(object sender, RoutedEventArgs e)
    {
        // Set focus to the username textbox
        txtUser.Focus();
    }

    private void SetupKeyEventHandlers()
    {
        // Username textbox: move to password on Enter
        txtUser.KeyDown += (s, e) =>
        {
            if (e.Key == Key.Enter)
            {
                txtPass.Focus();
                e.Handled = true;
            }
        };

        // Password box: attempt login on Enter
        txtPass.KeyDown += (s, e) =>
        {
            if (e.Key == Key.Enter && _viewModel.LoginCommand.CanExecute(txtPass))
            {
                _viewModel.LoginCommand.Execute(txtPass);
                e.Handled = true;
            }
        };

        // Handle window closing
        Closing += (s, e) =>
        {
            // If we're in the middle of logging in, ask for confirmation
            if (_viewModel.IsLoading)
            {
                var result = MessageBox.Show("Authentication in progress. Are you sure you want to cancel?",
                    "Cancel Login", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.No) e.Cancel = true;
            }
        };
    }

    private void OnLoginSuccessful(object? sender, UserRole role)
    {
        try
        {
            Window mainWindow;

            if (role == UserRole.Admin)
            {
                // Create an admin dashboard window
                var adminDashboardWindow = new AdminDashboardWindow();
    
                try
                {
                    // Initialize necessary services
                    var bookingRepository = new BookingRepository();
                    var customerRepository = new CustomerRepository();
                    var roomRepository = new RoomRepository();
                    var roomTypeRepository = new RoomTypeRepository();
        
                    var bookingService = new BookingService(bookingRepository);
                    var customerService = new CustomerService(customerRepository, bookingRepository);
                    var roomService = new RoomService(roomRepository);
                    var roomTypeService = new RoomTypeService(roomTypeRepository);
                    var dialogService = new DialogService();

                    // Create booking management view with services
                    var bookingManagementView = new BookingManagementView(bookingService, customerService, dialogService);
        
                    // Set the booking management view as the main content
                    adminDashboardWindow.SetMainContent(bookingManagementView);
        
                    // Register all views
                    adminDashboardWindow.RegisterViews(
                        new BookingManagementView(bookingService, customerService, dialogService),
                        new CustomerManagementView(customerService, dialogService),
                        new RoomManagementView(roomService, roomTypeService, dialogService));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading admin dashboard: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
    
                mainWindow = adminDashboardWindow;
            }
            else
            {
                // Get the user account from the login process
                var loginVm = sender as LoginViewModel;
                var customer = loginVm?.CurrentCustomer;

                if (customer == null)
                {
                    MessageBox.Show("Unable to retrieve customer information.", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }

                // Create a customer window that hosts the booking view
                var customerWindow = new CustomerWindow(customer);
                try
                {
                    var roomRepository = new RoomRepository();
                    var bookingRepository = new BookingRepository();
                    var roomTypeRepository = new RoomTypeRepository();

                    IRoomService roomService = new RoomService(roomRepository);
                    IBookingService bookingService = new BookingService(bookingRepository);
                    IRoomTypeService roomTypeService = new RoomTypeService(roomTypeRepository);

                    // Create booking view with customer info and add it to the window
                    var bookingView = new BookingView(customer, roomService, bookingService, roomTypeService);
                    customerWindow.SetMainContent(bookingView);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening main window: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }


                mainWindow = customerWindow;
            }

            // Show the new window and close the login window
            mainWindow.Show();
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error opening main window: {ex.Message}", "Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private void Border_MouseDown(object sender, MouseButtonEventArgs e)
    {
        // Allow dragging the window when left mouse button is pressed
        if (e.ChangedButton == MouseButton.Left)
            try
            {
                DragMove();
            }
            catch (InvalidOperationException)
            {
            }
    }

    private void LoginButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (_viewModel.LoginCommand.CanExecute(txtPass)) _viewModel.LoginCommand.Execute(txtPass);
    }
}