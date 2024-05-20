using AutoMapper;
using ECommerce.API.DTOs.Cart;
using ECommerce.API.ECommerce.Application.Interfaces;
using ECommerce.API.ECommerce.Domain.Model;
using ECommerce.API.ECommerce.Infrastructure;
using Microsoft.EntityFrameworkCore;

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

            // Check if the product size exists and has sufficient quantity
            var ProductSizeColor = await _dbContext.ProductSizeColors.FirstOrDefaultAsync(ps =>
                ps.ProductId == cartAddEditDTO.ProductId && ps.SizeName == cartAddEditDTO.Size && ps.ColorName == cartAddEditDTO.Color);

            if (ProductSizeColor == null || ProductSizeColor.Quantity < cartAddEditDTO.Quantity)
            {
                throw new Exception("Unavailable size and color combination. Please choose another or contact support.");
            }

            // If both size and color are available in sufficient quantity, add to cart
            var existingCartItem = await _dbContext.Carts.FirstOrDefaultAsync(c =>
                c.ProductId == cartAddEditDTO.ProductId && c.ApplicationUserId == cartAddEditDTO.ApplicationUserId);

            // Item doesn't exist in the cart, so add a new entry
            var cartItem = _mapper.Map<Cart>(cartAddEditDTO);
            _dbContext.Carts.Add(cartItem);

            await _dbContext.SaveChangesAsync();
        }
        public async Task<CartGetDTO> GetCartAsync(string userId)
        {
            var carts = await _dbContext.Carts
                .Where(c => c.ApplicationUserId == userId)
                .Include(c => c.Product)
                .ToListAsync();

            var cartDTO = new CartGetDTO
            {
                ApplicationUserId = userId,
                Items = carts.Select(c => new CartItemDTO
                {
                    ProductId = c.ProductId,
                    ProductName = c.Product.ProductName,
                    Price = (decimal)c.Product.Price,
                    Quantity = c.Quantity,
                    Size = c.Size,
                    Color = c.Color
                }).ToList()
            };

            return cartDTO;
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
        public async Task<(bool success, List<CartAddEditDTO> outOfStockItems)> UpdateCartAsync(List<CartAddEditDTO> newItems)
        {
            List<CartAddEditDTO> outOfStockItems = new List<CartAddEditDTO>();
            bool success = true;
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    foreach (var item in newItems)
                    {
                        var cartItem = await _dbContext.Carts
                            .SingleOrDefaultAsync(c => c.ApplicationUserId == item.ApplicationUserId
                                                     && c.ProductId == item.ProductId
                                                     && c.Color == item.Color
                                                     && c.Size == item.Size);

                        int quantity = await _dbContext.ProductSizeColors
                            .Where(pcs => pcs.ProductId == item.ProductId
                                       && pcs.ColorName == item.Color
                                       && pcs.SizeName == item.Size)
                            .Select(c => c.Quantity)
                            .SingleOrDefaultAsync();

                        if (cartItem == null)
                        {
                            success = false;
                            continue;
                        }

                        if (item.Quantity > quantity)
                        {
                            outOfStockItems.Add(item);
                            success = false;
                        }
                        else
                        {
                            cartItem.Quantity = item.Quantity;
                            _dbContext.Carts.Update(cartItem);
                        }
                    }

                    if (success)
                    {
                        await _dbContext.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    else
                    {
                        await transaction.RollbackAsync();
                    }

                    return (success, outOfStockItems);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("Failed to update cart items.", ex);
                }
            }
        }

    }
}