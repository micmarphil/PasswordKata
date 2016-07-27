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
        //[Test]
        //public void PasswordHelper_ReturnsValidPassword()
        //{
        //    var validPassword = PasswordHelper.ValidPassword();
        //    var verifier = new PasswordVerifier();

        //    Assert.DoesNotThrow(() => verifier.Verify(validPassword));
        //}


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
        public void AllValidationsMustPass_ReturnsForAnyPassword_VerificationResult()
        {
            var allValidationsMustPass = new AllValidationsMustPass();

            var invalidPasswordResult = allValidationsMustPass.Verify(null);
            var validPasswordResult = allValidationsMustPass.Verify(PasswordHelper.ValidPassword());

            Assert.IsInstanceOf<VerificationResult>(invalidPasswordResult);
            Assert.IsInstanceOf<VerificationResult>(validPasswordResult);
        }


    }

}