using System.Collections;
using System.Linq;
using SpecExpress.MessageStore;
using SpecExpress.Rules;
using SpecExpress.Rules.GeneralValidators;
using System;

namespace SpecExpress.DSL
{
    public class WithBuilder<T, TProperty>
    {
        private readonly PropertyValidator<T, TProperty> _propertyValidator;

        public WithBuilder(PropertyValidator<T, TProperty> propertyValidator)
        {
            _propertyValidator = propertyValidator;
        }

        public IAndOr<T, TProperty> Message(string message)
        {
            //set error message for last rule added
            RuleValidator rule = _propertyValidator.Rules.Last();
            rule.Message = message;
            return new ActionJoinBuilder<T, TProperty>(_propertyValidator);
        }

        public IAndOr<T, TProperty> MessageKey<TMessage>(TMessage messageKey)
        {
            //set error message for last rule added
            RuleValidator rule = _propertyValidator.Rules.Last();
            rule.MessageKey = messageKey;
            return new ActionJoinBuilder<T, TProperty>(_propertyValidator);
        }

        /// <summary>
        /// Sets Specification used to validate this Property to the Default
        /// </summary>
        /// <returns></returns>
        public IAndOr<T, TProperty> Specification()
        {  
            var specRule = new SpecificationRule<T, TProperty>();
            _propertyValidator.AddRule(specRule);
            return new ActionJoinBuilder<T, TProperty>(_propertyValidator);
        }

        /// <summary>
        /// Sets Specification used to validate this Property to the Default
        /// </summary>
        /// <returns></returns>
        public IAndOr<T, TProperty> Specification<TSpecType>() where TSpecType:Validates<TProperty>, new()
        {
            TSpecType specification = new TSpecType();
            var specRule = new SpecificationRule<T, TProperty>(specification);
            _propertyValidator.AddRule(specRule);
            
            return new ActionJoinBuilder<T, TProperty>(_propertyValidator);
        }

        

        /// <summary>
        /// Sets Specification used to validate this Property to the Default
        /// </summary>
        /// <returns></returns>
        public IAndOr<T, TProperty> Specification(Action<Validates<TProperty>> rules)
        {   
            var specification = new SpecificationExpression<TProperty>();
            rules(specification);

            var specRule = new SpecificationRule<T, TProperty>(specification);           
        

            _propertyValidator.AddRule(specRule);

            return new ActionJoinBuilder<T, TProperty>(_propertyValidator);
        }


        /// <summary>
        /// ForEachSpecification<< Contact >> ( spec => spec.Check(c => c.LastName).Required(); );
        /// </summary>
        /// <typeparam name="TCollectionType"></typeparam>
        /// <param name="rules"></param>
        /// <returns></returns>
        public IAndOr<T, TProperty> ForEachSpecification<TCollectionType>(Action<Validates<TCollectionType>> rules)
        {
            var specification = new SpecificationExpression<TCollectionType>();
            rules(specification);

            var specRule = new ForEachSpecificationRule<T, TProperty, TCollectionType>(specification);


            _propertyValidator.AddRule(specRule);

            return new ActionJoinBuilder<T, TProperty>(_propertyValidator);
        }

        /// <summary>
        /// ForEachSpecification<< Contact, ContactSpecification >>(); //explictly use supplied specification
        /// </summary>
        /// <typeparam name="TCollectionType"></typeparam>
        /// <typeparam name="TCollectionSpecType"></typeparam>
        /// <returns></returns>
        public IAndOr<T, TProperty> ForEachSpecification<TCollectionType, TCollectionSpecType>()
            where TCollectionSpecType : Validates<TCollectionType>, new()
        {
            var specification = new TCollectionSpecType();
            var specRule = new ForEachSpecificationRule<T, TProperty, TCollectionType>(specification);
            _propertyValidator.AddRule(specRule);

            return new ActionJoinBuilder<T, TProperty>(_propertyValidator);
        }

        /// <summary>
        /// ForEachSpecification<< Contact >>(); //Default Specification
        /// </summary>
        /// <typeparam name="TCollectionType"></typeparam>
        /// <param name="rules"></param>
        /// <returns></returns>
        public IAndOr<T, TProperty> ForEachSpecification<TCollectionType>()
        {
            var specRule = new ForEachSpecificationRule<T, TProperty, TCollectionType>();
            _propertyValidator.AddRule(specRule);
            return new ActionJoinBuilder<T, TProperty>(_propertyValidator);
        }
    }



    public class WithBuilderForCollections<T, TProperty>  where TProperty : IEnumerable
    {
        private readonly PropertyValidator<T, TProperty> _propertyValidator;

        public WithBuilderForCollections(PropertyValidator<T, TProperty> propertyValidator)
        {
            _propertyValidator = propertyValidator;
        }

        public IAndOrForCollections<T, TProperty> Message(string message)
        {
            //set error message for last rule added
            RuleValidator rule = _propertyValidator.Rules.Last();
            rule.Message = message;
            return new ActionJoinBuilderForCollections<T, TProperty>(_propertyValidator);
        }

        public IAndOrForCollections<T, TProperty> MessageKey<TMessage>(TMessage messageKey)
        {
            //set error message for last rule added
            RuleValidator rule = _propertyValidator.Rules.Last();
            rule.MessageKey = messageKey;
            return new ActionJoinBuilderForCollections<T, TProperty>(_propertyValidator);
        }

        /// <summary>
        /// Sets Specification used to validate this Property to the Default
        /// </summary>
        /// <returns></returns>
        public IAndOrForCollections<T, TProperty> Specification()
        {
            var specRule = new SpecificationRule<T, TProperty>();
            _propertyValidator.AddRule(specRule);
            return new ActionJoinBuilderForCollections<T, TProperty>(_propertyValidator);
        }

        /// <summary>
        /// Sets Specification used to validate this Property to the Default
        /// </summary>
        /// <returns></returns>
        public IAndOrForCollections<T, TProperty> Specification<TSpecType>() where TSpecType : Validates<TProperty>, new()
        {
            TSpecType specification = new TSpecType();
            var specRule = new SpecificationRule<T, TProperty>(specification);
            _propertyValidator.AddRule(specRule);

            return new ActionJoinBuilderForCollections<T, TProperty>(_propertyValidator);
        }



        /// <summary>
        /// Sets Specification used to validate this Property to the Default
        /// </summary>
        /// <returns></returns>
        public IAndOrForCollections<T, TProperty> Specification(Action<Validates<TProperty>> rules)
        {
            var specification = new SpecificationExpression<TProperty>();
            rules(specification);

            var specRule = new SpecificationRule<T, TProperty>(specification);


            _propertyValidator.AddRule(specRule);

            return new ActionJoinBuilderForCollections<T, TProperty>(_propertyValidator);
        }

        ///// <summary>
        ///// ForEachSpecification<< Contact >>();
        ///// </summary>
        ///// <typeparam name="TCollectionType"></typeparam>
        ///// <typeparam name="TCollectionSpecType"></typeparam>
        ///// <returns></returns>
        //public IAndOr<T, TProperty> ForEachSpecification<TCollectionType, TCollectionSpecType>()
        //    where TCollectionSpecType : SpecificationBase<TCollectionType>, new()
        //     //where TProperty : IEnumerable
        //{
        //    var specification = new TCollectionSpecType();
        //    var specRule = new  ForEachSpecificationRule<T, TCollectionType>(specification);
        //    _propertyValidator.AddRule(specRule);

        //    return new ActionJoinBuilder<T, TProperty>(_propertyValidator);
        //}

        /// <summary>
        /// ForEachSpecification<<Contact>>( spec => 
        /// {
        ///     spec.Check(r.Property).Required();
        /// });
        /// </summary>
        /// <typeparam name="TCollectionType"></typeparam>
        /// <param name="rules"></param>
        /// <returns></returns>
        public IAndOrForCollections<T, TProperty> ForEachSpecification<TCollectionType>(Action<Validates<TCollectionType>> rules)
        {
            var specification = new SpecificationExpression<TCollectionType>();
            rules(specification);

            var specRule = new ForEachSpecificationRule<T, TProperty, TCollectionType>();


            _propertyValidator.AddRule(specRule);

            return new ActionJoinBuilderForCollections<T, TProperty>(_propertyValidator);
        }


    }

}