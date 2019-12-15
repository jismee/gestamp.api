using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestamp.API.Dtos
{
    public class SaleForUpdateDto
    {
        public string OrderPriority { get; set; }
        public DateTime ShipDate { get; set; }
        public int UnitsSold { get; set; }
        public double UnitPrice { get; set; }
        public double UnitCost { get; set; }
        public double TotalRevenue { get; set; }
        public double TotalCost { get; set; }
        public double TotalProfit { get; set; }
    }
}
