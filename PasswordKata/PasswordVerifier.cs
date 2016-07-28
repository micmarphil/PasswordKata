using System;

namespace PasswordKata
{
    public class PasswordVerifier
    {
        public void Verify(string password, IValidationStrategy validationStrategy)
        {
            var validationResult = validationStrategy.Verify(password);
            if (validationResult is ValidPassword)
            {
                return;
            }

            throw new ArgumentException("");
        }
    }
}
