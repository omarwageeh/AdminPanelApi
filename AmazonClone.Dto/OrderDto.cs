using AmazonClone.Model;
using AmazonClone.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonClone.Dto
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid CustomerId { get; set; }
        public Status Status { get; set; }
        public CustomerDto? Customer { get; set; }
        public List<OrderDetailsDto> OrderDetails { get; set; } = null!;
    }
}
