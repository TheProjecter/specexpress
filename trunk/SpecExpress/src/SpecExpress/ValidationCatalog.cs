using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SpecExpress
{
    public static class ValidationCatalog
    {
        public static bool ValidateObjectGraph { get; set; }

        public static IDictionary<Type, Specification> Registry = new Dictionary<Type, Specification>();

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


        /// <summary>
        /// Evaluate an object against it's matching Specification and returns any broken rules.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static ValidationNotification Validate(object instance)
        {
            //Guard for null
            if (instance == null)
            {
                throw new ArgumentNullException("Validate requires a non-null instance.");
            }
            
            if (!Registry.Any())
            {
                throw new ArgumentNullException("No Specifications found.");
            }

            if (!Registry.ContainsKey(instance.GetType()))
            {
                throw new NullReferenceException("No Specification registered for type" + instance.GetType());
            }

            Specification spec = Registry[instance.GetType()];
            
            return new ValidationNotification {Errors = spec.Validate(instance)};
        }

        public static void ResetRegistries()
        {
            Registry.Clear();
        }

        public static void RegisterSpecification<TEntity>(SpecificationBase<TEntity> expression)
        {
            RegisterSpecificationWithRegistry(expression);
        }

        public static void  AssertConfigurationIsValid()
        {
            //Look for PropertyValidators with no Rules
            var invalidPropertyValidators = from r in Registry.Values
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

        public static Specification GetSpecificationFromRegistry<TType>() 
        {
            if (Registry.ContainsKey(typeof(TType)) )
            {
                return Registry[typeof(TType)];
            }
            else
            {
                //
                throw new InvalidOperationException("No Specification found for Type " + typeof (TType).ToString());
            }
            
        }


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
                //TODO: this assumes the Spec directly inherits from SpecificationBase
                Type typeForSpec = spec.GetType().BaseType.GetGenericArguments().FirstOrDefault();
                Registry.Add(typeForSpec, spec); 
            }
        }
    }
}