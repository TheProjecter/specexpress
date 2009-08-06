using System.Collections.Generic;
using SpecExpress.DSL;
using SpecExpress.Rules.StringValidators;

namespace SpecExpress
{
    /// <summary>
    /// Changed return from RuleBuilder to ActionJoin so displays AND/WITH after a Rule.
    /// </summary>
    public static class CoreValidatorExtensions
    {
        #region Int
        public static ActionJoinBuilder<T, int> GreaterThan<T>(this IRuleBuilder<T, int> expression, int greaterThan)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Int.GreaterThan<T>(greaterThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, int> LessThan<T>(this IRuleBuilder<T, int> expression, int lessThan)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Int.LessThan<T>(lessThan));
            return expression.JoinBuilder;
        }
        #endregion

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