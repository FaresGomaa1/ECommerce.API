﻿using ECommerce.API.ECommerce.Domain.Model;

namespace ECommerce.API.DTOs.Cart
{
    public class CartItemDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}