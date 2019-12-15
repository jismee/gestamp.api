using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestamp.API.Model
{
    public class Sale
    {
        // Region,Country,Item Type, Sales Channel,Order Priority, 
        // Order Date,Order ID, Ship Date,Units Sold, Unit Price,Unit Cost, 
        // Total Revenue,Total Cost, Total Profit

        // Sub-Saharan Africa,South Africa,Fruits,Offline,M,7/27/2012,
        // 443368995,7/28/2012,1593,9.33,6.92,14862.69,11023.56,3839.13

        public int Id { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string ItemType { get; set; }
        public string SaleChannel { get; set; }
        public string OrderPriority { get; set; }
        public DateTime OrderDate { get; set; }
        public long OrderId { get; set; }
        public DateTime ShipDate { get; set; }
        public int UnitsSold { get; set; }
        public double UnitPrice { get; set; }
        public double UnitCost { get; set; }
        public double TotalRevenue { get; set; }
        public double TotalCost { get; set; }
        public double TotalProfit { get; set; }
    }
}
