using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecExpress
{
    public static class ValidationContainer
    {
        private static IDictionary<Type, IValidatable> _registry = new Dictionary<Type, IValidatable>();
       
        public static void RulesForEntity<TEntity>(Action<SpecificationBase<TEntity>> rules)
        {
            var specification = new SpecificationExpression<TEntity>();
            rules(specification);

            RegisterSpecification(specification);
        }

        public static void Scan(Action<SpecificationRegistry> configuration)
        {
            var specificationRegistry = new SpecificationRegistry();
            configuration(specificationRegistry);

            _registry = specificationRegistry.FoundSpecifications;
        }

        public static ValidationNotification Validate(object instance)
        {
            Type typeArgs = instance.GetType();
           
            //Guard for null
            if (!_registry.Any())
            {
                throw  new ArgumentNullException("No Specifications found.");
            }

            var spec = _registry[typeArgs] as IValidatable;

            if (spec == null)
            {
                throw new ArgumentException("No Specification found for type " + typeArgs);
            }

            var notification = new ValidationNotification();
            notification.Errors.AddRange(spec.Validate(instance));

            return notification;
        }

        public static void RegisterSpecification<TEntity>(SpecificationBase<TEntity> expression)
        {
            _registry.Add(typeof(TEntity), expression);
        }
    }




}
