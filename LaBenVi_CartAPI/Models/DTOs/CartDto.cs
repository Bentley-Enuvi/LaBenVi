namespace LaBenVi_CartAPI.Models.DTOs
{
    public class CartDto
    {
        public CartHeaderDto CartHeader { get; set; }
        public string UserId { get; set; }
        public IEnumerable<CartDetailsDto>? CartDetails { get; set; }

    }
}
