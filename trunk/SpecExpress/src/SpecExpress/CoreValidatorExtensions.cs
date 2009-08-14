using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using SpecExpress.DSL;
using SpecExpress.Rules;
using SpecExpress.Rules.DateValidators;
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

        public static ActionJoinBuilder<T, int> GreaterThan<T>(this IRuleBuilder<T, int> expression, Expression<Func<T, int>> greaterThan)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Int.GreaterThan<T>(greaterThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, int> LessThan<T>(this IRuleBuilder<T, int> expression, int lessThan)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Int.LessThan<T>(lessThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, int> LessThan<T>(this IRuleBuilder<T, int> expression, Expression<Func<T, int>> lessThan)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Int.LessThan<T>(lessThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, int> GreaterThanEqualTo<T>(this IRuleBuilder<T, int> expression, int greaterThanEqualTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Int.GreaterThanEqualTo<T>(greaterThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, int> GreaterThanEqualTo<T>(this IRuleBuilder<T, int> expression, Expression<Func<T, int>> greaterThanEqualTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Int.GreaterThanEqualTo<T>(greaterThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, int> LessThanEqualTo<T>(this IRuleBuilder<T, int> expression, int lessThanEqualTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Int.LessThanEqualTo<T>(lessThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, int> LessThanEqualTo<T>(this IRuleBuilder<T, int> expression, Expression<Func<T, int>> lessThanEqualTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Int.LessThanEqualTo<T>(lessThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, int> Equals<T>(this IRuleBuilder<T, int> expression, int equalTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Int.EqualTo<T>(equalTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, int> Equals<T>(this IRuleBuilder<T, int> expression, Expression<Func<T, int>> equalTo )
        {
            expression.RegisterValidator(new Rules.NumericValidators.Int.EqualTo<T>(equalTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, int> Between<T>(this IRuleBuilder<T, int> expression, int floor, int ceiling)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Int.Between<T>(floor, ceiling));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, int> Between<T>(this IRuleBuilder<T, int> expression, Expression<Func<T, int>> floor, int ceiling)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Int.Between<T>(floor, ceiling));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, int> Between<T>(this IRuleBuilder<T, int> expression, int floor, Expression<Func<T, int>> ceiling)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Int.Between<T>(floor, ceiling));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, int> Between<T>(this IRuleBuilder<T, int> expression, Expression<Func<T, int>> floor, Expression<Func<T, int>> ceiling)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Int.Between<T>(floor, ceiling));
            return expression.JoinBuilder;
        }

        #endregion

        #region Long
        public static ActionJoinBuilder<T, long> GreaterThan<T>(this IRuleBuilder<T, long> expression, long greaterThan)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Long.GreaterThan<T>(greaterThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, long> GreaterThan<T>(this IRuleBuilder<T, long> expression, Expression<Func<T, long>> greaterThan)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Long.GreaterThan<T>(greaterThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, long> LessThan<T>(this IRuleBuilder<T, long> expression, long lessThan)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Long.LessThan<T>(lessThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, long> LessThan<T>(this IRuleBuilder<T, long> expression, Expression<Func<T, long>> lessThan)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Long.LessThan<T>(lessThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, long> GreaterThanEqualTo<T>(this IRuleBuilder<T, long> expression, long greaterThanEqualTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Long.GreaterThanEqualTo<T>(greaterThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, long> GreaterThanEqualTo<T>(this IRuleBuilder<T, long> expression, Expression<Func<T, long>> greaterThanEqualTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Long.GreaterThanEqualTo<T>(greaterThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, long> LessThanEqualTo<T>(this IRuleBuilder<T, long> expression, long lessThanEqualTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Long.LessThanEqualTo<T>(lessThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, long> LessThanEqualTo<T>(this IRuleBuilder<T, long> expression, Expression<Func<T, long>> lessThanEqualTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Long.LessThanEqualTo<T>(lessThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, long> Equals<T>(this IRuleBuilder<T, long> expression, Expression<Func<T, long>> equalTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Long.EqualTo<T>(equalTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, long> Equals<T>(this IRuleBuilder<T, long> expression, long equalTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Long.EqualTo<T>(equalTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, long> Between<T>(this IRuleBuilder<T, long> expression, long floor, long ceiling)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Long.Between<T>(floor, ceiling));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, long> Between<T>(this IRuleBuilder<T, long> expression, Expression<Func<T, long>> floor, long ceiling)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Long.Between<T>(floor, ceiling));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, long> Between<T>(this IRuleBuilder<T, long> expression, long floor, Expression<Func<T, long>> ceiling)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Long.Between<T>(floor, ceiling));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, long> Between<T>(this IRuleBuilder<T, long> expression, Expression<Func<T, long>> floor, Expression<Func<T, long>> ceiling)
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

        public static ActionJoinBuilder<T, short> GreaterThan<T>(this IRuleBuilder<T, short> expression, Expression<Func<T, short>> greaterThan)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Short.GreaterThan<T>(greaterThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, short> LessThan<T>(this IRuleBuilder<T, short> expression, short lessThan)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Short.LessThan<T>(lessThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, short> LessThan<T>(this IRuleBuilder<T, short> expression, Expression<Func<T, short>> lessThan)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Short.LessThan<T>(lessThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, short> GreaterThanEqualTo<T>(this IRuleBuilder<T, short> expression, short greaterThanEqualTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Short.GreaterThanEqualTo<T>(greaterThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, short> GreaterThanEqualTo<T>(this IRuleBuilder<T, short> expression, Expression<Func<T, short>> greaterThanEqualTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Short.GreaterThanEqualTo<T>(greaterThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, short> LessThanEqualTo<T>(this IRuleBuilder<T, short> expression, short lessThanEqualTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Short.LessThanEqualTo<T>(lessThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, short> LessThanEqualTo<T>(this IRuleBuilder<T, short> expression, Expression<Func<T, short>> lessThanEqualTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Short.LessThanEqualTo<T>(lessThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, short> Equals<T>(this IRuleBuilder<T, short> expression, Expression<Func<T, short>> equalTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Short.EqualTo<T>(equalTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, short> Equals<T>(this IRuleBuilder<T, short> expression, short equalTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Short.EqualTo<T>(equalTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, short> Between<T>(this IRuleBuilder<T, short> expression, short floor, short ceiling)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Short.Between<T>(floor, ceiling));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, short> Between<T>(this IRuleBuilder<T, short> expression, Expression<Func<T, short>> floor, short ceiling)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Short.Between<T>(floor, ceiling));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, short> Between<T>(this IRuleBuilder<T, short> expression, short floor, Expression<Func<T, short>> ceiling)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Short.Between<T>(floor, ceiling));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, short> Between<T>(this IRuleBuilder<T, short> expression, Expression<Func<T, short>> floor, Expression<Func<T, short>> ceiling)
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

        public static ActionJoinBuilder<T, float> GreaterThan<T>(this IRuleBuilder<T, float> expression, Expression<Func<T, float>> greaterThan)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Float.GreaterThan<T>(greaterThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, float> LessThan<T>(this IRuleBuilder<T, float> expression, float lessThan)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Float.LessThan<T>(lessThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, float> LessThan<T>(this IRuleBuilder<T, float> expression, Expression<Func<T, float>> lessThan)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Float.LessThan<T>(lessThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, float> GreaterThanEqualTo<T>(this IRuleBuilder<T, float> expression, float greaterThanEqualTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Float.GreaterThanEqualTo<T>(greaterThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, float> GreaterThanEqualTo<T>(this IRuleBuilder<T, float> expression, Expression<Func<T, float>> greaterThanEqualTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Float.GreaterThanEqualTo<T>(greaterThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, float> LessThanEqualTo<T>(this IRuleBuilder<T, float> expression, float lessThanEqualTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Float.LessThanEqualTo<T>(lessThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, float> LessThanEqualTo<T>(this IRuleBuilder<T, float> expression, Expression<Func<T, float>> lessThanEqualTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Float.LessThanEqualTo<T>(lessThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, float> Equals<T>(this IRuleBuilder<T, float> expression, Expression<Func<T, float>> equalTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Float.EqualTo<T>(equalTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, float> Equals<T>(this IRuleBuilder<T, float> expression, float equalTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Float.EqualTo<T>(equalTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, float> Between<T>(this IRuleBuilder<T, float> expression, float floor, float ceiling)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Float.Between<T>(floor, ceiling));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, float> Between<T>(this IRuleBuilder<T, float> expression, Expression<Func<T, float>> floor, float ceiling)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Float.Between<T>(floor, ceiling));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, float> Between<T>(this IRuleBuilder<T, float> expression, float floor, Expression<Func<T, float>> ceiling)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Float.Between<T>(floor, ceiling));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, float> Between<T>(this IRuleBuilder<T, float> expression, Expression<Func<T, float>> floor, Expression<Func<T, float>> ceiling)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Float.Between<T>(floor, ceiling));
            return expression.JoinBuilder;
        }

        #endregion

        #region Double
        public static ActionJoinBuilder<T, double> GreaterThan<T>(this IRuleBuilder<T, double> expression, double greaterThan)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Double.GreaterThan<T>(greaterThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, double> GreaterThan<T>(this IRuleBuilder<T, double> expression, Expression<Func<T, double>> greaterThan)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Double.GreaterThan<T>(greaterThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, double> LessThan<T>(this IRuleBuilder<T, double> expression, double lessThan)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Double.LessThan<T>(lessThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, double> LessThan<T>(this IRuleBuilder<T, double> expression, Expression<Func<T, double>> lessThan)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Double.LessThan<T>(lessThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, double> GreaterThanEqualTo<T>(this IRuleBuilder<T, double> expression, double greaterThanEqualTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Double.GreaterThanEqualTo<T>(greaterThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, double> GreaterThanEqualTo<T>(this IRuleBuilder<T, double> expression, Expression<Func<T, double>> greaterThanEqualTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Double.GreaterThanEqualTo<T>(greaterThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, double> LessThanEqualTo<T>(this IRuleBuilder<T, double> expression, double lessThanEqualTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Double.LessThanEqualTo<T>(lessThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, double> LessThanEqualTo<T>(this IRuleBuilder<T, double> expression, Expression<Func<T, double>> lessThanEqualTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Double.LessThanEqualTo<T>(lessThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, double> Equals<T>(this IRuleBuilder<T, double> expression, Expression<Func<T, double>> equalTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Double.EqualTo<T>(equalTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, double> Equals<T>(this IRuleBuilder<T, double> expression, double equalTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Double.EqualTo<T>(equalTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, double> Between<T>(this IRuleBuilder<T, double> expression, double floor, double ceiling)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Double.Between<T>(floor, ceiling));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, double> Between<T>(this IRuleBuilder<T, double> expression, Expression<Func<T, double>> floor, double ceiling)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Double.Between<T>(floor, ceiling));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, double> Between<T>(this IRuleBuilder<T, double> expression, double floor, Expression<Func<T, double>> ceiling)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Double.Between<T>(floor, ceiling));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, double> Between<T>(this IRuleBuilder<T, double> expression, Expression<Func<T, double>> floor, Expression<Func<T, double>> ceiling)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Double.Between<T>(floor, ceiling));
            return expression.JoinBuilder;
        }

        #endregion

        #region Decimal
        public static ActionJoinBuilder<T, decimal> GreaterThan<T>(this IRuleBuilder<T, decimal> expression, decimal greaterThan)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Decimal.GreaterThan<T>(greaterThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, decimal> GreaterThan<T>(this IRuleBuilder<T, decimal> expression, Expression<Func<T, decimal>> greaterThan)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Decimal.GreaterThan<T>(greaterThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, decimal> LessThan<T>(this IRuleBuilder<T, decimal> expression, decimal lessThan)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Decimal.LessThan<T>(lessThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, decimal> LessThan<T>(this IRuleBuilder<T, decimal> expression, Expression<Func<T, decimal>> lessThan)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Decimal.LessThan<T>(lessThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, decimal> GreaterThanEqualTo<T>(this IRuleBuilder<T, decimal> expression, decimal greaterThanEqualTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Decimal.GreaterThanEqualTo<T>(greaterThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, decimal> GreaterThanEqualTo<T>(this IRuleBuilder<T, decimal> expression, Expression<Func<T, decimal>> greaterThanEqualTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Decimal.GreaterThanEqualTo<T>(greaterThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, decimal> LessThanEqualTo<T>(this IRuleBuilder<T, decimal> expression, decimal lessThanEqualTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Decimal.LessThanEqualTo<T>(lessThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, decimal> LessThanEqualTo<T>(this IRuleBuilder<T, decimal> expression, Expression<Func<T, decimal>> lessThanEqualTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Decimal.LessThanEqualTo<T>(lessThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, decimal> Equals<T>(this IRuleBuilder<T, decimal> expression, Expression<Func<T, decimal>> equalTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Decimal.EqualTo<T>(equalTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, decimal> Equals<T>(this IRuleBuilder<T, decimal> expression, decimal equalTo)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Decimal.EqualTo<T>(equalTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, decimal> Between<T>(this IRuleBuilder<T, decimal> expression, decimal floor, decimal ceiling)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Decimal.Between<T>(floor, ceiling));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, decimal> Between<T>(this IRuleBuilder<T, decimal> expression, Expression<Func<T, decimal>> floor, decimal ceiling)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Decimal.Between<T>(floor, ceiling));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, decimal> Between<T>(this IRuleBuilder<T, decimal> expression, decimal floor, Expression<Func<T, decimal>> ceiling)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Decimal.Between<T>(floor, ceiling));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, decimal> Between<T>(this IRuleBuilder<T, decimal> expression, Expression<Func<T, decimal>> floor, Expression<Func<T, decimal>> ceiling)
        {
            expression.RegisterValidator(new Rules.NumericValidators.Decimal.Between<T>(floor, ceiling));
            return expression.JoinBuilder;
        }

        #endregion

        #region DateTime
        public static ActionJoinBuilder<T, DateTime> GreaterThan<T>(this IRuleBuilder<T, DateTime> expression,
                                                         DateTime greaterThan)
        {
            expression.RegisterValidator(new Rules.DateValidators.GreaterThan<T>(greaterThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, DateTime> GreaterThan<T>(this IRuleBuilder<T, DateTime> expression,
                                                 Expression<Func<T, DateTime>> greaterThan)
        {
            expression.RegisterValidator(new Rules.DateValidators.GreaterThan<T>(greaterThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, DateTime> GreaterThanEqualTo<T>(this IRuleBuilder<T, DateTime> expression,
                                                 DateTime greaterThanEqualTo)
        {
            expression.RegisterValidator(new Rules.DateValidators.GreaterThanEqualTo<T>(greaterThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, DateTime> GreaterThanEqualTo<T>(this IRuleBuilder<T, DateTime> expression,
                                         Expression<Func<T, DateTime>> greaterThanEqualTo)
        {
            expression.RegisterValidator(new Rules.DateValidators.GreaterThanEqualTo<T>(greaterThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, DateTime> LessThan<T>(this IRuleBuilder<T, DateTime> expression,
                                         DateTime lessThan)
        {
            expression.RegisterValidator(new Rules.DateValidators.LessThan<T>(lessThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, DateTime> LessThan<T>(this IRuleBuilder<T, DateTime> expression,
                                 Expression<Func<T, DateTime>> lessThan)
        {
            expression.RegisterValidator(new Rules.DateValidators.LessThan<T>(lessThan));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, DateTime> LessThanEqualTo<T>(this IRuleBuilder<T, DateTime> expression,
                                 DateTime lessThanEqualTo)
        {
            expression.RegisterValidator(new Rules.DateValidators.LessThanEqualTo<T>(lessThanEqualTo));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, DateTime> LessThanEqualTo<T>(this IRuleBuilder<T, DateTime> expression,
                         Expression<Func<T, DateTime>> lessThanEqualTo)
        {
            expression.RegisterValidator(new Rules.DateValidators.LessThanEqualTo<T>(lessThanEqualTo));
            return expression.JoinBuilder;
        }

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

        public static ActionJoinBuilder<T, DateTime> Between<T>(this IRuleBuilder<T, DateTime> expression,
            DateTime floor, DateTime ceiling)
        {
            expression.RegisterValidator(new Rules.DateValidators.Between<T>(floor, ceiling));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, DateTime> Between<T>(this IRuleBuilder<T, DateTime> expression,
            Expression<Func<T, DateTime>> floor, DateTime ceiling)
        {
            expression.RegisterValidator(new Rules.DateValidators.Between<T>(floor, ceiling));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, DateTime> Between<T>(this IRuleBuilder<T, DateTime> expression,
            DateTime floor, Expression<Func<T, DateTime>> ceiling)
        {
            expression.RegisterValidator(new Rules.DateValidators.Between<T>(floor, ceiling));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, DateTime> Between<T>(this IRuleBuilder<T, DateTime> expression,
            Expression<Func<T, DateTime>> floor, Expression<Func<T, DateTime>> ceiling)
        {
            expression.RegisterValidator(new Rules.DateValidators.Between<T>(floor, ceiling));
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

        #region Collection
        public static ActionJoinBuilder<T, IEnumerable> Contains<T>(this IRuleBuilder<T, IEnumerable> expression, object valueToLookFor)
        {
            expression.RegisterValidator(new Rules.Collection.Contains<T>(valueToLookFor));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, IEnumerable> Contains<T>(this IRuleBuilder<T, IEnumerable> expression, Expression<Func<T, IEnumerable>> valueToLookFor)
        {
            expression.RegisterValidator(new Rules.Collection.Contains<T>(valueToLookFor));
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

        public static ActionJoinBuilder<T, string> Expects<T>(this IRuleBuilder<T, string> expression, Func<string, bool> rule, string message)
        {
            expression.RegisterValidator(new CustomRule<T>(rule));
            //Custom messages can't derive what the Error Message is because each case is so generic
            expression.JoinBuilder.With.Message(message);
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
            expression.RegisterValidator(new LengthBetween<T>(min, max));
            return expression.JoinBuilder;
        }
    }
}