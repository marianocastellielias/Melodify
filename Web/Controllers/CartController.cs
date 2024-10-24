using Application.Interfaces;
using Application.Models;
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
        [HttpGet("cart/GetCart", Name = "GetMyCart")]
        public IActionResult GetMyCart()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userId == 0)
            {
                return NotFound("No hay un usuario logueado");
            }
            return Ok(_cartService.GetCart(userId));
        }
        [HttpPost("cart/albums")]
        public IActionResult AddAlbumCart([FromBody] AddAlbumToCartRequest request)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userId == 0)
            {
                return NotFound("No hay un usuario logueado");
            }
            var cartDto = _cartService.AddAlbumCart(request.AlbumId, userId);
            return CreatedAtAction("GetMyCart", cartDto);
        }
        [HttpDelete("cart/albums/{idAlbum}")]
        public IActionResult RemoveAlbumCart(int idAlbum)
        {

        }
    }
}
