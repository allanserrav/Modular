using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GestaoBuilder.Shared.Data;
using GestaoBuilder.Shared.Data.Business;
using GestaoBuilder.Shared.Helpers;

namespace GestaoBuilder.Core
{
    public class DataMap : IDataMap
    {
        public DataMap()
        {
            Itens = new List<DataMapItem>();
        }

        public DataMap(string documentName) : this()
        {
            this.DocumentName = documentName;
        }

        protected void AddMap(DataMapItem item)
        {
            Itens.Add(item);
        }

        public DataMapItem GetItem<TData, TProperty>(Expression<Func<TData, TProperty>> expressionKey)
        {
            var propertyName = ObjectHelper<TData>.GetPropertyName(expressionKey);
            return Itens.FirstOrDefault(d => d.PropertyName.Equals(propertyName));
        }

        protected List<DataMapItem> Itens { get; }
        public IEnumerable<DataMapItem> WriteMaps => Itens.Where(d => d.IsWriteDb);
        public IEnumerable<DataMapItem> ParseMaps => Itens.Where(d => !d.IsWriteDb);
        public string DocumentName { get; }
    }

    public class DataMap<TData> : DataMap, IDataMap<TData>
    //where TData : IDataBis
    {
        public DataMap(string documentName) : base(documentName)
        {
        }

        public static DataMap<TData> Inialize(string documentName)
        {
            var mapper = new DataMap<TData>(documentName);
            return mapper;
        }

        private void PropertyMap<TProperty>(Expression<Func<TData, TProperty>> expressionKey,
            string refName, bool isWriteDb = false, bool isRef = false, bool isIgnore = false)
        {
            var propertyName = ObjectHelper<TData>.GetPropertyName(expressionKey);
            var propertyType = ObjectHelper<TData>.GetPropertyType(expressionKey);
            bool isList = 
                propertyType.IsArray || propertyType.InheritedFrom(typeof(IEnumerable), typeof(IList), typeof(IEnumerable<>), typeof(IList<>)) && propertyType != typeof(String);
            bool isNested = false;
            if (isList)
            {
                propertyType = propertyType.GetElementType();
            }
            else if (propertyType.InheritedFrom(typeof(IDataBisKey), typeof(IDataBis)))
            {
                isNested = true;
            }
            var info = new DataMapItem { EntryRefName = refName, PropertyName = propertyName, IsIgnore = isIgnore, IsWriteDb = isWriteDb, IsList = isList, IsRef = isRef, IsNested = isNested, ClassType = typeof(TData), PropertyType = propertyType };
            AddMap(info);
        }

        public IDataMap<TData> PropertyParseAndWrite<TProperty>(Expression<Func<TData, TProperty>> expressionKey, string refName)
        {
            PropertyMap(expressionKey, refName);
            PropertyMap(expressionKey, refName, true);
            return this;
        }

        public IDataMap<TData> PropertyWrite<TProperty>(Expression<Func<TData, TProperty>> expressionKey, string refName)
        {
            PropertyMap(expressionKey, refName, true);
            return this;
        }

        public IDataMapParse<TData> PropertyParse<TProperty>(Expression<Func<TData, TProperty>> expressionKey, string refName)
        {
            PropertyMap(expressionKey, refName);
            return this;
        }

        public IDataMap<TData> ReferenceWrite<TProperty>(Expression<Func<TData, TProperty>> expressionKey, string refName)
            where TProperty : class, IDataBis
        {
            PropertyMap(expressionKey, refName, true, true);
            return this;
        }

        public IDataMap<TData> IgnoreParseAndWrite<TProperty>(Expression<Func<TData, TProperty>> expressionKey)
        {
            IgnoreParse(expressionKey);
            IgnoreWrite(expressionKey);
            return this;
        }

        public IDataMapParse<TData> IgnoreParse<TProperty>(Expression<Func<TData, TProperty>> expressionKey)
        {
            PropertyMap(expressionKey, null, false, false, true);
            return this;
        }

        public IDataMap<TData> IgnoreWrite<TProperty>(Expression<Func<TData, TProperty>> expressionKey)
        {
            PropertyMap(expressionKey, null, true, false, true);
            return this;
        }

        public void Join(DataMap inner)
        {
            this.Itens.AddRange(inner.WriteMaps);
            this.Itens.AddRange(inner.ParseMaps);
        }
    }

}