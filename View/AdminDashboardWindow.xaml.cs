using System.Windows;
using System.Windows.Controls;

namespace View
{
    public partial class AdminDashboardWindow : Window
    {
        private UserControl _bookingManagementView;
        private UserControl _customerManagementView;
        private UserControl _roomManagementView;
        
        public AdminDashboardWindow()
        {
            InitializeComponent();
            
            // Set window title
            Title = "Hotel Booking System - Admin Dashboard";
            
            // Handle window loaded event
            Loaded += AdminDashboardWindow_Loaded;
        }
        
        public void RegisterViews(UserControl bookingManagementView, UserControl customerManagementView, UserControl roomManagementView)
        {
            _bookingManagementView = bookingManagementView;
            _customerManagementView = customerManagementView;
            _roomManagementView = roomManagementView;
        }
        
        public void SetMainContent(object content)
        {
            MainContent.Content = content;
        }
        
        private void BookingManagementButton_Click(object sender, RoutedEventArgs e)
        {
            SetMainContent(_bookingManagementView);
        }
        
        private void CustomerManagementButton_Click(object sender, RoutedEventArgs e)
        {
            SetMainContent(_customerManagementView);
        }
        
        private void RoomManagementButton_Click(object sender, RoutedEventArgs e)
        {
            SetMainContent(_roomManagementView);
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
        
        private void AdminDashboardWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}