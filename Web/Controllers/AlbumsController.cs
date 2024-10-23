using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumsService _albumsService;
        
        
        public AlbumsController(IAlbumsService albumsService)
        {
            _albumsService = albumsService;
        }

        [HttpGet]
        [Authorize(Roles = ((int)UserRole.Artist))]
        public IActionResult GetAlbums()
        {
            var albums = _albumsService.GetAlbums();
            return Ok(albums);
        }

        //[Authorize(Roles = nameof(UserRole.Artist))]
        [HttpPost("create-album")]
        public async Task<IActionResult> AddAlbum([FromBody] AddAlbumDto albumDto)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");

            

            await _albumsService.AddAlbumAsync(albumDto, userId);

            return Ok("Álbum creado exitosamente");
        }

        [HttpGet("my-albums")]
        public async Task<IActionResult> GetMyAlbums()
        {

            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");

            // var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            var albumsDto = await _albumsService.GetMyAlbums(userId);

            return Ok(albumsDto);
        }

        
    }
}
