using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interacciones.src.Contracts
{
    public class CommentsRequest
    {
        public int VideoId { get; set; }
    }

    public class CommentsResponse
    {
        public int VideoId { get; set; }
    }
}