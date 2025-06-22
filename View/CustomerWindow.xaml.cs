using System;
        using System.Windows;
        using Models;
        
        namespace View
        {
            /// <summary>
            /// Interaction logic for CustomerWindow.xaml
            /// </summary>
            public partial class CustomerWindow : Window
            {
                private Customer _customer;
        
                /// <summary>
                /// Default constructor for design-time and XAML preview
                /// </summary>
                public CustomerWindow()
                {
                    InitializeComponent();
        
                    // Set window title
                    Title = "Hotel Booking System - Customer Portal";
                    
                    // Handle window loaded event
                    Loaded += CustomerWindow_Loaded;
                }
                
                /// <param name="customer">The current logged-in customer</param>
                public CustomerWindow(Customer customer) : this()
                {
                    _customer = customer;
        
                    // Display customer name in the header
                    if (_customer != null)
                    {
                        CustomerNameDisplay.Text = $"Welcome, {_customer.Name}";
                    }
                }

                /// <param name="content">The UserControl to display</param>
                public void SetMainContent(object content)
                {
                    MainContent.Content = content;
                }
                
                private void LogoutButton_Click(object sender, RoutedEventArgs e)
                {
                    var result = MessageBox.Show("Are you sure you want to logout?",
                        "Confirm Logout", MessageBoxButton.YesNo, MessageBoxImage.Question);
        
                    if (result == MessageBoxResult.Yes)
                    {
                        // Show login window and close this window
                        var loginWindow = new LoginWindow();
                        loginWindow.Show();
                        this.Close();
                    }
                }
        
                /// <summary>
                /// Window loaded event handler
                /// </summary>
                private void CustomerWindow_Loaded(object sender, RoutedEventArgs e)
                {
                    // Additional initialization can be done here if needed
                }
            }
        }