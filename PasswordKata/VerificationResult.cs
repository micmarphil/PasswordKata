using System.Collections.Generic;

namespace PasswordKata
{
    public abstract class VerificationResult
    {
        public List<string> ValidationMessages { get; set; }
    }

    public class InvalidPassword : VerificationResult
    {
    }

    public class ValidPassword : VerificationResult
    {
    }

}