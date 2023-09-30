using AmazonClone.Model;
using AmazonClone.Repository.Interface;

namespace AmazonClone.Service
{
    public class OrderSerivce
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderSerivce(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;            
        }
        public async Task<Order> AddOrder(Order order)
        {
            var entity = await _unitOfWork.OrderRepository.Add(order);
            await _unitOfWork.SaveChangesAsync();
            return entity;
        }
        public async Task<Order?> GetOrder(Guid id, bool includeCustomer = false)
        {
            if(includeCustomer) 
            {   
                var order = await _unitOfWork.OrderRepository.GetAllWithInclude(o => o.Id == id, "Customer");
                return order.FirstOrDefault();
            }
            else
            {
                var order = await _unitOfWork.OrderRepository.Get(o => o.Id == id);
                return order;
            }
            
        }
        public async Task<Order?> GetAllOrderByCustomerId(string customerId)
        {
            var order = await _unitOfWork.OrderRepository.GetAllWithInclude(o => o.Customer.Id == Guid.Parse(customerId), "Customer");
            return order.FirstOrDefault();

        }
        public async Task UpdateOrder(Order order)
        {
            _unitOfWork.OrderRepository.Update(order);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task DeleteOrder(Guid id)
        {
            var order = await _unitOfWork.OrderRepository.Get(o => o.Id == id);
            if (order == null) return;
            _unitOfWork.OrderRepository.Delete(order);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<OrderDetails> AddOrderDetails(OrderDetails orderDetails)
        {
            var entity = await _unitOfWork.OrderDetailsRepository.Add(orderDetails);
            await _unitOfWork.SaveChangesAsync();
            return entity;
        }
        public async Task<OrderDetails?> GetOrderDetails(Guid orderId, Guid productId)
        {
            var orderDetails = await _unitOfWork.OrderDetailsRepository.GetAllWithInclude(od => od.OrderId == orderId && od.ProductId == productId, "Product");
            return orderDetails.FirstOrDefault();
        }
        public async Task<IEnumerable<OrderDetails?>> GetAllOrderDetails(Guid orderId, bool includeProduct = false)
        {
            if (includeProduct)
            {
                var orderDetials = await _unitOfWork.OrderDetailsRepository.GetAllWithInclude(od => od.OrderId == orderId, "Product");
                return orderDetials;
            }
            else
            {
                var orderDetails = await _unitOfWork.OrderDetailsRepository.GetAll(od => od.OrderId == orderId);
                return orderDetails;
            }
            
        }
        public async Task DeleteOrderDetails(Guid orderId, Guid productId)
        {
            var orderDetails = await GetOrderDetails(orderId, productId);
            if(orderDetails == null) 
            {
                return;
            }
            _unitOfWork.OrderDetailsRepository.Delete(orderDetails);
            await _unitOfWork.SaveChangesAsync();
        }
        public async void UpdateOrderDetails(OrderDetails orderDetails)
        {
            _unitOfWork.OrderDetailsRepository.Update(orderDetails);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
