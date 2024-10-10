using YungChingProject.Models;

namespace YungChingProject.Services
{
    public interface ICustomerService
    {
        /// <summary>
        /// 取得所有客戶資料
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        /// <summary>
        /// 取得所有客戶資料
        /// </summary>
        /// <param name="customerId">客戶名稱</param>
        /// <returns></returns>
        Task<Customer?> GetCustomerByIdAsync(string customerId);
        /// <summary>
        /// 新增客戶資料
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<Customer> CreateCustomerAsync(Customer customer);
        /// <summary>
        /// 更新客戶資料
        /// </summary>
        /// <param name="customerId">客戶名稱</param>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<Customer?> UpdateCustomerAsync(string customerId, Customer customer);
        /// <summary>
        /// 刪除客戶資料
        /// </summary>
        /// <param name="customerId">客戶名稱</param>
        /// <returns></returns>
        Task<bool> DeleteCustomerAsync(string customerId);
    }
}
