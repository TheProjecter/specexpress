using System.Collections.Generic;

namespace NSpecBuilder
{
    public interface IValidator<T>
    {
        IEnumerable<ValidationNotification> Validate(T instance);
    }

    public interface IValidator<T, TProperty> : IValidator<T>
    {
        IValidatorOptions<T, TProperty> SetValidator(IPropertyValidator<T, TProperty> validator);
    }

    public interface IValidatorOptions<T, TProperty>
    {
        IValidatorOptions<T, TProperty> WithMessage(string errorMessage);
    }
}