using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interacciones.src.Models;
using Interacciones.src.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Interacciones.src.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class LikesCommentsController : ControllerBase
    {
        private readonly LikesRepository _repo;

        public LikesCommentsController(LikesRepository repo) => _repo = repo;

        [HttpGet("{id}")]
        public async Task<ActionResult<LikesComments>> Get(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            return item is null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post(LikesComments item)
        {
            await _repo.CreateAsync(item);
            return CreatedAtAction(nameof(Get), new { id = item.VideoId }, item);
        }
    }
}