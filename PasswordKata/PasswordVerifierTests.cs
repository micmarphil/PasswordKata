using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PasswordKata
{
    [TestFixture]
    public class PasswordVerifierTests
    {
        [Test]
        public void PasswordHelper_ReturnsValidPassword()
        {
            var validPassword = PasswordHelper.ValidPassword();
            var verifier = new PasswordVerifier();

            Assert.DoesNotThrow(() => verifier.Verify(validPassword));
        }


        [Test]
        public void MinimumLengthRule_LessThan8Characters_FailsVerification()
        {
            var invalidPassword = PasswordHelper.ValidPassword().Substring(0, 7);
            var rule = new MinimumLengthRule();

            Assert.That(rule.Validate(invalidPassword), Is.False);
        }

        [Test]
        public void MinimumLengthRule_AtLeast8Characters_PassesVerification()
        {
            var invalidPassword = PasswordHelper.ValidPassword().Substring(0, 8);
            var rule = new MinimumLengthRule();

            Assert.That(rule.Validate(invalidPassword), Is.True);
        }

        [Test]
        public void NotNullRule_Null_FailsValidation()
        {
            string nullPassword = null;
            var rule = new NotNullRule();

            Assert.That(rule.Validate(nullPassword), Is.False);
        }

        [Test]
        public void NotNullRule_NotNull_PassesValidation()
        {
            string nonNullPassword = PasswordHelper.ValidPassword();
            var rule = new NotNullRule();

            Assert.That(rule.Validate(nonNullPassword), Is.True);
        }

        [Test]
        public void UppercaseCharacterRule_WithoutUppercaseCharacter_FailsValidation()
        {
            var allLowercasePassword = PasswordHelper.ValidPassword().ToLower();
            var rule = new UppercaseCharacterRule();

            Assert.That(rule.Validate(allLowercasePassword), Is.False);
        }

        [Test]
        public void UppercaseCharacterRule_WithUppercaseCharacter_PassesValidation()
        {
            var uppercasePassword = PasswordHelper.ValidPassword().ToUpper();
            var rule = new UppercaseCharacterRule();

            Assert.That(rule.Validate(uppercasePassword), Is.True);
        }

        [Test]
        public void LowercaseCharacterRule_WithoutLowercaseCharacter_FailsValidation()
        {
            var allUppercasePassword = PasswordHelper.ValidPassword().ToUpper();
            var rule = new LowercaseCharacterRule();

            Assert.That(rule.Validate(allUppercasePassword), Is.False);
        }

        [Test]
        public void LowercaseCharacterRule_WithLowercaseCharacter_PassesValidation()
        {
            var allLowercasePassword = PasswordHelper.ValidPassword().ToLower();
            var rule = new LowercaseCharacterRule();

            Assert.That(rule.Validate(allLowercasePassword), Is.True);
        }

        [Test]
        public void NumericCharacterRule_WithoutNumericCharacter_FailsValidation()
        {
            var allAlphaPassword = PasswordHelper.AllAlphaPassword();
            var rule = new NumericCharacterRule();

            Assert.That(rule.Validate(allAlphaPassword), Is.False);
        }

        [Test]
        public void NumericCharacterRule_WithNumericCharacter_PassesValidation()
        {
            var passwordWithNumericChar = PasswordHelper.ValidPassword();
            var rule = new NumericCharacterRule();

            Assert.That(rule.Validate(passwordWithNumericChar), Is.True);
        }

        [Test]
        public void AllRulesEnforced()
        {

        }

    }

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

    public abstract class VerificationResult
    {
        public abstract List<string> ValidationMessages();
    }

    public class ValidPassword : VerificationResult
    {

    }

    public class InvalidPassword : VerificationResult
    {

    }

    public interface IValidationStrategy
    {
        VerificationResult Verify(string password);
    }

    public class TestValidationStrategy : IValidationStrategy
    {
        private Func<VerificationResult> _resultToReturn;

        public TestValidationStrategy(Func<VerificationResult> resultToReturn)
        {
            _resultToReturn = resultToReturn;
        }

        public VerificationResult Verify(string password)
        {
            return _resultToReturn.Invoke();
        }
    }
}