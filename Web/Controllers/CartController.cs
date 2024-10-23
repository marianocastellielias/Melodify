using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpGet("GetCart", Name = "GetMyCart")]
        public IActionResult GetMyCart()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
            if (userId == 0)
            {
                return NotFound("No hay un usuario logueado");
            }
            return Ok(_cartService.GetCart(userId));
        }
        [HttpPost("{idAlbum}")]
        public IActionResult AddAlbumCart(int idAlbum)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
            if (userId == 0)
            {
                return NotFound("No hay un usuario logueado");
            }
            var cartDto = _cartService.AddAlbumCart(idAlbum, userId);
            return CreatedAtAction("GetMyCart", cartDto);
        }
    }
}
