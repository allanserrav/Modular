using System.Collections.Generic;
using System.Linq;
using GestaoBuilder.CoreShared.Contracts;
using GestaoBuilder.Shared.Data;
using GestaoBuilder.Shared.Data.Business;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace GestaoBuilder.Data.MongoCore
{
    public class MongoReader : BaseMongo, IDataBisRead
    {
        public MongoReader(ICoreSupport core) : base(core, null)
        {
            Reader = this;
        }

        public IEnumerable<TEntity> GetJsonQuery<TEntity>(string jsonquery, string jsonorder = null) where TEntity : class, IBaseData, IDataBisKey
        {
            BsonDocument docquery = BsonSerializer.Deserialize<BsonDocument>(jsonquery ?? "{}");
            BsonDocument docorder = BsonSerializer.Deserialize<BsonDocument>(jsonorder ?? "{}");
            var query = new BsonDocumentFilterDefinition<BsonDocument>(docquery);
            var sort = new BsonDocumentSortDefinition<BsonDocument>(docorder);
            return FindAsync<TEntity>(query, sort);
        }

        public TEntity Get<TEntity>(string id) where TEntity : class, IBaseData, IDataBisKey
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            return FindOneAsync<TEntity>(filter);
        }

        public TEntity Get<TEntity>(IDataBisKey key) where TEntity : class, IBaseData, IDataBisKey
        {
            return Get<TEntity>(key.Id);
        }

        public TEntity GetByCodigo<TEntity>(string codigo) where TEntity : class, IBaseData, IDataBisKey
        {
            var map = Core.Mapper.Get<TEntity>();
            var itemCodigo = map.GetItem<TEntity, string>(d => d.Codigo);
            var filter = Builders<BsonDocument>.Filter.Eq(itemCodigo.EntryRefName, codigo);
            return FindOneAsync<TEntity>(filter);
        }

        public IQueryable<TEntity> GetQuery<TEntity>() where TEntity : class, IBaseData, IDataBisKey
        {
            var filter = Builders<BsonDocument>.Filter.Empty;
            return FindAsync<TEntity>(filter).AsQueryable();
        }
    }
}
