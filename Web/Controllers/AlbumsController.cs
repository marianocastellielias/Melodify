using Application.DTOs;
using Application.Interfaces;
using Application.Models;
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

        [Authorize(Roles = nameof(UserRole.Artist))]
        [HttpPost("create-album")]
        public IActionResult AddAlbum([FromBody] AddAlbumDto albumDto)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
            _albumsService.AddAlbumAsync(albumDto, userId);
            return Ok("Álbum creado exitosamente");
        }

        [Authorize(Roles = nameof(UserRole.Artist) + "," + nameof(UserRole.Client))]
        [HttpGet("my-albums")]
        public IActionResult GetMyAlbums()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
            var albumsDto = _albumsService.GetMyAlbums(userId);

            return Ok(albumsDto);
        }

        [Authorize(Roles = nameof(UserRole.Artist))]
        [HttpPut("update-album/{id}")]
        public IActionResult UpdateAlbum([FromBody] UpdateAlbumDto albumDto, int id)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                _albumsService.UpdateAlbumAsync(albumDto, userId, id);
                return Ok("Álbum actualizado exitosamente");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Artist))]
        [HttpDelete("/Delete/{id}")]
        public IActionResult DeleteUser([FromRoute] int id)
        {
            var user = _albumsService.DeleteAlbumAsync(id);
            return Ok("Album eliminado");
        }


    }
}