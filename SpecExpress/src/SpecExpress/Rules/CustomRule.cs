using System;
using System.Text.RegularExpressions;

namespace SpecExpress.Rules
{
    public class CustomRule<T> : RuleValidator<T, string>
    {
        private Func<string, bool> _expression;
        public override object[] Parameters
        {
            get { return new object[] {}; }
        }

        public CustomRule(Func<string, bool> rule)
        {
            _expression = rule;
        }

        public override ValidationResult Validate(RuleValidatorContext<T, string> context)
        {
            var result = (bool)(_expression.DynamicInvoke(new object[] { context.PropertyValue }));

            return Evaluate(result, context);
        }
    }
}
