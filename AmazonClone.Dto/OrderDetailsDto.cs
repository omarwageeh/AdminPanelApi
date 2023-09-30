using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonClone.Dto
{
    public class OrderDetailsDto
    {
        public ProductDto Product { get; set; }
        public int ProductCount { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
