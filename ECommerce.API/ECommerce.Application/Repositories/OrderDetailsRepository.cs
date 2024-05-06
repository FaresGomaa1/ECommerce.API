using AutoMapper;
using ECommerce.API.DTOs.OrderDetails;
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
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private readonly ECommerceDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrderDetailsRepository(ECommerceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task AddOrderDetailsAsync(OrderDetailsDTO orderDetails)
        {
            // Map DTO to domain model
            var newOrderDetails = _mapper.Map<OrderDetails>(orderDetails);

            // Add order details to the database
            await _dbContext.OrderDetails.AddAsync(newOrderDetails);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderDetailsDTO>> GetOrderDetailsByOrderIdAsync(int orderId)
        {
            // Retrieve order details from the database for the specified orderId
            var orderDetails = await _dbContext.OrderDetails
                .Where(od => od.OrderId == orderId)
                .ToListAsync();

            // Map order details to OrderDetailsDTO objects
            var orderDetailsDTOs = orderDetails.Select(od => _mapper.Map<OrderDetailsDTO>(od));

            return orderDetailsDTOs;
        }
    }
}