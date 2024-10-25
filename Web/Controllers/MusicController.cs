using Application.DTOs;
using Application.Interfaces;
using Application.Models;
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
        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpGet("Music/GetAll")]
        public IActionResult GetSongs()
        {
            var songs = _musicService.GetAllMusic();
            return Ok(songs);
        }
        [HttpGet("Music/GetMusicDetails")]
        public IActionResult GetMusicDetails(int id)
        {
            var song = _musicService.GetMusic(id);
            return Ok(song);
        }
        //[Authorize(Roles = nameof(UserRole.Artist))]
        [HttpPost("Music/AddMusic/{idAlbum}")]
        public IActionResult AddMusic([FromRoute]int idAlbum, [FromBody] AddMusicDto addMusicDto)
        {
            _musicService.AddSong(idAlbum, addMusicDto);
            return Ok("Cancion agregada.");
        }
        [HttpPut("Music/UpdateMusic/{id}")]
        public IActionResult UpdateMusic(int id, [FromBody] UpdateMusicDto updateMusicDto)
        {
            _musicService.UpdateMusic(id, updateMusicDto);
            return Ok("Musica actualizada exitosamente !!");
        }
        [HttpDelete("Music/DeleteMusic/{id}")]
        public IActionResult DeleteMusic(int id)
        {
            _musicService.DeleteMusic(id);
            return NoContent();
        }
    }
}
