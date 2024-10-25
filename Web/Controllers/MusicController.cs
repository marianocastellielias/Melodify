using Application.DTOs;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private IMusicService _musicService;

        public MusicController(IMusicService musicService)
        {
            _musicService = musicService;
        }
        //[Authorize(Roles = nameof(UserRole.Artist))]
        [HttpPost("AddMusic/{idAlbum}")]
        public IActionResult AddMusic([FromRoute]int idAlbum,[FromBody] AddMusicDto addMusicDto)
        {
            _musicService.AddSong(idAlbum, addMusicDto);

            return Ok("Cancion agregada.");
        }
    }
}
