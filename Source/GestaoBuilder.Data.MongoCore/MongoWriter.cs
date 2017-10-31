using System;
using System.Collections.Generic;
using GestaoBuilder.CoreShared.Contracts;
using GestaoBuilder.Shared.Data;
using GestaoBuilder.Shared.Data.Business;
using GestaoBuilder.Shared.Helpers;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;

namespace GestaoBuilder.Data.MongoCore
{
    public class MongoWriter : BaseMongo, IDataBisWrite
    {
        public MongoWriter(ICoreSupport core, IDataBisRead reader) : base(core, reader)
        {
        }

        public void AddFromJson(JToken json)
        {
            throw new NotImplementedException();
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class, IBaseData, IDataBisKey
        {
            OpenConnection();
            var map = Core.Mapper.Get<TEntity>();
            var collection = DataBase.GetCollection<BsonDocument>(map.DocumentName);
            // Mapeamento
            string idProperty = ObjectHelper<TEntity>.GetPropertyName(p => p.Id);
            //var baseClassMap = MongoHelper.GetClassMap(idProperty, entity.GetType().BaseType, Core.Mapper, null, null, entity.GetType());
            var entityClassMap = MongoHelper.GetClassMap(idProperty, entity.GetType(), Core.Mapper);
            entityClassMap.Freeze();

            entity.Id = ObjectId.GenerateNewId().ToString();
            var serializer = MongoHelper.GetBsonSerializer(entity.GetType(), entityClassMap);
            var bson = entity.ToBsonDocument(entity.GetType(), serializer);
            collection.InsertOne(bson);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class, IBaseData, IDataBisKey
        {
            throw new NotImplementedException();
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class, IBaseData, IDataBisKey
        {
            OpenConnection();
            var map = Core.Mapper.Get<TEntity>();
            var collection = DataBase.GetCollection<BsonDocument>(map.DocumentName);
            // Mapeamento
            string idProperty = ObjectHelper<TEntity>.GetPropertyName(p => p.Id);
            var dataModificada = entity as IDataModificada;
            var entityClassMap = MongoHelper.GetClassMap(idProperty, entity.GetType(), Core.Mapper, null, null, true, 
                    dataModificada?.Modificados);
            entityClassMap.Freeze();

            var serializer = MongoHelper.GetBsonSerializer(entity.GetType(), entityClassMap);
            var bson = entity.ToBsonDocument(entity.GetType(), serializer);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(entity.Id));
            var updaters = new List<UpdateDefinition<BsonDocument>>();
            foreach (var bsonItem in bson)
            {
                updaters.Add(Builders<BsonDocument>.Update.Set(bsonItem.Name, bsonItem.Value));
            }
            var updateOnProperty = map.GetItem<TEntity, DateTime?>(p => p.UpdatedOn);
            updaters.Add(Builders<BsonDocument>.Update.CurrentDate(updateOnProperty.EntryRefName));
            collection.UpdateOne(filter, Builders<BsonDocument>.Update.Combine(updaters));
        }
    }
}
