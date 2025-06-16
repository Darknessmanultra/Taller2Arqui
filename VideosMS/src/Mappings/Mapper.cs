using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using VideosMS.src.DTOs;
using VideosMS.src.Models;
using Video.Grpc;

namespace VideosMS.src.Mappings
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            // DTO <-> Domain
            CreateMap<Video, VideoDTO>();
            CreateMap<CreateVideoDTO, Video>();
            CreateMap<UpdateVideoDTO, Video>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // Domain <-> gRPC
            CreateMap<Video, VideoResponse>();

            CreateMap<UploadVideoRequest, Video>();

            CreateMap<UpdateVideoRequest, Video>();
        }
    }
}