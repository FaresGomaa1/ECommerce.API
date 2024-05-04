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
        public async Task<IActionResult> AddToCart(CartAddEditDTO cartAddEditDTO)
        {
            if (cartAddEditDTO == null)
                return BadRequest("Cart data is null");

            try
            {
                await _cartRepo.AddToCartAsync(cartAddEditDTO);
                return Ok("Item added to cart successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCarts()
        {
            try
            {
                var carts = await _cartRepo.GetAllCartsAsync();
                return Ok(carts);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpGet("{productId}/{applicationUserId}")]
        public async Task<IActionResult> GetCart(int productId = 0, string applicationUserId = "")
        {
            try
            {
                var cart = await _cartRepo.GetCartAsync(productId, applicationUserId);
                if (cart == null)
                    return NotFound("Cart not found");

                return Ok(cart);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpDelete("{productId}/{applicationUserId}")]
        public async Task<IActionResult> RemoveFromCart(int productId = 0, string applicationUserId = "Igonre")
        {

            try
            {
                var result = await _cartRepo.RemoveFromCartAsync(productId, applicationUserId);
                if (!result)
                    return NotFound("Cart item not found");

                return Ok("Cart item removed successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCart(CartAddEditDTO cartAddEditDTO)
        {
            if (cartAddEditDTO == null)
                return BadRequest("Cart data is null");

            if (cartAddEditDTO.Quantity <= 0)
                return BadRequest("Quantity must be greater than zero");

            try
            {
                var result = await _cartRepo.UpdateCartAsync(cartAddEditDTO);
                if (!result)
                    return NotFound("Cart item not found");

                return Ok("Cart item updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
    }
}