using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SpecExpress
{
    public class SpecificationScanner
    {
        private static readonly IList<Specification> _specifications = new List<Specification>();

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
            registerValidTypeInAssembly(assembly);
        }


        public void AddAssembliesFromPath(string path)
        {
            IEnumerable<string> assemblyPaths = Directory.GetFiles(path).Where(file =>
                                                                               Path.GetExtension(file).Equals(
                                                                                   ".exe",
                                                                                   StringComparison.OrdinalIgnoreCase)
                                                                               ||
                                                                               Path.GetExtension(file).Equals(
                                                                                   ".dll",
                                                                                   StringComparison.OrdinalIgnoreCase));

            foreach (string assemblyPath in assemblyPaths)
            {
                Assembly assembly = null;
                try
                {
                    assembly = Assembly.LoadFrom(assemblyPath);
                }
                catch
                {
                }
                if (assembly != null && assembly != Assembly.GetAssembly(typeof (ValidationContainer)))
                {
                    AddAssembly(assembly);
                }
            }
        }

        private static Assembly findTheCallingAssembly()
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

        private static void registerValidTypeInAssembly(Assembly assembly)
        {
            foreach (Type type in assembly.GetExportedTypes())
            {
                if (typeof (Specification).IsAssignableFrom(type))
                {
                    object o = Activator.CreateInstance(type);

                    Type keyType = o.GetType().BaseType.GetGenericArguments().FirstOrDefault();

                    try
                    {
                        //Register
                        _specifications.Add(o as Specification);
                    }
                    catch (ArgumentException argumentException)
                    {
                        throw new ArgumentException(
                            "A Specification for class " + keyType + " has been loaded already.", argumentException);
                    }
                }
            }
        }
    }
}