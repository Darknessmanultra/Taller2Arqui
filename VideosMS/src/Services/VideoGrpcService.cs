using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using VideoService.Grpc;
using VideosMS.src.Models;
using VideosMS.src.Repositories;

namespace VideosMS.src.Services
{
    public class VideoGrpcService : Grpc.VideoService.VideoServiceBase
    {
        private readonly VideoRepository _repo;
        private readonly IMapper _mapper;

        public VideoGrpcService(VideoRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public override async Task<VideoResponse> GetVideoById(VIdeoRequest request, ServerCallContext context)
        {
            var video = await _repo.GetByIdAsync(request.Id);
            if (video == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Video no encontrado"));

            return MapItemToReply(video);
        }

        public override async Task<VideoResponse> CreateVideo(UploadVideoRequest request, ServerCallContext context)
        {
            var video = new Video
            {
                Titulo = request.titulo,
                Descripcion = request.description,
                Genero= request.genero
            };
            await _repo.CreateAsync(video);
            return MapItemToReply(video);
        }

        public override async Task<Empty> UpdateItem(UpdateVideoRequest request, ServerCallContext context)
        {
            var item = await _repo.GetByIdAsync(request.Id);
            if (item == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Video no encontrado"));

            item.Titulo = request.titulo;
            item.Description = request.descripcion;
            item.Genero = request.genero;

            await _repo.UpdateAsync(item);
            return new Empty();
        }

        public override async Task<Empty> SoftDeleteItem(VideoRequest request, ServerCallContext context)
        {
            var item = await _repo.GetByIdAsync(request.Id);
            if (item == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Video no encontrado"));

            await _repo.SoftDeleteAsync(request.Id);
            return new Empty();
        }

        private VideoResponse MapItemToReply(Video video) => new()
        {
            Id = video.Id,
            Titulo = video.Titulo,
            Descripcion = video.Descripcion,
            Genero = video.Genero,
        };
    }
}