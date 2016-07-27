using System;
using System.Collections.Generic;

namespace PasswordKata
{
    public class ValidationContext
    {
        public void RunValidation(string password)
        {

        }
    }

    public class AllValidationsMustPass : IValidationStrategy
    {

        public VerificationResult Verify(string password)
        {
            var notNullRule = new NotNullRule();

            if (!notNullRule.Validate(password))
                return new InvalidPassword() {ValidationMessages = 
                    new List<string>() {notNullRule.FailureMessage()}};

            var rules = new List<IRule>()
                     {new MinimumLengthRule(),
                      new UppercaseCharacterRule(),
                      new LowercaseCharacterRule(),
                      new NumericCharacterRule()};

            var validationMessages = EvaluateRules(password, rules);

            if (validationMessages.Count == 0)
                return new ValidPassword();

            return new InvalidPassword() {ValidationMessages = validationMessages};
        }

        private List<string> EvaluateRules(string password, List<IRule> rules)
        {
            var validationMessages = new List<string>();

            foreach (var rule in rules)
            {
                var validationPassed = rule.Validate(password);

                if (!validationPassed)
                {
                    validationMessages.Add(rule.FailureMessage());
                }
            }

            return validationMessages;
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
        public List<string> ValidationMessages { get; set; }
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
