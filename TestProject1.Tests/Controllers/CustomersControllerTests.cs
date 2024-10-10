using NSubstitute;
using YungChingProject.Controllers;
using YungChingProject.Services;
using YungChingProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace YungChingProject.Tests.Controllers
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class CustomersControllerTests
    {
        private ICustomerService _customerService;
        private CustomersController _controller;

        [SetUp]
        public void SetUp()
        {
            _customerService = Substitute.For<ICustomerService>();

            _controller = new CustomersController(_customerService);
        }

        [Test]
        public async Task PostCustomer_WhenCalled_Returns201()
        {
            //Arrange
            Customer customer = new Customer() { CustomerId = "Jacky" };
            _customerService.CreateCustomerAsync(customer).Returns(customer);

            //Act
            var actionResult = await _controller.PostCustomer(customer);

            //Assert
            Assert.That(actionResult?.Result, Is.InstanceOf<CreatedAtActionResult>());
            var createdAtActionResult = actionResult?.Result as CreatedAtActionResult;
            Assert.That(createdAtActionResult?.StatusCode, Is.EqualTo((int)HttpStatusCode.Created));

            Customer? customer1 = createdAtActionResult.Value as Customer;
            Assert.IsNotNull(customer1);
            Assert.That(customer.CustomerId, Is.EqualTo(customer1?.CustomerId));
        }

        [Test]
        public async Task PostCustomer_WhenCalled_Returns409()
        {
            //Arrange
            Customer customer = new Customer() { CustomerId = "Jacky" };
            _customerService.CreateCustomerAsync(customer).Returns(Task.FromException<Customer>(new DbUpdateException()));
            _customerService.GetCustomerByIdAsync(customer.CustomerId).Returns(customer);
            //Act
            var actionResult = await _controller.PostCustomer(customer);

            //Assert
            Assert.That(actionResult?.Result, Is.InstanceOf<ConflictResult>());
        }
    }
}
