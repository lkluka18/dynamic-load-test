using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Common;
using Microsoft.Extensions.DependencyModel;

namespace LoadApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var asl = new AssemblyLoader();
            var cwd = Directory.GetCurrentDirectory();
            var path = Path.Combine(cwd, "Modules");
            var fileName = Path.Combine(path, "TestModule.dll");
            // var asm = asl.LoadFromAssemblyPath(fileName);

            // var type = asm.GetType("TestModule.Hello");
            // dynamic obj = Activator.CreateInstance(type);
            // Console.WriteLine(obj.Say());
            // IHello xx = null;


            //var myAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(fileName);
            var myAssembly = Assembly.LoadFrom(fileName);
            var myType = myAssembly.GetType("TestModule.Hello");
            //var types = myAssembly.ExportedTypes.Where(t => t.GetInterfaces().Any(i => i.FullName == "Common.IHello")).ToList();
            var types = myAssembly.GetTypes().Where(t => t.GetInterfaces().Any(i => i.GetType() == typeof(IHello)));
            var myInstance = Activator.CreateInstance(myType);
            ((IHello)myInstance).Say();
        }

        public class AssemblyLoader : AssemblyLoadContext
        {
            // Not exactly sure about this
            protected override Assembly Load(AssemblyName assemblyName)
            {
                var deps = DependencyContext.Default;
                var res = deps.CompileLibraries.Where(d => d.Name.Contains(assemblyName.Name)).ToList();
                var assembly = Assembly.Load(new AssemblyName(res.First().Name));
                return assembly;
            }
        }
    }
}
