using Application.Interfaces;
using Application.Models;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = nameof(UserRole.Client))]
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

        [Authorize(Roles = nameof(UserRole.Client))]
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

        [Authorize(Roles = nameof(UserRole.Client))]
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
                if (ex.Message.Contains("esta en el carrito"))
                {
                    return Conflict(new { message = ex.Message }); 
                }

                
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = nameof(UserRole.Client))]
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

        [Authorize(Roles = nameof(UserRole.Client))]
        [HttpPatch("albums/purchase")]
        public async Task<IActionResult> MakePurchase([FromBody] AddPurchaseDto purchase)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
                if (userId == 0)
                {
                    return NotFound("No hay un usuario logueado");
                }
                await _cartService.MakePurchase(userId, purchase.PaymentMethod);
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
