using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interacciones.src.Models;
using MongoDB.Driver;

namespace Interacciones.src.Repositories
{
    public class LikesRepository
    {
        private readonly IMongoCollection<LikesComments> _likesComments;

        public LikesRepository(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDB:ConnectionString"]);
            var database = client.GetDatabase(config["MongoDB:Database"]);
            _likesComments = database.GetCollection<LikesComments>("LikesComments");
        }

        public async Task<LikesComments?> GetByIdAsync(int id) =>
        await _likesComments.Find(i => i.VideoId == id).FirstOrDefaultAsync();

        public async Task CreateAsync(LikesComments item) =>
            await _likesComments.InsertOneAsync(item);
    }
}