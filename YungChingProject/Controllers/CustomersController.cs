using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using YungChingProject.Data;
using YungChingProject.Models;
using YungChingProject.Services;

namespace YungChingProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// 查詢所有客戶
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Customer>), 200)]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return Ok(await _customerService.GetAllCustomersAsync());
        }

        /// <summary>
        /// 查詢指定客戶資料
        /// </summary>
        /// <param name="customerId">客戶名稱</param>
        /// <returns></returns>
        [HttpGet("{customerId}")]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<Customer?>> GetCustomer(string customerId)
        {
            var customer = await _customerService.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        /// <summary>
        /// 更新客戶資料
        /// </summary>
        /// <param name="customerId">客戶名稱</param>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPut("{customerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutCustomer(string customerId, Customer customer)
        {
            if (customerId != customer.CustomerId)
            {
                return BadRequest();
            }

            try
            {
                await _customerService.UpdateCustomerAsync(customerId, customer);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CustomerExists(customer.CustomerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// 新增客戶資料
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Customer), 201)]
        [ProducesResponseType(409)]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            try
            {
                Customer createdCustomer = await _customerService.CreateCustomerAsync(customer);
                return CreatedAtAction(nameof(GetCustomer), new { customerId = customer.CustomerId }, customer);
            }
            catch (DbUpdateException)
            {
                if (await CustomerExists(customer.CustomerId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="customerId">客戶名稱</param>
        /// <returns></returns>
        [HttpDelete("{customerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteCustomer(string customerId)
        {
            bool isDeleted = await _customerService.DeleteCustomerAsync(customerId);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// 查詢客戶是否存在
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        private async Task<bool> CustomerExists(string customerId)
        {
            Customer? customer = await _customerService.GetCustomerByIdAsync(customerId);
            if (customer == null) return false;
            else return true;
        }
    }
}
