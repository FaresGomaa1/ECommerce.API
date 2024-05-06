using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.API.DTOs.WishList;
using ECommerce.API.ECommerce.Application.Interfaces;
using ECommerce.API.ECommerce.Domain.Model;
using ECommerce.API.ECommerce.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.ECommerce.Application.Repositories
{
    public class WishListRepo : IWishList
    {
        private readonly ECommerceDbContext _context;

        public WishListRepo(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task AddItemAsync(WishListAddEditDTO item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (string.IsNullOrEmpty(item.ApplicationUserId))
                throw new ArgumentException("ApplicationUserId cannot be null or empty.");

            if (!_context.Users.Any(u => u.Id == item.ApplicationUserId))
                throw new InvalidOperationException($"User with id {item.ApplicationUserId} does not exist.");

            if (!_context.Products.Any(p => p.Id == item.ProductId))
                throw new InvalidOperationException($"Product with id {item.ProductId} does not exist.");

            var wishlistItem = new Wishlist
            {
                ApplicationUserId = item.ApplicationUserId,
                ProductId = item.ProductId
            };

            _context.Wishlists.Add(wishlistItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(int productId, string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException(nameof(userId));

            if (!_context.Users.Any(u => u.Id == userId))
                throw new InvalidOperationException($"User with id {userId} does not exist.");

            if (!_context.Products.Any(p => p.Id == productId))
                throw new InvalidOperationException($"Product with id {productId} does not exist.");

            var wishlistItem = await _context.Wishlists.FirstOrDefaultAsync(wl => wl.ApplicationUserId == userId && wl.ProductId == productId);
            if (wishlistItem != null)
            {
                _context.Wishlists.Remove(wishlistItem);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<IEnumerable<WishListDTO>> GetAllItemsAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException(nameof(userId));

            if (!_context.Users.Any(u => u.Id == userId))
                throw new InvalidOperationException($"User with id {userId} does not exist.");

            var wishlistItems = _context.Wishlists
                .Where(w => w.ApplicationUserId == userId)
                .Select(w => new WishListDTO
                {
                    ApplicationUserId = w.ApplicationUserId,
                    Items = new List<WishListItemDTO>
                    {
                        new WishListItemDTO
                        {
                            ProductId = w.ProductId,
                            ProductName = w.Product.ProductName,
                            Price = (decimal) w.Product.Price
                        }
                    }
                });

            return wishlistItems.ToList();
        }

        public async Task<WishListDTO> GetItemByIdAsync(int productId)
        {
            var wishlistItem = await _context.Wishlists.FindAsync(productId);
            if (wishlistItem == null)
                return null; // or throw an exception if necessary

            var wishlistDTO = new WishListDTO
            {
                ApplicationUserId = wishlistItem.ApplicationUserId,
                Items = new List<WishListItemDTO>
                {
                    new WishListItemDTO
                    {
                        ProductId = wishlistItem.ProductId,
                        ProductName = wishlistItem.Product.ProductName,
                        Price = (decimal)wishlistItem.Product.Price
                    }
                }
            };

            return wishlistDTO;
        }
    }
}
