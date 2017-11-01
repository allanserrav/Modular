using System;
using Modular.Shared.Data;
using Modular.Shared.Data.Business;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Modular.Data.MongoCore.Serializers
{
    public class ReferenceDataBisSerializer<TValue> : SealedClassSerializerBase<TValue>
        where TValue : class, IDataBis
    {
        private readonly IMapper _mapper;
        private readonly IDataBisRead _reader;

        public ReferenceDataBisSerializer(IMapper mapper, IDataBisRead reader)
        {
            _mapper = mapper;
            _reader = reader;
        }

        public override TValue Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var oid = context.Reader.ReadObjectId();
            return _reader.Get<TValue>(oid.ToString());
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TValue value)
        {
            ObjectId id;
            if (String.IsNullOrEmpty(value.Id))
            {
                id = ObjectId.GenerateNewId();
                value.Id = id.ToString();
            }
            context.Writer.WriteObjectId(new ObjectId(value.Id));
            //base.Serialize(context, args, value);
        }
    }
}
