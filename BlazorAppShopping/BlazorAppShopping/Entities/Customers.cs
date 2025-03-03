namespace BlazorAppShopping.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using BlazorAppShopping.Data;
    using Microsoft.AspNetCore.Identity;

    public class Customer
    {
        public int CustomerId { get; set; }

        public string UserId { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; } = null!;


        public string FullName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }

}
