using System;
using System.Collections.Generic;

namespace PasswordKata
{
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
}