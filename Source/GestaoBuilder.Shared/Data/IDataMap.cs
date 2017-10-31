using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GestaoBuilder.Shared.Data.Business;

namespace GestaoBuilder.Shared.Data
{
    public interface IDataMap
    {
        string DocumentName { get; }
        IEnumerable<DataMapItem> ParseMaps { get; }
        IEnumerable<DataMapItem> WriteMaps { get; }

        DataMapItem GetItem<TData, TProperty>(Expression<Func<TData, TProperty>> expressionKey);
    }

    public interface IDataMap<TData> : IDataMap, IDataMapParse<TData>
    {
        IDataMap<TData> IgnoreParseAndWrite<TProperty>(Expression<Func<TData, TProperty>> expressionKey);
        IDataMap<TData> IgnoreWrite<TProperty>(Expression<Func<TData, TProperty>> expressionKey);
        IDataMap<TData> PropertyParseAndWrite<TProperty>(Expression<Func<TData, TProperty>> expressionKey, string refName);
        IDataMap<TData> PropertyWrite<TProperty>(Expression<Func<TData, TProperty>> expressionKey, string refName);
        IDataMap<TData> ReferenceWrite<TProperty>(Expression<Func<TData, TProperty>> expressionKey, string refName)
            where TProperty : class, IDataBis;
    }

    public interface IDataMapParse<TData>
    {
        IDataMapParse<TData> IgnoreParse<TProperty>(Expression<Func<TData, TProperty>> expressionKey);
        IDataMapParse<TData> PropertyParse<TProperty>(Expression<Func<TData, TProperty>> expressionKey, string refName);
    }

}