using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SpecExpress.MessageStore;

namespace SpecExpress
{
    public static class ValidationCatalog
    {

        public static bool ValidateObjectGraph { get; set; }

        //public static IDictionary<Type, Specification> Registry = new Dictionary<Type, Specification>();

        public static ValidationCatalogConfiguration Configuration { get; private set;}

       
        public static  SpecificationContainer SpecificationContainer = new SpecificationContainer();

        private static object _syncLock = new object();

        static ValidationCatalog()
        {
            Configuration = buildDefaultValidationConfiguration();
        }

        /// <summary>
        /// Add Specifications dynamically without a SpecificationBase
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="rules"></param>
        public static void AddSpecification<TEntity>(Action<Validates<TEntity>> rules)
        {
            //Should these rules be "disposable"? ie, not added to registry?
            var specification = new SpecificationExpression<TEntity>();
            rules(specification);

            SpecificationContainer.Add(specification);
        }

        /// <summary>
        /// Configure the scanning of Assemblies containing Specifications used by Validate(object)
        /// </summary>
        /// <param name="configuration"></param>
        public static void Scan(Action<SpecificationScanner> configuration)
        {
            var specificationRegistry = new SpecificationScanner();
            configuration(specificationRegistry);

            SpecificationContainer.Add(specificationRegistry.FoundSpecifications);
        }
        
        public static void Configure(Action<ValidationCatalogConfiguration> action)
        {
                //Should these rules be "disposable"? ie, not added to registry?
                action(Configuration);
        }

        public static void Reset()
        {
            lock (_syncLock)
            {
                SpecificationContainer.Reset();
            }
        }

        public static void ResetConfiguration()
        {
            Configuration = buildDefaultValidationConfiguration();
        }

        

        public static void AssertConfigurationIsValid()
        {
            lock (_syncLock)
            {

                //Look for multiple specifications for a type where no default is defined.
                //TODO: Implement multispec check

                // RB 20091014: Allow a Property Validator with no rules defined to be valid (i.e. "Check(c => c.Name).Optional();" ).
                ////Look for PropertyValidators with no Rules
                //var invalidPropertyValidators = from r in _registry
                //                                from v in r.PropertyValidators
                //                                where v.Rules == null || !v.Rules.Any()
                //                                select
                //                                    r.GetType().Name + " is invalid because it has no rules defined for property '" +
                //                                    v.PropertyName + "'.";

                //if (invalidPropertyValidators.Any())
                //{
                //    var errorString = invalidPropertyValidators.Aggregate(string.Empty, (x, y) => x + "\n" + y);
                //    throw new SpecExpressConfigurationException(errorString);
                //}
            }
        }

        #region Object Validation

        /// <summary>
        /// Evaluate an object against it's matching Specification and returns any broken rules.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static ValidationNotification Validate(object instance)
        {
            //try to find a specification for the type
            Specification specification = SpecificationContainer.TryGetSpecification(instance.GetType());

            if (specification != null)
            {
                //Specification for this type found
                return Validate(instance, specification);
            }
            else
            {
                //No spec found for type, try for Collection
                if (instance is IEnumerable)
                {
                    return ValidateCollection((IEnumerable)instance, SpecificationContainer);
                }
                else
                {
                    //Unable to find specification, so call GetSpecification to generate an error message
                    SpecificationContainer.GetSpecification(instance.GetType());
                    return null;
                }
            }
        }

        public static ValidationNotification Validate(object instance, Specification specification)
        {
            //Guard for null
            if (instance == null)
            {
                throw new ArgumentNullException("Validate requires a non-null instance.");
            }

            //If the Specification and instance type match up the use them
            if ( specification.ForType == instance.GetType())
            {
                return new ValidationNotification { Errors = specification.Validate(instance, SpecificationContainer) };
            }

            //The Specification isn't for the same type as the instance, check if it's a collection of that type
            if (instance is IEnumerable)
            {
                return ValidateCollection((IEnumerable)instance, specification, SpecificationContainer);
            }
            
            throw new SpecExpressConfigurationException("Specification is invalid for the instance. Specification is for type " + specification.ForType.ToString() + " and instance is type " + instance.GetType().ToString() + "." );
        }

        public static ValidationNotification Validate<TSpec>(object instance) where TSpec : Specification, new()
        {
            var spec = new TSpec() as Specification;
            return Validate(instance, spec);
        }

        #region ValidationContext
        public static ValidationNotification ValidateContext(object instance, ValidationContext context)
        {
            //try to find a specification for the type
            Specification specification = context.SpecificationContainer.TryGetSpecification(instance.GetType());

            if (specification != null)
            {
                //Specification for this type found
                return Validate(instance, specification);
            }
            else
            {
                //No spec found for type, try for Collection
                if (instance is IEnumerable)
                {
                    return ValidateCollection((IEnumerable)instance, context.SpecificationContainer);
                }
                else
                {
                    //Unable to find specification, so call GetSpecification to generate an error message
                    context.SpecificationContainer.GetSpecification(instance.GetType());
                    return null;
                }
            }
        }

        public static ValidationNotification ValidateContext<TContext>(object instance) where TContext : ValidationContext, new()
        {
            var context = new TContext() as ValidationContext;
            return ValidateContext(instance, context);
        }
        #endregion

        private static ValidationNotification ValidateCollection(IEnumerable instance, SpecificationContainer specificationContainer)
        {
            //assume that the first item in the collection is the same for all items in the collection and get the specification for that type
            IEnumerator enumerator = ((IEnumerable)instance).GetEnumerator();

            //move to the first item in the collection if it's not empty
            if (enumerator.MoveNext())
            {
                var specification = specificationContainer.GetSpecification(enumerator.Current.GetType());
                return ValidateCollection((IEnumerable)instance, specification, specificationContainer);
            }
            else
            {
                //Collection was empty, return default ValidationNotification
                return new ValidationNotification();
            }
        }

        private static ValidationNotification ValidateCollection(IEnumerable instance, Specification specification, SpecificationContainer specificationContainer)
        {
            //Guard for null
            if (instance == null)
            {
                throw new ArgumentNullException("Validate requires a non-null instance.");
            }

            var collectionResult = new List<ValidationResult>();

            //Object being validated is a collection.
            //Check if the type in the collection has a Specification
            IEnumerator enumerator = instance.GetEnumerator();

            while (enumerator.MoveNext())
            {
                //validate the object with the given specification
                collectionResult.AddRange(specification.Validate(enumerator.Current, specificationContainer));
            }

            return new ValidationNotification { Errors = collectionResult };
            
        }

       

        #endregion

        #region Property Validation

        public static ValidationNotification ValidateProperty(object instance, string propertyName)
        {
            var specification = SpecificationContainer.TryGetSpecification(instance.GetType());

            return ValidateProperty(instance, propertyName, specification);
        }

        public static ValidationNotification ValidateProperty(object instance, string propertyName,
                                                              Specification specification)
        {
            var validators = from validator in specification.PropertyValidators
                             where validator.PropertyInfo.Name == propertyName
                             select validator;

            if (!validators.Any())
            {
                throw new ArgumentException(string.Format("There are no validation rules defined for {0}.{1}.", instance.GetType().FullName, propertyName));
            }

            var results =
                (from propertyValidator in validators
                 select propertyValidator.Validate(instance, SpecificationContainer))
                .SelectMany(x => x)
                .ToList();

            return new ValidationNotification() { Errors = results };
        }

        public static ValidationNotification ValidateProperty<T>(T instance, Expression<Func<T,object>> property)
        {
            Specification specification = SpecificationContainer.TryGetSpecification(typeof(T));

            return ValidateProperty(instance, property, specification);
        }

        public static ValidationNotification ValidateProperty<T>(T instance, Expression<Func<T, object>> property,
                                                              Specification specification)
        {
            var prop = new PropertyValidator<T, object>(property);

            return ValidateProperty(instance, prop.PropertyInfo.Name, specification);
           
        }

        #endregion

        private static ValidationCatalogConfiguration buildDefaultValidationConfiguration()
        {
            lock (_syncLock)
            {
                var config = new ValidationCatalogConfiguration()
                                                            {
                                                                DefaultMessageStore =
                                                                    new ResourceMessageStore(
                                                                    RuleErrorMessages.ResourceManager),
                                                                ValidateObjectGraph = false
                                                            };
                return config;
            }
        }

    }
}