using System;
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
        /// Configures Assemblies to scan to register Specifications used by Validate(object)
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
            if (!Registry.Any())
            {
                throw new ArgumentNullException("No Specifications found.");
            }

            if (!Registry.ContainsKey(instance.GetType()))
            {
                throw new NullReferenceException("No Specification registered for type" + instance.GetType());
            }

            Specification spec = Registry[instance.GetType()];

            if (spec == null)
            {
                throw new ArgumentException("No Specification found for type " + instance.GetType());
            }

            //Group

            return new ValidationNotification {Errors = spec.Validate(instance)};
        }

        public static void ResetRegistries()
        {
            Registry = new Dictionary<Type, Specification>();
        }

        public static void RegisterSpecification<TEntity>(SpecificationBase<TEntity> expression)
        {
            RegisterSpecificationWithRegistry(expression);
            //registerFoundSpecifications(new List<Specification> {expression});
        }

        public static void AssertConfigurationIsValid()
        {
            //TODO: implement
            //Look for PropertyValidators with no Rules

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



        //private static void registerFoundSpecifications(IList<Specification> specifications)
        //{
        //    foreach (Specification incomingSpecification in specifications)
        //    {
        //        //TODO: this assumes the Spec directly inherits from SpecificationBase
        //        Type typeForSpec = incomingSpecification.GetType().BaseType.GetGenericArguments().FirstOrDefault();

        //        //if (Registry.ContainsKey(typeForSpec))
        //        //{
        //        //    //Add rules for all PropertyValidators to the Rules for existing Property Validators
        //        //    incomingSpecification.PropertyValidators.ForEach(pv =>
        //        //                                                         {
        //        //                                                             //find the matching propertyValidator
        //        //                                                             IEnumerable<PropertyValidator> p =
        //        //                                                                 from propertyValidators in
        //        //                                                                     Registry[typeForSpec].
        //        //                                                                     PropertyValidators
        //        //                                                                 where
        //        //                                                                     propertyValidators.PropertyInfo ==
        //        //                                                                     pv.PropertyInfo
        //        //                                                                 select propertyValidators;

        //        //                                                             //Add all the rules on the incoming PropertyValidator to the matching existing PropertyValidator Rules
        //        //                                                             pv.Rules.ForEach(r => p.First().AddRule(r));
        //        //                                                         });
        //        //}
        //        //else
        //        //{
        //        Registry.Add(typeForSpec, incomingSpecification);
        //        //}
        //    }
        //}

        private static void CreateAndRegisterSpecificationsWithRegistry(IEnumerable<Type> specs)
        {
            List<Type> delayedSpecs = new List<Type>();

            //For each type, instantiate it and add it to the collection of specs found
            specs.ToList<Type>().ForEach(spec =>
            {
                try
                {
                    var s = Activator.CreateInstance(spec) as Specification;

                    RegisterSpecificationWithRegistry(s);
                    //_specifications.Add(o as Specification);
                }
                catch (System.Reflection.TargetInvocationException)
                {
                    //Can't create the object because it has a specification that hasn't been loaded yet
                    //save it for the next pass
                    delayedSpecs.Add(spec);
                }
            });

            
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