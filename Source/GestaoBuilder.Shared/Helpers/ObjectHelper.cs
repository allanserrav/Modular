using System;
using System.Linq;
using System.Linq.Expressions;

namespace Modular.Shared.Helpers
{
    public static class ObjectHelper
    {

        public static TGeneric CreateGeneric<TGeneric>(Type genericType, Type targetType, params object[] args)
        {
            var t = genericType.MakeGenericType(targetType);
            return (TGeneric)Activator.CreateInstance(t, args);
        }

        public static TResult CallGeneric<TResult>(object instance, string memberName, Type targetType, params object[] args)
        {
            var method = instance.GetType().GetMethod(memberName);
            var generic = method.MakeGenericMethod(targetType);
            return (TResult)generic.Invoke(instance, args);
        }

        public static bool InheritedFrom(this object obj, object from)
        {
            if (@from != null)
            {
                return InheritedFrom(obj, @from.GetType());
            }
            return false;
        }

        public static bool InheritedFrom(this object obj, Type typefrom)
        {
            var objtype = obj.GetType();
            bool resultBaseType = objtype == typefrom || objtype.BaseType == typefrom;
            if (!resultBaseType)
            {
                return objtype.GetInterfaces().Any(i => i == typefrom);
            }
            return true;
        }

        public static bool InheritedFrom(this Type type, params Type[] argsTypefrom)
        {
            var objtype = type;
            bool resultBaseType = argsTypefrom.Any(c => c == objtype || c == objtype.BaseType);
            return resultBaseType || objtype.GetInterfaces().Any(i => argsTypefrom.Any(c => c == i));
        }

        public static bool IsNumeric(this Type type)
        {
            if( type == typeof(decimal) ||
                type == typeof(double))
            {
                return true;
            }
            return false;
        }

        public static void Join(this object to, object from)
        {
            var totype = to.GetType();
            var fromtype = from.GetType();
            foreach (var toProperty in totype.GetProperties()) {
                var fromProperty = fromtype.GetProperty(toProperty.Name);
                if (fromProperty != null) {
                    toProperty.SetValue(to, fromProperty.GetValue(from));
                }
            }
        }
    }

    public static class ObjectHelper<T>
    {
        public static string GetPropertyName<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            var exp = (MemberExpression) expression.Body;
            return exp.Member.Name;
        }

        public static Type GetPropertyType<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            var exp = (MemberExpression) expression.Body;
            return exp.Type;
        }
    }
}

