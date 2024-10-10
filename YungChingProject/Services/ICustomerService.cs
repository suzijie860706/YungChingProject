using YungChingProject.Models;

namespace YungChingProject.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(string customerId);
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<Customer?> UpdateCustomerAsync(string customerId, Customer customer);
        Task<bool> DeleteCustomerAsync(string customerId);
    }
}
