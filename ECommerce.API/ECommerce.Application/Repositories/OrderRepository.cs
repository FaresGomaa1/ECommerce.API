using AutoMapper;
using ECommerce.API.DTOs.Address;
using ECommerce.API.DTOs.Order;
using ECommerce.API.ECommerce.Application.Interfaces;
using ECommerce.API.ECommerce.Domain.Model;
using ECommerce.API.ECommerce.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.API.ECommerce.Application.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMapper _mapper;
        private readonly ECommerceDbContext _dbContext;
        public OrderRepository(ECommerceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<int> AddOrderAsync(OrderDTO order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), "Order cannot be null.");
            }

            // Map DTO to domain model
            var newOrder = new Order()
            {
                OrderDate = DateTime.Now,
                TotalAmount = order.TotalAmount,
                OrderStatus = "Pending",
                ApplicationUserId = order.ApplicationUserId,
                AddressId = order.AddressId
            };

            // Add order to the database
            await _dbContext.Orders.AddAsync(newOrder);
            await _dbContext.SaveChangesAsync();

            // Return the ID of the newly created order
            return newOrder.Id;
        }
        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync(string userId)
        {
            // Retrieve all orders from the database
            var orders = await _dbContext.Orders
                .Where(o => o.ApplicationUserId == userId)
                .ToListAsync();
            // Map orders to OrderDTO objects
            var orderDTOs = orders.Select(o => _mapper.Map<OrderDTO>(o));
            return orderDTOs;
        }
    }
}