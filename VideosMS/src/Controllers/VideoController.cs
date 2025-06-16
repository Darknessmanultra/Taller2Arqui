using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using VideosMS.src.Contracts;
using VideosMS.src.DTOs;
using VideosMS.src.Models;
using VideosMS.src.Repositories;

namespace Videos.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly VideoRepository _repo;
        private readonly IMapper _mapper;
        private readonly IRequestClient<LikesRequest> _requestLikes;

        public VideoController(VideoRepository repo, IMapper mapper,IRequestClient<LikesRequest> requestLikes)
        {
            _repo = repo;
            _mapper = mapper;
            _requestLikes = requestLikes;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VideoDTO>>> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<VideoDTO>>(items));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VideoDTO>> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null || item.Borrado)
                return NotFound();
            var dto =_mapper.Map<VideoDTO>(item);
            try
            {
                var response = await _requestLikes.GetResponse<LikesResponse>(new LikesRequest { VideoId = id });
                dto.Likes = response.Message.Likes;
            }
            catch
            {
                dto.Likes = 0;
            }
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<VideoDTO>> Create(CreateVideoDTO dto)
        {
            var item = _mapper.Map<Video>(dto);
            await _repo.CreateAsync(item);

            var readDto = _mapper.Map<VideoDTO>(item);
            return CreatedAtAction(nameof(GetById), new { id = readDto.Id }, readDto);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, UpdateVideoDTO dto)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null || item.Borrado)
                return NotFound();

            _mapper.Map(dto, item);

            await _repo.UpdateAsync(item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null || item.Borrado)
                return NotFound();

            await _repo.SoftDeleteAsync(id);
            return NoContent();
        }
    }
}