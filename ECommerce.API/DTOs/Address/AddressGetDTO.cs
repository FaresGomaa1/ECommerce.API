﻿using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.DTOs.Address
{
    public class AddressGetDTO
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int BlockNumber { get; set; }
        public int FloorLevel { get; set; }
    }
}
