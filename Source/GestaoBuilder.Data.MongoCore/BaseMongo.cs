using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using GestaoBuilder.CoreShared.Contracts;
using GestaoBuilder.Shared;
using GestaoBuilder.Shared.Data;
using GestaoBuilder.Shared.Data.Business;
using GestaoBuilder.Shared.Data.System.Domain;
using GestaoBuilder.Shared.Helpers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace GestaoBuilder.Data.MongoCore
{
    public abstract class BaseMongo
    {
        public IDataBisRead Reader { get; protected set; }
        public ICoreSupport Core { get; }
        public MongoClient Client { get; private set; }
        public IMongoDatabase DataBase { get; private set; }

        protected BaseMongo(ICoreSupport core, IDataBisRead reader)
        {
            Reader = reader;
            Core = core;
        }

        protected void OpenConnection()
        {
            if (Client == null) {
                var empresa = Core.UsuarioEntry.EmpresaEmUSo;
                var empresaDo = empresa.EmpresaMaster ?? (empresa.EmpresaPai ?? empresa);
                Client = new MongoClient(empresaDo.ConnectionStringDb);
                DataBase = Client.GetDatabase(empresaDo.DatabaseName);
            }
        }

        protected IFindFluent<BsonDocument, TEntity> FindSyncFluent<TEntity>(FilterDefinition<BsonDocument> filtro, IDataBisRead reader, SortDefinition<BsonDocument> sort = null) where TEntity : class, IBaseData, IDataBisKey
        {
            OpenConnection();
            var map = Core.Mapper.Get<TEntity>();
            var collection = DataBase.GetCollection<BsonDocument>(map.DocumentName);
            var options = new FindOptions {
                BatchSize = 2,
                NoCursorTimeout = false,
            };
            string idProperty = ObjectHelper<TEntity>.GetPropertyName(p => p.Id);
            var entityClassMap = MongoHelper.GetClassMap(idProperty, typeof(TEntity), Core.Mapper, reader);
            entityClassMap.Freeze();
            var serializer = (IBsonSerializer<TEntity>)MongoHelper.GetBsonSerializer(typeof(TEntity), entityClassMap);
            var findResult = collection.Find(filtro, options);
            if (sort != null) {
                findResult.Sort(sort);
            }
            return findResult.As(serializer);
        }

        protected TEntity FindOneAsync<TEntity>(FilterDefinition<BsonDocument> filtro,
            SortDefinition<BsonDocument> sort = null)
            where TEntity : class, IBaseData, IDataBisKey
        {
            return FindAsync<TEntity>(filtro, sort, 1).FirstOrDefault();
        }

        protected IEnumerable<TEntity> FindAsync<TEntity>(FilterDefinition<BsonDocument> filtro, SortDefinition<BsonDocument> sort = null, int? limit = null, int? skip = null)
            where TEntity : class, IBaseData, IDataBisKey
        {
            OpenConnection();
            var map = Core.Mapper.Get<TEntity>();
            var collection = DataBase.GetCollection<BsonDocument>(map.DocumentName);
            string idProperty = ObjectHelper<TEntity>.GetPropertyName(p => p.Id);
            var entityClassMap = MongoHelper.GetClassMap(idProperty, typeof(TEntity), Core.Mapper, Reader);
            entityClassMap.Freeze();
            var options = new FindOptions<BsonDocument> {
                BatchSize = 2,
                NoCursorTimeout = false,
                Sort = sort,
                Skip = skip,
                Limit = limit,
            };
            if (!BsonClassMap.IsClassMapRegistered(entityClassMap.ClassType)) {
                BsonClassMap.RegisterClassMap(entityClassMap);
            }
            using (var cursor = collection.FindAsync(filtro, options).GetAwaiter().GetResult()) {
                while (cursor.MoveNextAsync().GetAwaiter().GetResult()) {
                    var batch = cursor.Current;
                    foreach (var document in batch)
                    {
                        var obj = BsonSerializer.Deserialize<TEntity>(document);
                        if (obj is IDataModificada mod)
                        {
                            mod.LimparModificados();   
                        }
                        yield return obj;
                    }
                }
            }
        }
    }
}
