using System;
using System.Linq;
using Modular.Data.MongoCore.Serializers;
using Modular.Shared;
using Modular.Shared.Data;
using Modular.Shared.Data.Business;
using Modular.Shared.Helpers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace Modular.Data.MongoCore
{
    public static class MongoHelper
    {
        public static IBsonSerializer GetBsonSerializer(Type targetType, BsonClassMap classMap)
        {
            var serializerType = typeof(BsonClassMapSerializer<>).MakeGenericType(targetType);
            return (IBsonSerializer)Activator.CreateInstance(serializerType, classMap);
        }

        public static BsonClassMap GetClassMap(string idProperty, Type classmapType, IMapper mapper, IDataBisRead reader = null,
            BsonClassMap baseClassMap = null, bool isUpdater = false, string[] onlyProperties = null, params Type[] knowTypes)
        {
            var map = mapper.Get(classmapType);
            var classMap = new BsonClassMap(classmapType, baseClassMap);
            if (knowTypes.Length > 0) classMap.SetIsRootClass(true);
            classMap.SetDiscriminator(classmapType.Name);
            foreach (var knowType in knowTypes) {
                classMap.AddKnownType(knowType);
            }
            if (!isUpdater) {
                classMap.MapIdProperty(idProperty)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
            }
            else {
                classMap.UnmapProperty(idProperty);
            }
            foreach (var item in map.WriteMaps.Where(d => d.ClassType == classmapType)) {
                if ((!isUpdater && !item.IsIgnore) || (isUpdater && onlyProperties != null && onlyProperties.Any(s => s == item.PropertyName))) {
                    if (item.IsRef) {
                        classMap.MapProperty(item.PropertyName)
                            .SetElementName(item.EntryRefName)
                            .SetIdGenerator(StringObjectIdGenerator.Instance)
                            .SetSerializer(ObjectHelper.CreateGeneric<IBsonSerializer>(typeof(ReferenceDataBisSerializer<>), item.PropertyType, mapper, reader));
                        continue;
                    }
                    if (item.IsList)
                    {
                        var itemArraySerializer = ObjectHelper.CreateGeneric<IBsonSerializer>(typeof(ObjectSerializer<>),
                            item.PropertyType, mapper, reader);
                        var arraySerializer = ObjectHelper.CreateGeneric<IBsonSerializer>(typeof(ArraySerializer<>),
                            item.PropertyType, itemArraySerializer);
                        classMap.MapProperty(item.PropertyName)
                            .SetElementName(item.EntryRefName)
                            .SetSerializer(arraySerializer)
                            ;
                        continue;
                    }
                    if (item.IsNested)
                    {
                        var serializer = ObjectHelper.CreateGeneric<IBsonSerializer>(typeof(ObjectSerializer<>),
                            item.PropertyType, mapper, reader);
                        classMap.MapProperty(item.PropertyName)
                            .SetElementName(item.EntryRefName)
                            .SetSerializer(serializer)
                            ;
                        continue;
                    }
                    var member = classMap.MapProperty(item.PropertyName).SetElementName(item.EntryRefName);
                    if (item.PropertyType.IsNumeric())
                    {
                        member.SetSerializer(new DecimalSerializer(BsonType.Decimal128));
                    }
                }
                else {
                    classMap.UnmapProperty(item.PropertyName);
                }
            }
            return classMap;
        }
    }
}
