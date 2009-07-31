using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NSpecBuilder
{
    public class ValidatorBuilder<T>
    {
        private List<IValidator<T>> _validators = new List<IValidator<T>>();

        public IValidator<T, TProperty> That<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            var validator = new Validator<T, TProperty>(expression);
            _validators.Add(validator);
            return validator;
        }
    }
}