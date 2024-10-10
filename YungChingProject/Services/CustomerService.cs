using Microsoft.EntityFrameworkCore;
using YungChingProject.Data;
using YungChingProject.Models;
using YungChingProject.Repositories;

namespace YungChingProject.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICRUDRepository<Customer> _repository;

        public CustomerService(ICRUDRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            // Use repository to get all customers without any condition
            return await _repository.FindAsync(_ => true);
        }

        public async Task<Customer?> GetCustomerByIdAsync(string customerId)
        {
            // Use repository to find customer by ID
            return await _repository.FindByIdAsync(customerId);
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            // Use repository to create a new customer
            var isCreated = await _repository.CreateAsync(customer);
            if (isCreated)
            {
                return customer;
            }
            throw new Exception("Failed to create customer.");
        }

        public async Task<Customer?> UpdateCustomerAsync(string customerId, Customer customer)
        {
            // Use repository to find the customer first
            var existingCustomer = await _repository.FindByIdAsync(customerId);
            if (existingCustomer == null)
            {
                return null;
            }

            // Update the existing customer details
            existingCustomer.CompanyName = customer.CompanyName;
            existingCustomer.ContactName = customer.ContactName;
            existingCustomer.ContactTitle = customer.ContactTitle;
            existingCustomer.Address = customer.Address;
            existingCustomer.City = customer.City;
            existingCustomer.Area = customer.Area;
            existingCustomer.PostalCode = customer.PostalCode;
            existingCustomer.Country = customer.Country;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.Fax = customer.Fax;


            // Use repository to update the customer
            await _repository.UpdateAsync(customer);

            return existingCustomer;
        }

        public async Task<bool> DeleteCustomerAsync(string customerId)
        {
            // Find the customer before deleting
            var customer = await _repository.FindByIdAsync(customerId);
            if (customer == null)
            {
                return false;
            }

            // Use repository to delete the customer
            await _repository.DeleteAsync(customer);
            return true;
        }

    }
}
