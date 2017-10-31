using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GestaoBuilder.Shared.Atributos;
using GestaoBuilder.Shared.Helpers;

namespace GestaoBuilder.Shared.Data
{
    public static class DataHelper
    {
        public static string GetDocumentName<TData>(this TData data)
            where TData : IDataKey
        {
            return GetDocumentName(data.GetType());
        }

        public static string GetDocumentName(this Type type)
        {
            var attrs = type.GetAtributos();
            if (attrs.FirstOrDefault(a => a is DataAttribute) is DataAttribute info) {
                return info.DocumentName;
            }
            return null;
        }
    }
}
