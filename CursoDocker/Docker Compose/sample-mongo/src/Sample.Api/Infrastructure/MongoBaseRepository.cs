﻿using MongoDB.Driver;
using Sample.Api.Domain.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Api.Infrastructure
{
    public class MongoBaseRepository<T> : IMongoBaseRepository<T> where T : IEntity
    {
        protected readonly IMongoClient Client;

        protected readonly IMongoCollection<T> Collection;

        public MongoBaseRepository(IMongoClient client)
        {
            Client = client;

            Collection = client
                .GetDatabase("SampleMongoDb")
                .GetCollection<T>(typeof(T).Name);
        }

        public virtual async Task AddAsync(T obj, CancellationToken cancellationToken)
        {
            await Collection.InsertOneAsync(obj, cancellationToken: cancellationToken);
        }

        public virtual async Task UpdateAsync(T obj, CancellationToken cancellationToken)
        {
            await Collection.ReplaceOneAsync(x => x.Id == obj.Id, obj, cancellationToken: cancellationToken);
        }

        public virtual async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await Collection.Find(Builders<T>.Filter.Eq(x => x.Id, id))
                .SingleOrDefaultAsync(cancellationToken);
        }

        public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await Collection.DeleteOneAsync(x => x.Id == id, cancellationToken);
        }

        public virtual async Task<List<T>> FindAsync(CancellationToken cancellationToken)
        {
            return await Collection.Find(_ => true).ToListAsync(cancellationToken);
        }

        public virtual Task<IClientSessionHandle> StartSessionAsync(CancellationToken cancellationToken)
        {
            return Client.StartSessionAsync(cancellationToken: cancellationToken);
        }
    }
}