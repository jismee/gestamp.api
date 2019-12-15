using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestamp.API.Dtos
{
    public class SaleForListDto
    {
        public int Id { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string SaleChannel { get; set; }
        public string OrderPriority { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShipDate { get; set; }
        public double TotalRevenue { get; set; }
        public double TotalCost { get; set; }
        public double TotalProfit { get; set; }
    }
}
