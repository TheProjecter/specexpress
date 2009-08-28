using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SpecExpress
{
    public class SpecificationScanner
    {
        private readonly List<Type> _specifications = new List<Type>();

        internal IList<Type> FoundSpecifications
        {
            get { return _specifications; }
        }
        
        public void TheCallingAssembly()
        {
            Assembly callingAssembly = findTheCallingAssembly();

            if (callingAssembly != null)
            {
                AddAssembly(callingAssembly);
            }
        }

        public void AddAssembly(Assembly assembly)
        {
            scanAssembliesForSpecifications(new List<Assembly>() { assembly });            
        }


        public void AddAssembliesFromPath(string path)
        {
            var r = Directory.GetFiles(path).Where(file =>
                                                                               Path.GetExtension(file).Equals(
                                                                                   ".exe",
                                                                                   StringComparison.OrdinalIgnoreCase)
                                                                               ||
                                                                               Path.GetExtension(file).Equals(
                                                                                   ".dll",
                                                                                   StringComparison.OrdinalIgnoreCase))
                                                                                   .Select(assemblyPath => Assembly.LoadFrom(assemblyPath));

           
            List<Assembly> assemblies = r.Where<Assembly>(assembly => assembly != null && assembly != typeof(ValidationCatalog).Assembly)
                .ToList<Assembly>();

            scanAssembliesForSpecifications(assemblies);
        }

        private Assembly findTheCallingAssembly()
        {
            var trace = new StackTrace(false);

            Assembly thisAssembly = Assembly.GetExecutingAssembly();
            Assembly callingAssembly = null;
            for (int i = 0; i < trace.FrameCount; i++)
            {
                StackFrame frame = trace.GetFrame(i);
                Assembly assembly = frame.GetMethod().DeclaringType.Assembly;
                if (assembly != thisAssembly)
                {
                    callingAssembly = assembly;
                    break;
                }
            }
            return callingAssembly;
        }

        private void scanAssembliesForSpecifications(List<Assembly> assemblies)
        {
            //Find all types in all assemblies that inherit from Specification
            IEnumerable<Type> specs = from a in assemblies
                        select a.GetExportedTypes() into types
                        from type in types
                        where typeof(Specification).IsAssignableFrom(type)
                        select type;

            _specifications.AddRange(specs);

            //registerSpecifications(specs);
        }

        //private void registerSpecifications(IEnumerable<Type> specs)
        //{
        //    List<Type> delayedSpecs = new List<Type>();

        //    //For each type, instantiate it and add it to the collection of specs found
        //    specs.ToList<Type>().ForEach(spec => 
        //                                     {
        //                                         try
        //                                         {
        //                                             object o = Activator.CreateInstance(spec);
        //                                             _specifications.Add(o as Specification);
        //                                         }
        //                                         catch (System.Reflection.TargetInvocationException)
        //                                         {
        //                                             //Can't create the object because it has a specification that hasn't been loaded yet
        //                                             //save it for the next pass
        //                                             delayedSpecs.Add(spec);
        //                                             //throw;
        //                                         }
        //                                     });
            
        //    registerSpecifications(delayedSpecs);

                                                 
                                                 
        //}
    }
}