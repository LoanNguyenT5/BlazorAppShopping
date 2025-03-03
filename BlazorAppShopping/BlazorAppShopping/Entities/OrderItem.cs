namespace BlazorAppShopping.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class OrderItem
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; } = null!;

        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
    }

}
