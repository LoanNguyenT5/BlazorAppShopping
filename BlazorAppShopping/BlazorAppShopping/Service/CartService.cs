// CartService.cs
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorAppShopping.Entities;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

public class CartService
{
    private const string CartKey = "CartItems";
    private readonly ProtectedSessionStorage _sessionStorage;

    public CartService(ProtectedSessionStorage sessionStorage)
    {
        _sessionStorage = sessionStorage;
    }

    public async Task<List<CartItem>> GetCartItemsAsync()
    {
        var result = await _sessionStorage.GetAsync<List<CartItem>>(CartKey);
        return result.Success ? result.Value ?? new List<CartItem>() : new List<CartItem>();
    }

    public async Task AddToCartAsync(Product product, int quantity = 1)
    {

        var cartItems = await GetCartItemsAsync();

        if (cartItems == null)
        {
            cartItems = new List<CartItem>();
        }

      
        var existingItem = cartItems.FirstOrDefault(item => item.Product?.ProductId == product.ProductId);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity; 
        }
        else
        {

            cartItems.Add(new CartItem { Product = product, Quantity = quantity });
        }


        await _sessionStorage.SetAsync(CartKey, cartItems);
    }

    public async Task RemoveFromCartAsync(int productId)
    {
        var cartItems = await GetCartItemsAsync();
        cartItems.RemoveAll(item => item.Product.ProductId == productId);
        await _sessionStorage.SetAsync(CartKey, cartItems);
    }

    public async Task ClearCartAsync()
    {
        await _sessionStorage.DeleteAsync(CartKey); 
    }
}
public class CartItem
{
    public Product Product { get; set; }
    public int Quantity { get; set; }
}