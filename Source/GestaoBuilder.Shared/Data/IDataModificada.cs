using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using Modular.Shared.Helpers;

namespace Modular.Shared.Data
{
    public interface IDataModificada
    {
        string[] Modificados { get; }

        void LimparModificados();
    }

    public class DataModificada<TData> : IDataModificada
        where TData : class, IDataKey
    {
        public DataModificada()
        {
            ModificadoMap = new Dictionary<string, CampoModificado>();
        }

        protected class CampoModificado
        {
            public object Obj { get; set; }
            public bool FoiModificado { get; set; }
        }

        protected Dictionary<string, CampoModificado> ModificadoMap { get; }

        protected TProperty Getter<TProperty>(Expression<Func<TData, TProperty>> expression)
        {
            string propertyName = ObjectHelper<TData>.GetPropertyName(expression);
            if (ModificadoMap.ContainsKey(propertyName))
            {
                return (TProperty) ModificadoMap[propertyName].Obj;
            }
            return default(TProperty);
        }

        protected void Setter<TProperty>(Expression<Func<TData, TProperty>> expression, object value)
        {
            string propertyName = ObjectHelper<TData>.GetPropertyName(expression);
            ModificadoMap[propertyName] = new CampoModificado() {FoiModificado = true, Obj = value};
        }

        string[] IDataModificada.Modificados
        {
            get { return ModificadoMap
                    .Where(k => k.Value.FoiModificado)
                    .Select(k => k.Key).ToArray(); }
        }

        void IDataModificada.LimparModificados()
        {
            foreach (var item in ModificadoMap.Values)
            {
                item.FoiModificado = false;
            }
        }
    }
}