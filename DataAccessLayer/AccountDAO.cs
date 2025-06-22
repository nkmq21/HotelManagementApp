using Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace DataAccessLayer
{
    public class AccountDAO
    {
        // Configuration helper for admin credentials
        private static class AdminConfig
        {
            private static readonly IConfiguration _configuration;

            static AdminConfig()
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                _configuration = builder.Build();
            }

            public static string GetAdminEmail()
            {
                return _configuration["AdminAccount:Email"] ?? "admin@FUMiniHotelSystem.com";
            }

            public static string GetAdminPassword()
            {
                return _configuration["AdminAccount:Password"] ?? "@@abc123@@";
            }
        }

        // In-memory collection of accounts
        private static List<Account> _accounts = new List<Account>
        {
            new Account { 
                Id = 1, 
                Username = "admin", 
                PasswordHash = AdminConfig.GetAdminPassword(), 
                Email = AdminConfig.GetAdminEmail(), 
                Role = 1 
            },
            new Account { Id = 2, Username = "user1", PasswordHash = "user1hash", Email = "user1@example.com", Role = 2 },
            new Account { Id = 3, Username = "user2", PasswordHash = "user2hash", Email = "user2@example.com", Role = 2 }
        };

        public static Account GetAccountById(int id)
        {
            // Special handling for admin account (assuming admin has id=1)
            if (id == 1)
            {
                var adminAccount = _accounts.FirstOrDefault(a => a.Id == 1);
                if (adminAccount == null)
                {
                    throw new InvalidOperationException("Admin account not found. System configuration error.");
                }
                return adminAccount;
            }

            // Find regular user account
            var account = _accounts.FirstOrDefault(a => a.Id == id);

            // If no account found, throw exception
            if (account == null)
            {
                throw new KeyNotFoundException($"Account with ID {id} not found.");
            }

            return account;
        }

        public static List<Account> GetAllAccount()
        {
            return _accounts;
        }

        public static Customer GetCustomerByAccountId(int accountId)
        {
            var customers = new List<Customer>
            {
                // Admin account doesn't have a customer profile
                new Customer {
                    CustomerId = 1,
                    Name = "John Doe",
                    Email = "user1@example.com",
                    Phone = "555-1234",
                    Password = "user1hash"
                },
                new Customer {
                    CustomerId = 2,
                    Name = "Jane Smith",
                    Email = "user2@example.com",
                    Phone = "555-5678",
                    Password = "user2hash"
                }
            };

            if (accountId == 1)
            {
                return null;
            }

            int customerId = accountId - 1;

            // Find customer with matching customerId
            var customer = customers.FirstOrDefault(c => c.CustomerId == customerId);

            // If no customer found for this account, throw exception
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer profile for account ID {accountId} not found.");
            }

            return customer;
        }
    }
}