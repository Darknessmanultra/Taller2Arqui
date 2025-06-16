using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using VideosMS.src.Models;

namespace VideosMS.src.Repositories
{
    public class VideoRepository
    {
        private readonly IMongoCollection<Video> _videos;

        public VideoRepository(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDB:ConnectionString"]);
            var database = client.GetDatabase(config["MongoDB:Database"]);
            _videos = database.GetCollection<Video>("Videos");
        }

        public async Task<Video?> GetByIdAsync(int id) =>
        await _videos.Find(i => i.Id == id && !i.Borrado).FirstOrDefaultAsync();

        public async Task<List<Video>> GetAllAsync() =>
            await _videos.Find(i => !i.Borrado).ToListAsync();

        public async Task CreateAsync(Video video) =>
            await _videos.InsertOneAsync(video);

        public async Task UpdateAsync(Video video) => await _videos.ReplaceOneAsync(i => i.Id == video.Id, video);

        public async Task SoftDeleteAsync(int id) =>
        await _videos.UpdateOneAsync(
            i => i.Id == id,
            Builders<Video>.Update.Set(x => x.Borrado, true));
    }
}