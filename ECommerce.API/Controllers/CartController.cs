using System;
using System.Threading.Tasks;
using ECommerce.API.DTOs.Cart;
using ECommerce.API.ECommerce.Application.Interfaces;
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

        [HttpGet("{userId}")]
        public async Task<ActionResult> GetAllCarts(string userId)
        {
            try
            {
                var carts = await _cartRepo.GetAllCartsAsync(userId);
                return Ok(carts);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
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

        [HttpDelete("{productId}/{applicationUserId}")]
        public async Task<ActionResult> RemoveFromCart(int productId = 0, string applicationUserId = "Igonre")
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

        [HttpPut]
        public async Task<ActionResult> UpdateCart(CartAddEditDTO cartAddEditDTO)
        {
            if (cartAddEditDTO == null)
                return BadRequest(new {message = "Cart data is null" });

            if (cartAddEditDTO.Quantity <= 0)
                return BadRequest(new {message = "Quantity must be greater than zero" });

            try
            {
                var result = await _cartRepo.UpdateCartAsync(cartAddEditDTO);
                if (!result)
                    return NotFound(new {message = "Cart item not found" });

                return Ok(new {message = "Cart item updated successfully" });
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