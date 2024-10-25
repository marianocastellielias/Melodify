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
        [HttpGet("GetCart", Name = "GetMyCart")]
        public IActionResult GetMyCart()
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
                if (userId == 0)
                {
                    return NotFound("No hay un usuario logueado");
                }
                return Ok(_cartService.GetCart(userId));
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAllMyPurchases")]
        public IActionResult GetAllMyPurchases()
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
                if (userId == 0)
                {
                    return NotFound("No hay un usuario logueado");
                }
                return Ok(_cartService.GetAllPurchases(userId));
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("albums/add")]
        public IActionResult AddAlbumCart([FromBody] AddAlbumToCartRequest request)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
                if (userId == 0)
                {
                    return NotFound("No hay un usuario logueado");
                }
                var cartDto = _cartService.AddAlbumCart(request.AlbumId, request.Quantity, userId);
                return CreatedAtAction("GetMyCart", cartDto);
            }
            catch(NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("albums/remove/{id}")]
        public IActionResult RemoveAlbumCart(int id)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
                if (userId == 0)
                {
                    return NotFound("No hay un usuario logueado");
                }
                _cartService.RemoveAlbumCart(id, userId);
                return NoContent();
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPatch("albums/purchase")]
        public IActionResult MakePurchase([FromBody] AddPurchaseDto purchase)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
                if (userId == 0)
                {
                    return NotFound("No hay un usuario logueado");
                }
                _cartService.MakePurchase(userId, purchase.PaymentMethod);
                return Ok("Compra realizada con exito !");
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
