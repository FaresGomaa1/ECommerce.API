using ECommerce.API.DTOs.Address;
using ECommerce.API.DTOs.Order;
using ECommerce.API.DTOs.OrderDetails;
using ECommerce.API.ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailsRepository _orderDetailsRepository;
        private readonly IAddressRepo _addressRepository;

        public OrderController(IOrderRepository orderRepository, IOrderDetailsRepository orderDetailsRepository, IAddressRepo addressRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _orderDetailsRepository = orderDetailsRepository ?? throw new ArgumentNullException(nameof(orderDetailsRepository));
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewOrder(OrderDTO newOrder, List<OrderDetailsAdd> OrderDetails, AddressAddEditDTO newAddress)
        {
            try
            {
                if (newOrder.AddressId == null && newAddress != null)
                {
                    var addedAddress = await _addressRepository.AddAsync(newAddress);
                    newOrder.AddressId = addedAddress.AddressId;
                }
                // Check if combinations are available in stock
                if (!_orderDetailsRepository.AreCombinationsAvailable(OrderDetails))
                {
                    return BadRequest("Some of the orders are not available in stock.");
                }
                // Add the order
                await _orderRepository.AddOrderAsync(newOrder);
                // Add order details
                await _orderDetailsRepository.AddOrderDetailsAsync(OrderDetails);

                return Ok("Order created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the order: {ex.Message}");
            }
        }
    }
}