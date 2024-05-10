using AutoMapper;
using ECommerce.API.DTOs.OrderDetails;
using ECommerce.API.ECommerce.Application.Interfaces;
using ECommerce.API.ECommerce.Domain.Model;
using ECommerce.API.ECommerce.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.ECommerce.Application.Repositories
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private readonly ECommerceDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrderDetailsRepository(ECommerceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task AddOrderDetailsAsync(List<OrderDetailsAdd> orderDetailsList)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                foreach (var orderDetails in orderDetailsList)
                {
                    // Retrieve the current quantity from the database
                    var productSizeColor = await _dbContext.ProductSizeColors
                        .FirstOrDefaultAsync(ps => ps.ProductId == orderDetails.ProductId &&
                                                     ps.ColorName == orderDetails.Color &&
                                                     ps.SizeName == orderDetails.Size);

                    if (productSizeColor != null)
                    {
                        // Subtract the order quantity from the current quantity
                        productSizeColor.Quantity -= orderDetails.Quantity;

                        if (productSizeColor.Quantity < 0)
                        {
                            // If quantity becomes negative, rollback the transaction
                            await transaction.RollbackAsync();
                            throw new InvalidOperationException("Product size color combination is out of stock.");
                        }
                        else if (productSizeColor.Quantity == 0)
                        {
                            // If quantity becomes zero, remove the product size color
                            _dbContext.ProductSizeColors.Remove(productSizeColor);
                        }

                        // Update the database with the new quantity
                        _dbContext.ProductSizeColors.Update(productSizeColor);

                        // Map DTO to domain model
                        var newOrderDetails = _mapper.Map<OrderDetails>(orderDetails);

                        // Add order details to the database
                        await _dbContext.OrderDetails.AddAsync(newOrderDetails);
                    }
                    else
                    {
                        await transaction.RollbackAsync();
                        throw new KeyNotFoundException("Product size color combination not found.");
                    }
                }

                // Save changes and commit the transaction after processing all order details
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }
        public async Task<IEnumerable<OrderDetailsGetDTO>> GetOrderDetailsByOrderIdAsync(int orderId)
        {
            // Retrieve order details from the database for the specified orderId
            var orderDetails = await _dbContext.OrderDetails
                .Where(od => od.OrderId == orderId)
                .ToListAsync();

            // Map order details to OrderDetailsDTO objects
            var orderDetailsDTOs = orderDetails.Select(od => _mapper.Map<OrderDetailsGetDTO>(od));

            return orderDetailsDTOs;
        }
        public bool AreCombinationsAvailable(List<OrderDetailsAdd> orderDetailsList)
        {
            foreach (var item in orderDetailsList)
            {
                // Get the total quantity available for the combination of size and color
                int availableQuantity = _dbContext.ProductSizeColors
                    .Where(p => p.ProductId == item.ProductId && p.ColorName == item.Color && p.SizeName == item.Size)
                    .Sum(p => p.Quantity);

                // Check if available quantity is sufficient for the current order detail
                if (availableQuantity < item.Quantity)
                {
                    return false;
                }
            }

            return true;
        }
    }
}