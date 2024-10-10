using YungChingProject.Data;
using YungChingProject.Repositories;
using YungChingProject.Services;
using Microsoft.EntityFrameworkCore;
using YungChingProject.Models;

namespace YungChingProject.Tests.Repositories
{
    [TestFixture]
    public class CustomerRepositoryTests : CRUDRepositoryTestsBase<Customer>
    {
        protected override void SeedData()
        {
            _dbset.Add(new Customer
            {
                CustomerId = "Jacky",
                CompanyName = "Jacky Company",
                ContactName = "Andy Wu",
                ContactTitle = "Develop Representative",
                Address = "Obere Str. 57",
                City = "Taipei",
                Area = null,
                PostalCode = "12209",
                Country = "Taiwan",
                Phone = "0900123456",
                Fax = "030-0076545",
                Orders = new List<Order>(), 
                CustomerTypes = new List<CustomerDemographic>()
            });
        }
    }
}