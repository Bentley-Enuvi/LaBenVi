﻿namespace LaBenVi_CartAPI.Models.DTOs
{
    public class CartDetailsDto
    {
        public int CartDetailsId { get; set; }
        public int CartHeaderId { get; set; }
        public CartHeaderDto? CartHeader { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public ProductDto? Product { get; set; }
        public int Count { get; set; }
    }
}
