namespace PasswordKata
{
    public interface IValidationStrategy
    {
        VerificationResult Verify(string password);
    }
}