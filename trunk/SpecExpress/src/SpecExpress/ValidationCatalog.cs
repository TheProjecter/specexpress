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

        private static IList<Specification> _registry = new List<Specification>();

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

            RegisterSpecification(specification);
        }

        /// <summary>
        /// Configure the scanning of Assemblies containing Specifications used by Validate(object)
        /// </summary>
        /// <param name="configuration"></param>
        public static void Scan(Action<SpecificationScanner> configuration)
        {
            var specificationRegistry = new SpecificationScanner();
            configuration(specificationRegistry);

            CreateAndRegisterSpecificationsWithRegistry(specificationRegistry.FoundSpecifications);
        }

        /// <summary>
        /// Scans AppDomain For Specifications
        /// </summary>
        public static void Scan()
        {
            var specificationRegistry = new SpecificationScanner();
            specificationRegistry.AddAssembliesFromAppDomain();
            CreateAndRegisterSpecificationsWithRegistry(specificationRegistry.FoundSpecifications);
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
                _registry.Clear();
            }
        }

        public static void ResetConfiguration()
        {
            Configuration = buildDefaultValidationConfiguration();
        }

        public static void RegisterSpecification<TEntity>(Validates<TEntity> expression)
        {
            RegisterSpecificationWithRegistry(expression);
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
            Specification specification = TryGetSpecification(instance.GetType());

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
                    return ValidateCollection((IEnumerable)instance);
                }
                else
                {
                    //Unable to find specification, so call GetSpecification to generate an error message
                    GetSpecification(instance.GetType());
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
                return new ValidationNotification { Errors = specification.Validate(instance) };
            }

            //The Specification isn't for the same type as the instance, check if it's a collection of that type
            if (instance is IEnumerable)
            {
                return ValidateCollection((IEnumerable)instance, specification);
            }
            
            throw new SpecExpressConfigurationException("Specification is invalid for the instance. Specification is for type " + specification.ForType.ToString() + " and instance is type " + instance.GetType().ToString() + "." );
        }

        private static ValidationNotification ValidateCollection(IEnumerable instance)
        {
            //assume that the first item in the collection is the same for all items in the collection and get the specification for that type
            IEnumerator enumerator = ((IEnumerable)instance).GetEnumerator();

            //move to the first item in the collection if it's not empty
            if (enumerator.MoveNext())
            {
                var specification = GetSpecification(enumerator.Current.GetType());
                return ValidateCollection((IEnumerable)instance, specification);
            }
            else
            {
                //Collection was empty, return default ValidationNotification
                return new ValidationNotification();
            }
        }

        private static ValidationNotification ValidateCollection(IEnumerable instance, Specification specification)
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
                collectionResult.AddRange(specification.Validate(enumerator.Current));
            }

            return new ValidationNotification { Errors = collectionResult };
            
        }

        public static ValidationNotification Validate<TSpec>(object instance) where TSpec : new()
        {
            var spec = new TSpec() as Specification;
            return Validate(instance, spec);
        }

        #endregion

        #region Property Validation

        public static ValidationNotification ValidateProperty(object instance, string propertyName)
        {
            Specification specification = TryGetSpecification(instance.GetType());

            return ValidateProperty(instance, propertyName, specification);
        }

        public static ValidationNotification ValidateProperty(object instance, string propertyName,
                                                              Specification specification)
        {
            var validators = from validator in specification.PropertyValidators
                             where validator.PropertyName == propertyName
                             select validator;

            if (!validators.Any())
            {
                throw new ArgumentException(string.Format("There are not any validation rules defined for {0}.{1}.", instance.GetType().FullName, propertyName));
            }

            var results =
                (from propertyValidator in validators
                 select propertyValidator.Validate(instance))
                .SelectMany(x => x)
                .ToList();

            return new ValidationNotification() { Errors = results };
        }

        public static ValidationNotification ValidateProperty<T>(T instance, Expression<Func<T,object>> property)
        {
            Specification specification = TryGetSpecification(typeof(T));

            return ValidateProperty(instance, property, specification);
        }

        public static ValidationNotification ValidateProperty<T>(T instance, Expression<Func<T, object>> property,
                                                              Specification specification)
        {
            var prop = new PropertyValidator<T, object>(property);

            return ValidateProperty(instance, prop.PropertyName, specification);
           
        }

        #endregion

        #region Container

        public static Specification TryGetSpecification(Type type)
        {
            lock (_syncLock)
            {

                var specs = from r in _registry
                            where r.ForType == type
                            select r;

                //No Specs found for type
                if (!specs.ToList().Any())
                {
                    return null;
                }

                //If more than one spec was found for type
                if (specs.ToList().Count > 1)
                {
                    //try to return the default
                    var defaultSpecs = from s in specs
                                       where s.DefaultForType
                                       select s;

                    //No default specs defined
                    if (!defaultSpecs.Any())
                    {
                        throw new SpecExpressConfigurationException("Multiple Specifications found and none are defined as default.");
                    }

                    //Multiple specs defined as Default
                    if (defaultSpecs.Count() > 1)
                    {
                        throw new SpecExpressConfigurationException("Multiple Specifications found and multiple are defined as default.");
                    }

                    return defaultSpecs.First();
                }

                return specs.First();
            }
        }

        public static Specification GetSpecification(Type type)
        {
            var spec = TryGetSpecification(type);
            if (spec == null)
            {
                throw new SpecExpressConfigurationException("No Specification for type " + type + " was found.");
            }

            return spec;
        }

        public static Validates<TType> GetSpecification<TType>()
        {
            return GetSpecification(typeof(TType)) as Validates<TType>;
        }

        public static Validates<TType> TryGetSpecification<TType>()
        {
            return TryGetSpecification(typeof(TType)) as Validates<TType>;
        }

        public static IList<Specification> GetAllSpecifications()
        {
            // For thread safety, return a copy of the registry
            return new List<Specification>(_registry);
        }

        #endregion

        #region Registration

        private static void CreateAndRegisterSpecificationsWithRegistry(IEnumerable<Type> specs)
        {
            int counter = 0;
            int max = 10;

            var delayedSpecs = CreateAndRegisterSpecificationsWithRegistryIterator(specs, counter, max);

            while (delayedSpecs.Any())
            {
                counter++;
                delayedSpecs = CreateAndRegisterSpecificationsWithRegistryIterator(specs, counter, max);
            }

        }

        private static List<Type> CreateAndRegisterSpecificationsWithRegistryIterator(IEnumerable<Type> specs, int counter, int max)
        {
            //TODO: This can result in a stackoverflow if a ForEachSpecification<Type> never finds a default spec for Type

            var delayedSpecs = new List<Type>();
          

            //For each type, instantiate it and add it to the collection of specs found
            specs.ToList<Type>().ForEach(spec =>
            {
                // Prevent two of the same specification from being registered
                if (! (from specification in _registry where specification.GetType().FullName == spec.FullName select specification).Any())
                {
                    try
                    {
                        var s = Activator.CreateInstance(spec) as Specification;

                        RegisterSpecificationWithRegistry(s);
                    }
                    catch (System.Reflection.TargetInvocationException te)
                    {
                        if (counter > max)
                        {
                            throw new SpecExpressConfigurationException(
                                string.Format("Exception thrown while trying to register {0}.", spec.FullName), te);
                        }
                        else
                        {
                            //Can't create the object because it has a specification that hasn't been loaded yet
                            //save it for the next pass
                            delayedSpecs.Add(spec);

                        }
                    }
                    catch (Exception err)
                    {
                        throw new SpecExpressConfigurationException(
                          string.Format("Exception thrown while trying to register {0}.", spec.FullName), err);
                    }
                }
            });

            return delayedSpecs;

            //Process any specification that couldn't be reloaded
            //if (delayedSpecs.Any())
            //{
            //    CreateAndRegisterSpecificationsWithRegistry(delayedSpecs);
            //}
        }

        private static void RegisterSpecificationWithRegistry(Specification spec)
        {
            lock (_syncLock)
            {

                if (spec != null)
                {
                    _registry.Add(spec);
                }
            }
        }

        private static ValidationCatalogConfiguration buildDefaultValidationConfiguration()
        {
            lock (_syncLock)
            {
                ValidationCatalogConfiguration config = new ValidationCatalogConfiguration()
                                                            {
                                                                DefaultMessageStore =
                                                                    new ResourceMessageStore(
                                                                    RuleErrorMessages.ResourceManager),
                                                                ValidateObjectGraph = false
                                                            };
                return config;
            }
        }

        #endregion

    }
}