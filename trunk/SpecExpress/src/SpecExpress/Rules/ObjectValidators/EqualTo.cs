//using System;
//using System.Linq;
//using System.Linq.Expressions;

//namespace SpecExpress.Rules.ObjectValidators
//{
//    public class EqualTo<T> : RuleValidator<T, object>
//    {
//        private object _equalTo;

//        public EqualTo(object equalTo)
//        {
//            _equalTo = equalTo;
//        }

//        public EqualTo(Expression<Func<T, object>> expression)
//        {
//            SetPropertyExpression(expression);
//        }

//        public override ValidationResult Validate(RuleValidatorContext<T, object> context)
//        {
//            if (PropertyExpressions.Any())
//            {
//                _equalTo = GetExpressionValue(context);
//            }

//            return Evaluate(context.PropertyValue.Equals(_equalTo), context);
//        }

//        public override object[] Parameters
//        {
//            get { return new object[] { _equalTo }; }
//        }
//    }
//}