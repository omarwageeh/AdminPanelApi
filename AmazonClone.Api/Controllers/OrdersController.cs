using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmazonClone.Model;
using AmazonClone.Service;
using AmazonClone.Model.Enum;
using AmazonClone.Dto;
using Microsoft.AspNetCore.Authorization;

namespace AmazonClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Microsoft.AspNetCore.Cors.EnableCors()]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly OrderSerivce _orderService;

        public OrdersController(OrderSerivce orderSerivce)
        {
            _orderService = orderSerivce;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var orders = await _orderService.GetAllOrders();
            var result = new List<OrderDto>();
            foreach (var order in orders)
            {
                var orderDetails = await _orderService.GetAllOrderDetails(order.Id, true);
                var orderDetailsDto = new List<OrderDetailsDto>();
                foreach (var item in orderDetails)
                {
                    var product = item.Product;
                    orderDetailsDto.Add(new OrderDetailsDto
                    {
                        Product = new ProductDto
                        {
                            NameEn = product.NameEn,
                            NameAr = product.NameAr,
                            Id = product.Id,
                            CategoryId = product.CategoryId.ToString(),
                            StockQuantity = product.StockQuantity,
                            UnitPrice = product.UnitPrice
                        },
                        ProductCount = item.ProductCount,
                        UnitPrice = item.UnitPrice               

                    });
                }
                result.Add(new OrderDto { Id = order.Id, Customer = new CustomerDto { FullName = order.Customer.FullName }, CustomerId = order.CustomerId, Status = order.Status, TotalPrice = order.TotalPrice, OrderDetails = orderDetailsDto });
            }

            return Ok(result);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(Guid id)
        { 
            var order = await _orderService.GetOrder(id, true);

            if (order == null)
            {
                return NotFound();
            }
            var orderDetails = await _orderService.GetAllOrderDetails(order.Id, true);
            var orderDetailsDto = new List<OrderDetailsDto>();
            foreach (var item in orderDetails)
            {
                var product = item.Product;
                orderDetailsDto.Add(new OrderDetailsDto
                {
                    Product = new ProductDto
                    {
                        NameEn = product.NameEn,
                        NameAr = product.NameAr,
                        Id = product.Id,
                        CategoryId = product.CategoryId.ToString(),
                        StockQuantity = product.StockQuantity,
                        UnitPrice = product.UnitPrice
                    },
                    ProductCount = item.ProductCount,
                    UnitPrice = item.UnitPrice
                });
            }
            var orderDto = new OrderDto { Id = order.Id, Customer = new CustomerDto { FullName = order.Customer.FullName }, CustomerId = order.CustomerId, Status = order.Status, TotalPrice = order.TotalPrice, OrderDetails = orderDetailsDto };
            return Ok(orderDto);
        }

        // POST: api/Orders/5
        [HttpPost("{id}")]
        public async Task<IActionResult> PutOrder(Guid id,Status status)
        {
            var order = await _orderService.GetOrder(id);
            if (order == null)
            {
                return NotFound();
            }
            order.Status = status;
            await _orderService.UpdateOrder(order);

            return NoContent();
        }

    }
}
