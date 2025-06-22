using Models;
using Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security;
using System.Windows;
using System.Windows.Input;

namespace ViewModel
{
    public enum UserRole
    {
        Customer = 1,
        Admin = 2
    }
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IAccountService _accountService;
        private string _username = string.Empty;
        private string _statusMessage = string.Empty;
        private bool _isLoading = false;
        private UserRole _currentRole;
        

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler<UserRole>? LoginSuccessful;

        public LoginViewModel()
        {
            _accountService = new AccountService();
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
            CancelCommand = new RelayCommand(ExecuteCancel);
        }
        
        public Customer? CurrentCustomer { get; private set; }
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public UserRole CurrentRole
        {
            get => _currentRole;
            private set
            {
                _currentRole = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand CancelCommand { get; }

        private bool CanExecuteLogin(object? parameter)
        {
            // The parameter will be the password from the view
            var passwordBox = parameter as System.Windows.Controls.PasswordBox;
            return !IsLoading &&
                   !string.IsNullOrWhiteSpace(Username) &&
                   passwordBox != null &&
                   !string.IsNullOrWhiteSpace(passwordBox.Password);
        }

        private void ExecuteLogin(object? parameter)
        {
            try
            {
                IsLoading = true;
                StatusMessage = "Logging in...";

                var passwordBox = parameter as System.Windows.Controls.PasswordBox;
                if (passwordBox == null)
                    return;

                // In a real app, you would authenticate with username/password
                // For this example, we're just using a simplified approach
                var authenticatedAccount = Authenticate(Username, passwordBox.Password);

                if (authenticatedAccount != null)
                {
                    StatusMessage = "Login successful!";
                    CurrentRole = (UserRole)(authenticatedAccount.Role ?? (int)UserRole.Customer);
                    OpenMainWindow();
                }
                else
                {
                    StatusMessage = "Invalid username or password";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void ExecuteCancel(object? parameter)
        {
            // Close the application
            Application.Current.Shutdown();
        }

        private Account? Authenticate(string username, string password)
        {
            try
            {
                var accounts = _accountService.GetAccounts();
                var account = accounts.FirstOrDefault(acc =>
                    acc.Username == username &&
                    acc.PasswordHash == password);
            
                if (account != null)
                {
                    // Get the customer associated with this account
                    CurrentCustomer = _accountService.GetCustomerByAccountId(account.Id);
                }
        
                return account;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void OpenMainWindow()
        {
            //MessageBox.Show("Login successful! Main window would open here.",
            //    "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            LoginSuccessful?.Invoke(this, CurrentRole);

        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}