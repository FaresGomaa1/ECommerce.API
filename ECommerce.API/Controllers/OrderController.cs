using ECommerce.API.DTOs.Address;
using ECommerce.API.DTOs.Order;
using ECommerce.API.DTOs.OrderDetails;
using ECommerce.API.ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateNewOrder(OrderDTO newOrder)
        {
            try
            {
                if ((newOrder.AddressId == null || newOrder.AddressId == 0) && newOrder.newAddress != null)
                {
                    var addedAddress = await _addressRepository.AddAsync(newOrder.newAddress);
                    newOrder.AddressId = addedAddress.AddressId;
                }
                // Check if combinations are available in stock
                if (!_orderDetailsRepository.AreCombinationsAvailable(newOrder.OrderDetails))
                {
                    return BadRequest(new {message = "Some of the orders are not available in stock." });
                }
                decimal totalAmount = await _orderDetailsRepository.CalculateTotalAmountAsync(newOrder.OrderDetails);
                decimal tolerance = 0.01m;
                if (Math.Abs(newOrder.TotalAmount - totalAmount) > tolerance)
                {
                    return BadRequest(new {message = $"The calculated total amount ({totalAmount}) does not match the provided total amount ({newOrder.TotalAmount})." });
                }
                // Add the orderin
                int orderId = await _orderRepository.AddOrderAsync(newOrder);
                // Add order details
                await _orderDetailsRepository.AddOrderDetailsAsync(newOrder.OrderDetails, orderId);

                return Ok(new {message = "Order created successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the order: {ex.Message}");
            }
        }
    }
}