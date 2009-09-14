using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using SpecExpress.DSL;
using SpecExpress.Rules;
using SpecExpress.Rules.Collection;
using SpecExpress.Rules.DateValidators;
using SpecExpress.Rules.GeneralValidators;
using SpecExpress.Rules.IComparableValidators;
using SpecExpress.Rules.StringValidators;

namespace SpecExpress
{
    /// <summary>
    /// Changed return from RuleBuilder to ActionJoin so displays AND/WITH after a Rule.
    /// </summary>
    public static class CoreValidatorExtensions
    {

        #region DateTime
        public static ActionJoinBuilder<T, DateTime> IsInFuture<T>(this IRuleBuilder<T, DateTime> expression)
        {
            expression.RegisterValidator(new Rules.DateValidators.IsInFuture<T>());
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, DateTime> IsInPast<T>(this IRuleBuilder<T, DateTime> expression)
        {
            expression.RegisterValidator(new Rules.DateValidators.IsInPast<T>());
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

        public static ActionJoinBuilder<T, string> LengthBetween<T>(this IRuleBuilder<T, string> expression, Expression<Func<T, int>> min,
                                                            int max)
        {
            expression.RegisterValidator(new LengthBetween<T>(min, max));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, string> LengthBetween<T>(this IRuleBuilder<T, string> expression, int min,
                                                            Expression<Func<T, int>> max)
        {
            expression.RegisterValidator(new LengthBetween<T>(min, max));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, string> LengthBetween<T>(this IRuleBuilder<T, string> expression, Expression<Func<T, int>> min,
                                                            Expression<Func<T, int>> max)
        {
            expression.RegisterValidator(new LengthBetween<T>(min, max));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, string> MinLength<T>(this IRuleBuilder<T, string> expression, int min)
        {
            expression.RegisterValidator(new MinLength<T>(min));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, string> MinLength<T>(this IRuleBuilder<T, string> expression, Expression<Func<T, int>> min)
        {
            expression.RegisterValidator(new MinLength<T>(min));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, string> MaxLength<T>(this IRuleBuilder<T, string> expression, int max)
        {
            expression.RegisterValidator(new MaxLength<T>(max));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, string> MaxLength<T>(this IRuleBuilder<T, string> expression, Expression<Func<T,int>> max)
        {
            expression.RegisterValidator(new MaxLength<T>(max));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, string> Matches<T>(this IRuleBuilder<T, string> expression, string regexPattern)
        {
            expression.RegisterValidator(new Matches<T>(regexPattern));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, string> Matches<T>(this IRuleBuilder<T, string> expression, Expression<Func<T,string>> regexPattern)
        {
            expression.RegisterValidator(new Matches<T>(regexPattern));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, string> MaxLength<T>(this IRuleBuilder<T, string> expression, Expression<Func<T, string>> regexPattern)
        {
            expression.RegisterValidator(new Matches<T>(regexPattern));
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
        #endregion

        #region Collection
        public static ActionJoinBuilder<T, TProperty> Contains<T, TProperty>(this IRuleBuilder<T, TProperty> expression, object valueToLookFor) where TProperty : IEnumerable
        {
            expression.RegisterValidator(new Rules.Collection.Contains<T, TProperty>(valueToLookFor));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, TProperty> Contains<T, TProperty>(this IRuleBuilder<T, TProperty> expression, Expression<Func<T, IEnumerable>> valueToLookFor) where TProperty : IEnumerable
        {
            expression.RegisterValidator(new Rules.Collection.Contains<T, TProperty>(valueToLookFor));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, TProperty> ForEach<T, TProperty>(this IRuleBuilder<T, TProperty> expression, Predicate<object> predicate, string messageTemplate) where TProperty : IEnumerable
        {
            expression.RegisterValidator(new Rules.Collection.ForEach<T, TProperty>(predicate, messageTemplate));
            return expression.JoinBuilder;
        }

        #endregion

        #region IComparable
        public static ActionJoinBuilder<T, TProperty> GreaterThan<T, TProperty>(this IRuleBuilder<T, TProperty> expression, TProperty greaterThan) where TProperty : IComparable
        {
            expression.RegisterValidator(new Rules.IComparableValidators.GreaterThan<T, TProperty>(greaterThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, TProperty> GreaterThan<T, TProperty>(this IRuleBuilder<T, TProperty> expression, Expression<Func<T, TProperty>> greaterThan) where TProperty : IComparable
        {
            expression.RegisterValidator(new Rules.IComparableValidators.GreaterThan<T, TProperty>(greaterThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, TProperty> GreaterThanEqualTo<T, TProperty>(this IRuleBuilder<T, TProperty> expression, TProperty greaterThanEqualTo) where TProperty : IComparable
        {
            expression.RegisterValidator(new Rules.IComparableValidators.GreaterThanEqualTo<T, TProperty>(greaterThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, TProperty> GreaterThanEqualTo<T, TProperty>(this IRuleBuilder<T, TProperty> expression, Expression<Func<T, TProperty>> greaterThanEqualTo) where TProperty : IComparable
        {
            expression.RegisterValidator(new Rules.IComparableValidators.GreaterThanEqualTo<T, TProperty>(greaterThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, TProperty> LessThan<T, TProperty>(this IRuleBuilder<T, TProperty> expression, TProperty lessThan) where TProperty : IComparable
        {
            expression.RegisterValidator(new Rules.IComparableValidators.LessThan<T, TProperty>(lessThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, TProperty> LessThan<T, TProperty>(this IRuleBuilder<T, TProperty> expression, Expression<Func<T, TProperty>> lessThan) where TProperty : IComparable
        {
            expression.RegisterValidator(new Rules.IComparableValidators.LessThan<T, TProperty>(lessThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, TProperty> LessThanEqualTo<T, TProperty>(this IRuleBuilder<T, TProperty> expression, TProperty lessThanEqualTo) where TProperty : IComparable
        {
            expression.RegisterValidator(new Rules.IComparableValidators.LessThanEqualTo<T, TProperty>(lessThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, TProperty> LessThanEqualTo<T, TProperty>(this IRuleBuilder<T, TProperty> expression, Expression<Func<T, TProperty>> lessThanEqualTo) where TProperty : IComparable
        {
            expression.RegisterValidator(new Rules.IComparableValidators.LessThanEqualTo<T, TProperty>(lessThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, TProperty> EqualTo<T, TProperty>(this IRuleBuilder<T, TProperty> expression, TProperty equalTo) where TProperty : IComparable
        {
            expression.RegisterValidator(new Rules.IComparableValidators.EqualTo<T, TProperty>(equalTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, TProperty> EqualTo<T, TProperty>(this IRuleBuilder<T, TProperty> expression, Expression<Func<T, TProperty>> equalTo) where TProperty : IComparable
        {
            expression.RegisterValidator(new Rules.IComparableValidators.EqualTo<T, TProperty>(equalTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, TProperty> Between<T, TProperty>(this IRuleBuilder<T, TProperty> expression, TProperty floor, TProperty ceiling) where TProperty : IComparable
        {
            expression.RegisterValidator(new Between<T, TProperty>(floor, ceiling));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, TProperty> Between<T, TProperty>(this IRuleBuilder<T, TProperty> expression, Expression<Func<T, TProperty>> floor, TProperty ceiling) where TProperty : IComparable
        {
            expression.RegisterValidator(new Between<T, TProperty>(floor, ceiling));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, TProperty> Between<T, TProperty>(this IRuleBuilder<T, TProperty> expression, TProperty floor, Expression<Func<T, TProperty>> ceiling) where TProperty : IComparable
        {
            expression.RegisterValidator(new Between<T, TProperty>(floor, ceiling));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, TProperty> Between<T, TProperty>(this IRuleBuilder<T, TProperty> expression, Expression<Func<T, TProperty>> floor, Expression<Func<T, TProperty>> ceiling) where TProperty : IComparable
        {
            expression.RegisterValidator(new Between<T, TProperty>(floor, ceiling));
            return expression.JoinBuilder;
        }
        #endregion

        #region Boolean
        public static ActionJoinBuilder<T, bool> IsTrue<T>(this IRuleBuilder<T, bool> expression)
        {
            expression.RegisterValidator(new Rules.Boolean.IsTrue<T>());
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, bool> IsFalse<T>(this IRuleBuilder<T, bool> expression)
        {
            expression.RegisterValidator(new Rules.Boolean.IsFalse<T>());
            return expression.JoinBuilder;
        }
        #endregion

        #region Custom
        public static ActionJoinBuilder<T, TProperty> Expect<T, TProperty>(this IRuleBuilder<T, TProperty> expression, Func<T, TProperty, bool> rule, string message)
        {
            expression.RegisterValidator(new CustomRule<T, TProperty>(rule));
            //Custom messages can't derive what the Error Message is because each case is so generic
            expression.JoinBuilder.With.Message(message);
            return expression.JoinBuilder;
        }
        #endregion

        #region General
        public static ActionJoinBuilder<T, TProperty> IsInSet<T, TProperty>(this IRuleBuilder<T, TProperty> expression, IEnumerable<TProperty> set)
        {
            expression.RegisterValidator(new IsInSet<T, TProperty>(set));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, TProperty> IsInSet<T, TProperty>(this IRuleBuilder<T, TProperty> expression, Expression<Func<T, IEnumerable<TProperty>>> set)
        {
            expression.RegisterValidator(new IsInSet<T, TProperty>(set));
            return expression.JoinBuilder;
        }
        #endregion

    }
}