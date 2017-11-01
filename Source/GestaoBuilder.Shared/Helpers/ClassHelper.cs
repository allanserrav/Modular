using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Modular.Shared.Helpers
{
    public static class ClassHelper<T, TValue>
    {
        public class ObjectProperty<Tin>
        {
            private readonly Dictionary<string, TValue> _map = new Dictionary<string, TValue>();

            public ObjectProperty<Tin> MapProperty<TProperty>(Expression<Func<Tin, TProperty>> expressionKey, TValue value)
            {
                var exp = (MemberExpression)expressionKey.Body;
                _map.Add(exp.Member.Name, value);
                return this;
            }

            public Dictionary<string, TValue> GetDictionary(Dictionary<string, TValue> join = null)
            {
                if (join != null) {
                    foreach (var k in join) {
                        _map.Add(k.Key, k.Value);
                    }
                }
                return _map;
            }
        }

        public static string GetClassName()
        {
            return typeof(T).Name;
        }

        public static ObjectProperty<T> MapProperty<TProperty>(Expression<Func<T, TProperty>> expressionKey, TValue value)
        {
            var map = new ObjectProperty<T>();
            return map.MapProperty(expressionKey, value);
        }

        public static string GetPropertyName<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            var exp = (MemberExpression)expression.Body;
            return exp.Member.Name;
        }
    }
}
