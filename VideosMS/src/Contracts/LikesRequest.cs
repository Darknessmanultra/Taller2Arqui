using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideosMS.src.Contracts
{
    public class LikesRequest
    {
        public int VideoId { get; set; }
    }

    public class LikesResponse
    {
        public int VideoId { get; set; }
        public int Likes { get; set; }
    }
}