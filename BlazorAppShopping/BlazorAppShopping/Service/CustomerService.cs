
using BlazorAppShopping.Data;
using BlazorAppShopping.Entities;
using Microsoft.EntityFrameworkCore;


public class CustomerService
{
    private readonly ApplicationDbContext _dbContext;

    public CustomerService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Customer?> GetCustomerByUserIdAsync(string userId)
    {
        return await _dbContext.Customers.FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public async Task AddCustomerAsync(Customer customer)
    {
        _dbContext.Customers.Add(customer);
        await _dbContext.SaveChangesAsync();
    }


    public async Task UpdateCustomerAsync(Customer customer)
    {
        _dbContext.Customers.Update(customer);
        await _dbContext.SaveChangesAsync();
    }


    public async Task DeleteCustomerAsync(int customerId)
    {
        var customer = await _dbContext.Customers.FindAsync(customerId);
        if (customer != null)
        {
            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        return await _dbContext.Customers.ToListAsync();
    }
}