using System;
using GestaoBuilder.Shared.Data;
using GestaoBuilder.Shared.Data.Business;
using GestaoBuilder.Shared.Helpers;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace GestaoBuilder.Data.MongoCore.Serializers
{
    public class ObjectSerializer<TValue> : SealedClassSerializerBase<TValue>
        where TValue : class, IDataBisKey
    {
        private readonly IMapper _mapper;
        private readonly MongoReader _reader;

        public ObjectSerializer(IMapper mapper, MongoReader reader)
        {
            _mapper = mapper;
            _reader = reader;
        }

        public override TValue Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            string idProperty = ObjectHelper<TValue>.GetPropertyName(p => p.Id);
            var entityClassMap = MongoHelper.GetClassMap(idProperty, typeof(TValue), _mapper, _reader)
                .Freeze();
            var serializer = new BsonClassMapSerializer<TValue>(entityClassMap);
            return serializer.Deserialize(context, args);
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TValue value)
        {
            string idProperty = ObjectHelper<TValue>.GetPropertyName(p => p.Id);
            var entityClassMap = MongoHelper.GetClassMap(idProperty, value.GetType(), _mapper);
            entityClassMap.Freeze();

            value.Id = ObjectId.GenerateNewId().ToString();
            var serializer = MongoHelper.GetBsonSerializer(value.GetType(), entityClassMap);
            var bson = value.ToBsonDocument(value.GetType(), serializer);
            WriteDocument(context.Writer, bson);
        }

        private void WriteDocument(IBsonWriter writer, BsonDocument document)
        {
            writer.WriteStartDocument();
            foreach (var item in document) {
                writer.WriteName(item.Name);
                switch (item.Value.BsonType) {
                    case BsonType.ObjectId:
                        writer.WriteObjectId(item.Value.AsObjectId);
                        break;
                    case BsonType.String:
                        writer.WriteString(item.Value.AsString);
                        break;
                    case BsonType.Int32:
                        writer.WriteInt32(item.Value.AsInt32);
                        break;
                    case BsonType.Decimal128:
                        writer.WriteDecimal128(item.Value.AsDecimal128);
                        break;
                    case BsonType.DateTime:
                        writer.WriteDateTime(item.Value.AsBsonDateTime.MillisecondsSinceEpoch);
                        break;
                    case BsonType.Boolean:
                        writer.WriteBoolean(item.Value.AsBoolean);
                        break;
                    case BsonType.Document:
                        WriteDocument(writer, item.Value.AsBsonDocument);
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            writer.WriteEndDocument();
        }
    }
}
