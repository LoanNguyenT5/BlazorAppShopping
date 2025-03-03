namespace BlazorAppShopping.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;

    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }


        [StringLength(1000)] 
        public string Description { get; set; } = string.Empty;


        [Required]
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }

        [JsonIgnore]
        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = null!;

        public string ImageURL { get; set; } = string.Empty;

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }

}
