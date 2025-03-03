using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlazorAppShopping.Data;
using BlazorAppShopping.Entities;

public class Payment
{
    [Key]
    public int PaymentId { get; set; }

    public int OrderId { get; set; }

    [ForeignKey("OrderId")]
    public Order Order { get; set; } = null!;

    public string UserId { get; set; } = string.Empty;

    [ForeignKey("UserId")]
    public ApplicationUser User { get; set; } = null!;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; }

    [Required, StringLength(50)]
    public string Status { get; set; } = "Pending";

    public DateTime PaymentDate { get; set; } = DateTime.Now;


    public string? PaymentMethod { get; set; } 
    public string? TransactionId { get; set; } 
}