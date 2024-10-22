using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumController : ControllerBase
    {
        private readonly IUserAlbumService _userAlbumService;

        public AlbumController(IUserAlbumService userAlbumService)
        {
            _userAlbumService = userAlbumService;
        }

        [HttpGet("my-albums")]
        public async Task<IActionResult> GetMyAlbums()
        {
       
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized(); 
            }

            
            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized(); 
            }

            
            var albumsDto = await _userAlbumService.GetMyAlbums(userId);

            return Ok(albumsDto);
        }
    }
}