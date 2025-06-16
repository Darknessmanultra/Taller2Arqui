using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interacciones.src.Models;
using MongoDB.Driver;

namespace Interacciones.src.Repositories
{
    public class CommentRepository
    {
        private readonly IMongoCollection<Commenta> _comentarios;
    }
}