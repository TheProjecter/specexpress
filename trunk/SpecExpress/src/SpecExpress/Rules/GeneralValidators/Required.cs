using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using SpecExpress.Util;

namespace SpecExpress.Rules.GeneralValidators
{
    public class Required<T, TProperty> : RuleValidator<T, TProperty>
    {
        public override object[] Parameters
        {
            get { return new object[] {}; }
        }

        public override ValidationResult Validate(RuleValidatorContext<T, TProperty> context)
        {
            //return Evaluate( 
            //    !(  context.PropertyValue == null
            //        || context.PropertyValue.Equals(string.Empty)
            //        || Equals(context.PropertyValue, default(TProperty))
            //        || !( !(context.PropertyValue is IEnumerable) || (context.PropertyValue is IEnumerable && ((IEnumerable)(context.PropertyValue)).GetEnumerator().MoveNext())))
            //    , context);

            return Evaluate(!context.PropertyValue.IsNullOrDefault(), context);
        }
    }


}