using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SpecExpress
{
    public class SpecificationScanner
    {
        private static IList<Specification> _specifications = new List<Specification>();

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
            var assemblyPaths = System.IO.Directory.GetFiles(path).Where(file =>
                                                                        System.IO.Path.GetExtension(file).Equals(
                                                                            ".exe", StringComparison.OrdinalIgnoreCase)
                                                                        ||
                                                                        System.IO.Path.GetExtension(file).Equals(
                                                                            ".dll", StringComparison.OrdinalIgnoreCase));

            foreach (var assemblyPath in assemblyPaths)
            {
                Assembly assembly = null;
                try
                {
                    assembly = System.Reflection.Assembly.LoadFrom(assemblyPath);
                }
                catch
                {
                }
                if (assembly != null && assembly != Assembly.GetAssembly(typeof(ValidationContainer)))
                {
                    AddAssembly(assembly);
                }
            }
        }

        internal IList<Specification> FoundSpecifications
        {
            get { return _specifications; }
        }

        private static Assembly findTheCallingAssembly()
        {
            var trace = new StackTrace(false);

            Assembly thisAssembly = System.Reflection.Assembly.GetExecutingAssembly();
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
            foreach (var type in assembly.GetExportedTypes())
            {
                if (typeof(Specification).IsAssignableFrom(type))
                {
                    object o = Activator.CreateInstance(type);

                    Type keyType = o.GetType().BaseType.GetGenericArguments().FirstOrDefault();

                    try
                    {
                        //Register
                        _specifications.Add(o as Specification);
                    }
                    catch (System.ArgumentException argumentException)
                    {
                        throw new ArgumentException("A Specification for class " + keyType + " has been loaded already.", argumentException);
                    }
                }
            }

        }
    }
}
