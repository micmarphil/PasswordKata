using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace PasswordKata
{
    public class LowercaseCharacterRule : IRule
    {
        private const string ERROR_MESSAGE = "Password must contain at least one lowercase character.";

        public bool Validate(string password)
        {
            return (password != password.ToUpper());
        }

        public string FailureMessage()
        {
            return ERROR_MESSAGE;
        }
    }

    public class MinimumLengthRule : IRule
    {
        private const string ERROR_MESSAGE = "Password length must be at least 8 characters.";

        public bool Validate(string password)
        {
            return (password.Length >= 8);
        }

        public string FailureMessage()
        {
            return ERROR_MESSAGE;
        }
    }

    public class NotNullRule : IRule
    {
        private const string ERROR_MESSAGE = "Password cannot be null.";

        public bool Validate(string password)
        {
            return (!(password == null));
        }

        public string FailureMessage()
        {
            return ERROR_MESSAGE;
        }
    }


    public class NumericCharacterRule : IRule
    {

        private const string ERROR_MESSAGE = "Password must contain a digit.";

        public bool Validate(string password)
        {
            return Regex.IsMatch(password, ".*\\d+.*");
        }

        public string FailureMessage()
        {
            return ERROR_MESSAGE;
        }
    }

    public class UppercaseCharacterRule : IRule
    {
        private const string ERROR_MESSAGE = "Password must contain at least one uppercase character.";

        public bool Validate(string password)
        {
            return (password != password.ToLower());
        }

        public string FailureMessage()
        {
            return ERROR_MESSAGE;
        }
    }
}
