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

        #region Long
        public static ActionJoinBuilder<T, long> GreaterThan<T>(this IRuleBuilder<T, long> expression, long greaterThan)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Long.GreaterThan<T>(greaterThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, long> LessThan<T>(this IRuleBuilder<T, long> expression, long lessThan)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Long.LessThan<T>(lessThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, long> GreaterThanEqualTo<T>(this IRuleBuilder<T, long> expression, long greaterThanEqualTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Long.GreaterThanEqualTo<T>(greaterThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, long> LessThanEqualTo<T>(this IRuleBuilder<T, long> expression, long lessThanEqualTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Long.LessThanEqualTo<T>(lessThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, long> Between<T>(this IRuleBuilder<T, long> expression, long floor, long ceiling)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Long.Between<T>(floor, ceiling));
            return expression.JoinBuilder;
        }

        #endregion

        #region Short
        public static ActionJoinBuilder<T, short> GreaterThan<T>(this IRuleBuilder<T, short> expression, short greaterThan)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Short.GreaterThan<T>(greaterThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, short> LessThan<T>(this IRuleBuilder<T, short> expression, short lessThan)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Short.LessThan<T>(lessThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, short> GreaterThanEqualTo<T>(this IRuleBuilder<T, short> expression, short greaterThanEqualTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Short.GreaterThanEqualTo<T>(greaterThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, short> LessThanEqualTo<T>(this IRuleBuilder<T, short> expression, short lessThanEqualTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Short.LessThanEqualTo<T>(lessThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, short> Between<T>(this IRuleBuilder<T, short> expression, short floor, short ceiling)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Short.Between<T>(floor, ceiling));
            return expression.JoinBuilder;
        }

        #endregion

        #region Float
        public static ActionJoinBuilder<T, float> GreaterThan<T>(this IRuleBuilder<T, float> expression, float greaterThan)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Float.GreaterThan<T>(greaterThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, float> LessThan<T>(this IRuleBuilder<T, float> expression, float lessThan)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Float.LessThan<T>(lessThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, float> GreaterThanEqualTo<T>(this IRuleBuilder<T, float> expression, float greaterThanEqualTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Float.GreaterThanEqualTo<T>(greaterThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, float> LessThanEqualTo<T>(this IRuleBuilder<T, float> expression, float lessThanEqualTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Float.LessThanEqualTo<T>(lessThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, float> Between<T>(this IRuleBuilder<T, float> expression, float floor, float ceiling)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Float.Between<T>(floor, ceiling));
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