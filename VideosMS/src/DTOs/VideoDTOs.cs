using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideosMS.src.DTOs
{
    public class CreateVideoDTO
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Genero { get; set; }
    }

    public class UpdateVideoDTO
    {
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? Genero { get; set; }
    }

    public class VideoDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Genero { get; set; }
        public int Likes { get; set; }
    }
}