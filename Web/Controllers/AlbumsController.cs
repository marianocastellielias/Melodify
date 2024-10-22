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
        public IActionResult GetAlbums()
        {
            var albums = _albumsService.GetAlbums();
            return Ok(albums);
        }

        [HttpPost("create-album")]
        public async Task<IActionResult> AddAlbum([FromBody] AddAlbumDto albumDto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized();
            }

            await _albumsService.AddAlbumAsync(albumDto, userId);

            return Ok("Álbum creado exitosamente");
        }
    }
}
