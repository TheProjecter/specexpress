using System.Collections.Generic;
using SpecExpress.DSL;
using SpecExpress.Rules.StringValidators;

namespace SpecExpress
{
    /// <summary>
    /// Changed return from RuleBuilder to ActionJoin so displays AND/WITH after a Rule.
    /// </summary>
    public static class DefaultRuleExtensions
    {
        public static ActionJoinBuilder<T, string> LengthBetween<T>(this IRuleBuilder<T, string> expression, int min,
                                                                    int max)
        {
            expression.RegisterValidator(new LengthValidator<T>(min, max));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, string> IsInSet<T>(this IRuleBuilder<T, string> expression, List<string> set)
        {
            expression.RegisterValidator(new IsInSet<T>(set));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, string> Between<T>(this IRuleBuilder<T, string> expression, int min,
                                                                   int max)
        {
            expression.RegisterValidator(new LengthValidator<T>(min, max));
            return expression.JoinBuilder;
        }

    }
}