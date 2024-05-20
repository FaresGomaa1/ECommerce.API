using System;
using System.Threading.Tasks;
using ECommerce.API.DTOs.Cart;
using ECommerce.API.ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepo _cartRepo;
        public CartController(ICartRepo cartRepo)
        {
            _cartRepo = cartRepo ?? throw new ArgumentNullException(nameof(cartRepo));
        }
        [Authorize(Roles = "customer")]
        [HttpPost]
        public async Task<ActionResult> AddToCart(CartAddEditDTO cartAddEditDTO)
        {
            if (cartAddEditDTO == null)
                return BadRequest(new { message = "Cart data is null" });

            try
            {
                await _cartRepo.AddToCartAsync(cartAddEditDTO);
                return Ok(new {message ="Item added to cart successfully" });
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<ActionResult> GetAllCarts(string userId)
        {
            try
            {
                var cart = await _cartRepo.GetCartAsync(userId);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        [Authorize]
        [HttpGet("{productId}/{applicationUserId}")]
        public async Task<ActionResult> GetCart(int productId = 0, string applicationUserId = "")
        {
            try
            {
                var cart = await _cartRepo.GetCartAsync(productId, applicationUserId);
                if (cart == null)
                    return NotFound(new {message = "Cart not found" });

                return Ok(cart);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        [Authorize]
        [HttpDelete("{productId}/{applicationUserId}")]
        public async Task<ActionResult> RemoveFromCart(int productId, string applicationUserId)
        {
            try
            {
                var result = await _cartRepo.RemoveFromCartAsync(productId, applicationUserId);
                if (!result)
                    return NotFound(new {message = "Cart item not found" });

                return Ok(new {message = "Cart item removed successfully" });
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        [Authorize]
        [HttpPut("update")]
        public async Task<ActionResult> UpdateCart(List<CartAddEditDTO> newItems)
        {
            try
            {
                var (success, outOfStockItems) = await _cartRepo.UpdateCartAsync(newItems);

                if (!success)
                {
                    return BadRequest(new { message = "Unable to update cart items.", outOfStockItems });
                }

                return Ok(new { message = "Cart updated successfully." });
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        private ActionResult HandleException(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
        }

    }
}