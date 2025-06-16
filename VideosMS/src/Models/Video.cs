using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VideosMS.src.Models
{
    public class Video
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }
        [BsonElement("titulo")]
        public string Titulo { get; set; }
        [BsonElement("descripcion")]
        public string Descripcion { get; set; }
        [BsonElement("genero")]
        public string Genero { get; set; }
        [BsonElement("borrado")]
        public bool Borrado { get; set; } = false;
    }
}