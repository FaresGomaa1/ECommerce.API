using AutoMapper;
using ECommerce.API.DTOs.Cart;
using ECommerce.API.ECommerce.Application.Interfaces;
using ECommerce.API.ECommerce.Domain.Model;
using ECommerce.API.ECommerce.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.ECommerce.Application.Repositories
{
    public class CartRepo : ICartRepo
    {
        private readonly ECommerceDbContext _dbContext;
        private readonly IMapper _mapper;

        public CartRepo(ECommerceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task AddToCartAsync(CartAddEditDTO cartAddEditDTO)
        {
            if (cartAddEditDTO == null)
                throw new ArgumentNullException(nameof(cartAddEditDTO));

            var existingCartItem = await _dbContext.Carts.FirstOrDefaultAsync(c =>
                c.ProductId == cartAddEditDTO.ProductId && c.ApplicationUserId == cartAddEditDTO.ApplicationUserId);

                // Item doesn't exist in the cart, so add a new entry
                var cartItem = _mapper.Map<Cart>(cartAddEditDTO);
                _dbContext.Carts.Add(cartItem);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<CartGetDTO>> GetAllCartsAsync()
        {
            var cartItems = await _dbContext.Carts.ToListAsync();

            if (cartItems == null || !cartItems.Any())
            {
                return Enumerable.Empty<CartGetDTO>();
            }

            return _mapper.Map<IEnumerable<CartGetDTO>>(cartItems);
        }
        public async Task<CartGetDTO> GetCartAsync(int productId, string applicationUserId)
        {
            Cart cartItem = null;

            if (productId != 0 && applicationUserId != null)
            {
                cartItem = await _dbContext.Carts.FirstOrDefaultAsync(c =>
                    c.ProductId == productId && c.ApplicationUserId == applicationUserId);
            }
            else if (productId != 0)
            {
                cartItem = await _dbContext.Carts.FirstOrDefaultAsync(c => c.ProductId == productId);
            }
            else if (!string.IsNullOrEmpty(applicationUserId))
            {
                cartItem = await _dbContext.Carts.FirstOrDefaultAsync(c => c.ApplicationUserId == applicationUserId);
            }
            else
            {
                // Handle the case where both productId and applicationUserId are invalid
                throw new ArgumentException("Either productId or applicationUserId must be provided.");
            }

            return _mapper.Map<CartGetDTO>(cartItem);
        }
        public async Task<bool> RemoveFromCartAsync(int productId, string applicationUserId)
        {
            if (productId == 0 && string.IsNullOrEmpty(applicationUserId))
            {
                // Handle the case where both productId and applicationUserId are invalid
                throw new ArgumentException("Either productId or applicationUserId must be provided.");
            }

            Cart cartItem = null;

            if (productId != 0 && !string.IsNullOrEmpty(applicationUserId))
            {
                cartItem = await _dbContext.Carts.FirstOrDefaultAsync(c =>
                    c.ProductId == productId && c.ApplicationUserId == applicationUserId);
            }
            else if (productId != 0)
            {
                cartItem = await _dbContext.Carts.FirstOrDefaultAsync(c => c.ProductId == productId);
            }
            else if (!string.IsNullOrEmpty(applicationUserId))
            {
                cartItem = await _dbContext.Carts.FirstOrDefaultAsync(c => c.ApplicationUserId == applicationUserId);
            }

            if (cartItem != null)
            {
                _dbContext.Carts.Remove(cartItem);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateCartAsync(CartAddEditDTO cartAddEditDTO)
        {
            if (cartAddEditDTO == null)
                throw new ArgumentNullException(nameof(cartAddEditDTO));

            var existingCartItem = await _dbContext.Carts.FirstOrDefaultAsync(c =>
                c.ProductId == cartAddEditDTO.ProductId && c.ApplicationUserId == cartAddEditDTO.ApplicationUserId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity = cartAddEditDTO.Quantity;
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}