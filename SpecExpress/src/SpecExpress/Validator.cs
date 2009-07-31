using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NSpecBuilder
{
    public class Validator<T, TProperty> : IValidator<T,TProperty>
    {
        public Validator(Expression<Func<T,TProperty>> expression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ValidationNotification> Validate(T instance)
        {
            throw new NotImplementedException();
        }

        public IValidatorOptions<T, TProperty> SetValidator(IPropertyValidator<T, TProperty> validator)
        {
            throw new NotImplementedException();
        }
    }
}