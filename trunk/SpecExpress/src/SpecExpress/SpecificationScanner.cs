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
        private readonly IList<Specification> _specifications = new List<Specification>();

        internal IList<Specification> FoundSpecifications
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
            registerAssemblies(new List<Assembly>() { assembly });            
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

            registerAssemblies(assemblies);


                 

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

        private void registerAssemblies(List<Assembly> assemblies)
        {
            //Find all types in all assemblies that inherit from Specification
            IEnumerable<Type> specs = from a in assemblies
                        select a.GetExportedTypes() into types
                        from type in types
                        where typeof(Specification).IsAssignableFrom(type)
                        select type;

            //For each type, instantiate it and add it to the collection of specs found
            specs.ToList<Type>().ForEach(spec => 
                {
                    object o = Activator.CreateInstance(spec);
                    _specifications.Add(o as Specification);
                });
        }
    }
}