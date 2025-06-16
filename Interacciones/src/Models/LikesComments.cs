using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Interacciones.src.Models
{
    public class LikesComments
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int VideoId;
        [BsonElement("likes")]
        public int Likes;
        [BsonElement("commentlist")]
        public List<Commenta> Comments { get; set; } = new();
    }

    public class Commenta
    {
        [BsonElement("videoId")]
        public int VideoId { get; set; }
        [BsonElement("userId")]
        public int UserId { get; set; }
        [BsonElement("comment")]
        public string Comment { get; set; } = string.Empty;
    }
}