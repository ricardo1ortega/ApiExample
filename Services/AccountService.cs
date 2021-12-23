using ApiExample.Db.Context;
using ApiExample.Db.Models;
using ApiExample.Resources;
using Microsoft.Extensions.Logging;
using System;

namespace ApiExample.Services
{
    public interface IAccountService
    {
        bool IsAnExistingUser(string userName);
        bool IsValidUserCredentials(string userName, string password);
        bool CreateAccount(string userName, string password);
    }

    public class AccountService : IAccountService
    {
        private readonly ILogger _logger;
        private UserContext _userContext;

        public AccountService(ILoggerFactory loggerFactory,
                    UserContext userContext)
        {
            _logger = loggerFactory.CreateLogger<AccountService>();
            _userContext = userContext;
        }

        public bool IsValidUserCredentials(string userName, string password)
        {
            _logger.LogInformation($"Validating user [{userName}]");
            if (string.IsNullOrWhiteSpace(userName))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            return _userContext.IsValidUserCredentials(userName, password);
        }

        public bool IsAnExistingUser(string userName)
        {
            return false;// _users.ContainsKey(userName);
        }

        public bool CreateAccount(string userName, string password)
        {
            _userContext.SaveUser(userName, password);
            return true;
        }
    }
}
