using System;
using SpecExpress.DSL;

namespace SpecExpress.Rules.DateValidators
{
    public static class DateRuleExtensions
    {
        public static ActionJoinBuilder<T, DateTime> IsBefore<T>(this IRuleBuilder<T, DateTime> expression,
                                                                 DateTime beforeDate)
        {
            expression.RegisterValidator(new IsBefore<T>(beforeDate));
            return expression.JoinBuilder;
        }

        public static ActionJoinBuilder<T, DateTime> IsAfter<T>(this IRuleBuilder<T, DateTime> expression,
                                                                DateTime afterDate)
        {
            expression.RegisterValidator(new IsAfter<T>(afterDate));
            return expression.JoinBuilder;
        }
    }
}