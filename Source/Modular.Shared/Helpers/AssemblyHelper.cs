using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Modular.Shared.Helpers
{
    public static class AssemblyHelper
    {
        public static TObject GetObjectFromFile<TObject>(FileInfo assemblyFile, string objectName, params object[] pconstructor)
        {
            if(!assemblyFile.Exists) {
                throw new FileNotFoundException();
            }
            var assembly = Assembly.LoadFrom(assemblyFile.FullName);
            var objectType = assembly.GetType(objectName);
            var constructor = objectType.GetConstructors()[0];
            return (TObject)constructor.Invoke(pconstructor);
        }

        public static IEnumerable<Type> GetInterfaceTypes<TInterface>(string assemblyPath)
        {
            var assembly = Assembly.LoadFrom(assemblyPath);
            var types = assembly.GetTypes();
            List<Type> interfaceTypes = new List<Type>();
            foreach(var type in types) {
                var i = type.GetInterface(typeof(TInterface).Name);
                if(i != null) { 
                    interfaceTypes.Add(type);
                }
            }
            return interfaceTypes;
        }

        public static IEnumerable<Attribute> GetAtributos(this Type type) => type.GetCustomAttributes<Attribute>(true);
    }
}
