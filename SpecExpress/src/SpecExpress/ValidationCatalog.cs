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

        public static ValidationCatalogConfiguration Configuration = buildDefaultValidationConfiguration();

        private static IList<Specification> _registry = new List<Specification>();

        /// <summary>
        /// Add Specifications dynamically without a SpecificationBase
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="rules"></param>
        public static void AddSpecification<TEntity>(Action<SpecificationBase<TEntity>> rules)
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

        public static void Configure(Action<ValidationCatalogConfiguration> action)
        {
            //Should these rules be "disposable"? ie, not added to registry?
            action(Configuration);
            
        }


        /// <summary>
        /// Evaluate an object against it's matching Specification and returns any broken rules.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static ValidationNotification Validate(object instance)
        {
            Type type = instance.GetType();
            var spec = GetSpecification(instance.GetType());
            return Validate(instance, spec);
        }

        public static ValidationNotification Validate(object instance, Specification specification)
        {
            //Guard for null
            if (instance == null)
            {
                throw new ArgumentNullException("Validate requires a non-null instance.");
            }

            return new ValidationNotification { Errors = specification.Validate(instance) }; 
        }

        public static void ResetRegistries()
        {
            _registry.Clear();
        }

        public static void ResetConfiguration()
        {
            Configuration = buildDefaultValidationConfiguration();
        }

        public static void RegisterSpecification<TEntity>(SpecificationBase<TEntity> expression)
        {
            RegisterSpecificationWithRegistry(expression);
        }

        public static void AssertConfigurationIsValid()
        {
            //Look for multiple specifications for a type where no default is defined.
            //TODO: Implement multispec check

            //Look for PropertyValidators with no Rules
            var invalidPropertyValidators = from r in _registry
                                            from v in r.PropertyValidators
                                            where v.Rules == null || !v.Rules.Any()
                                            select
                                                r.GetType().Name + " is invalid because it has no rules defined for property '" +
                                                v.PropertyName + "'.";

            if (invalidPropertyValidators.Any())
            {
                var errorString = invalidPropertyValidators.Aggregate(string.Empty, (x, y) => x + "\n" + y);
                throw new SpecExpressConfigurationError(errorString);
            }
        }

        #region Container

        public static Specification TryGetSpecification(Type type)
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
                var defaultSpecs =  from s in specs
                       where s.DefaultForType
                       select s;

                //No default specs defined
                if (!defaultSpecs.Any())
                {   
                    throw new ApplicationException("Multiple Specifications found and none are defined as default.");
                }

                //Multiple specs defined as Default
                if (defaultSpecs.Count() > 1)
                {
                    throw new ApplicationException("Multiple Specifications found and multiple are defined as default.");
                }

                return defaultSpecs.First();
            }

            return specs.First();
        }

        public static Specification GetSpecification(Type type)
        {
            var spec =  TryGetSpecification(type);
            if (spec == null)
            {
                throw new ApplicationException("No Specification for type " + type + " was found.");
            }

            return spec;
        }

        public static SpecificationBase<TType> GetSpecification<TType>()
        {
            return GetSpecification(typeof(TType)) as SpecificationBase<TType>;
        }

        public static SpecificationBase<TType> TryGetSpecification<TType>()
        {
            return TryGetSpecification(typeof(TType)) as SpecificationBase<TType>;
        }

        public static IList<Specification> GetAllSpecifications()
        {
            return _registry;
        }

        
        #endregion

        #region Registration

        private static void CreateAndRegisterSpecificationsWithRegistry(IEnumerable<Type> specs)
        {
            var delayedSpecs = new List<Type>();

            //For each type, instantiate it and add it to the collection of specs found
            specs.ToList<Type>().ForEach(spec =>
            {
                try
                {
                    var s = Activator.CreateInstance(spec) as Specification;

                    RegisterSpecificationWithRegistry(s);
                }
                catch (System.Reflection.TargetInvocationException)
                {
                    //Can't create the object because it has a specification that hasn't been loaded yet
                    //save it for the next pass
                    delayedSpecs.Add(spec);
                }
            });

            //Process any specification that couldn't be reloaded
            if (delayedSpecs.Any())
            {
                CreateAndRegisterSpecificationsWithRegistry(delayedSpecs);
            }
        }

        private static void RegisterSpecificationWithRegistry(Specification spec)
        {
            if (spec != null)
            {
                _registry.Add(spec);
            }
        }

        private static ValidationCatalogConfiguration buildDefaultValidationConfiguration()
        {
            return new ValidationCatalogConfiguration()
            {
                DefaultMessageStore = new ResourceMessageStore(RuleErrorMessages.ResourceManager),
                ValidateObjectGraph = false
            };
            
        }

        #endregion

    }
}