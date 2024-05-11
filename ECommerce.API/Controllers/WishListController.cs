using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.API.DTOs.WishList;
using ECommerce.API.ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWishList _wishListRepo;

        public WishListController(IWishList wishListRepo)
        {
            _wishListRepo = wishListRepo;
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(WishListAddEditDTO item)
        {
            try
            {
                await _wishListRepo.AddItemAsync(item);
                return Ok(new {message = "Item Addedd"});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding item to wishlist: {ex.Message}");
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAllItems(string userId)
        {
            try
            {
                var wishlistItems = await _wishListRepo.GetAllItemsAsync(userId);
                return Ok(wishlistItems);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving wishlist items: {ex.Message}");
            }
        }

        [HttpGet("{userId}/{productId}")]
        public async Task<IActionResult> GetItemById(string userId, int productId)
        {
            try
            {
                var wishlistItem = await _wishListRepo.GetItemByIdAsync(productId);
                if (wishlistItem == null)
                    return NotFound(new {message = $"Wishlist item with productId {productId} not found." });

                return Ok(wishlistItem);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving wishlist item: {ex.Message}");
            }
        }

        [HttpDelete("{userId}/{productId}")]
        public async Task<IActionResult> DeleteItem(string userId, int productId)
        {
            try
            {
                await _wishListRepo.DeleteItemAsync(productId, userId);
                return Ok(new {message = "Item deleted" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting wishlist item: {ex.Message}");
            }
        }
    }
}