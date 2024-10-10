using System.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using YungChingProject.Services;
using YungChingProject.Repositories;
using YungChingProject.Models;
using NSubstitute;
using Castle.Core.Resource;


namespace YungChingProject.Tests.Services
{
    [TestFixture]
    public class CustomerServiceTests
    {
        private ICRUDRepository<Customer> _repository;
        private CustomerService _CustomerService;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<ICRUDRepository<Customer>>();
            _CustomerService = new CustomerService(_repository);
        }

        [Test]
        public async Task CreateUserAsync_WhenCalled_ReturnsEntity()
        {
            //Arrange
            Customer customer = new Customer() { CustomerId = "Jacky" };
            _repository.CreateAsync(Arg.Any<Customer>()).Returns(true);

            //Act
            Customer customerResult = await _CustomerService.CreateCustomerAsync(customer);

            //Assert
            Assert.That(customerResult, Is.Not.Null);
        }

        [Test]
        public async Task DeleteCustomerAsync_WhenCalled_ReturnsTrue()
        {
            //Arrange
            Customer customer = new Customer() { CustomerId = "Jacky" };
            _repository.FindByIdAsync(customer.CustomerId).Returns(customer);

            //Act
            bool isDeleted = await _CustomerService.DeleteCustomerAsync(customer.CustomerId);

            //Assert
            Assert.That(isDeleted, Is.True);
        }

        [Test]
        public async Task DeleteCustomerAsync_WhenCalled_ReturnsFalse()
        {
            //Arrange
            Customer customer = new Customer() { CustomerId = "Jacky" };
            _repository.FindByIdAsync(customer.CustomerId).Returns((Customer?)null);

            //Act
            bool isDeleted = await _CustomerService.DeleteCustomerAsync(customer.CustomerId);

            //Assert
            Assert.That(isDeleted, Is.False);
        }
    }
}
