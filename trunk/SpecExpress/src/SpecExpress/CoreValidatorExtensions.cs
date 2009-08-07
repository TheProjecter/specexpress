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

        public static ActionJoinBuilder<T, int> GreaterThanEqualTo<T>(this IRuleBuilder<T, int> expression, int greaterThanEqualTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Int.GreaterThanEqualTo<T>(greaterThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, int> LessThanEqualTo<T>(this IRuleBuilder<T, int> expression, int lessThanEqualTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Int.LessThanEqualTo<T>(lessThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, int> Between<T>(this IRuleBuilder<T, int> expression, int floor, int ceiling)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Int.Between<T>(floor,ceiling));
            return expression.JoinBuilder;
        }

        #endregion

        #region String

        public static ActionJoinBuilder<T, string> LengthBetween<T>(this IRuleBuilder<T, string> expression, int min,
                                                                    int max)
        {
            expression.RegisterValidator(new LengthBetween<T>(min, max));
            return expression.JoinBuilder;
        }
        public static ActionJoinBuilder<T, string> MinLength<T>(this IRuleBuilder<T, string> expression, int min)
        {
            expression.RegisterValidator(new MinLength<T>(min));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, string> MaxLength<T>(this IRuleBuilder<T, string> expression, int max)
        {
            expression.RegisterValidator(new MaxLength<T>(max));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, string> IsNumeric<T>(this IRuleBuilder<T, string> expression)
        {
            expression.RegisterValidator(new Numeric<T>());
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, string> IsAlpha<T>(this IRuleBuilder<T, string> expression)
        {
            expression.RegisterValidator(new Alpha<T>());
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, string> IsInSet<T>(this IRuleBuilder<T, string> expression, IEnumerable<string> set)
        {
            expression.RegisterValidator(new IsInSet<T>(set));
            return expression.JoinBuilder;
        }

        #endregion


        public static ActionJoinBuilder<T, string> IsInSet<T>(this IRuleBuilder<T, string> expression, List<string> set)
        {
            expression.RegisterValidator(new IsInSet<T>(set));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, string> Between<T>(this IRuleBuilder<T, string> expression, int min,
                                                              int max)
        {
            expression.RegisterValidator(new LengthBetween<T>(min, max));
            return expression.JoinBuilder;
        }
    }
}