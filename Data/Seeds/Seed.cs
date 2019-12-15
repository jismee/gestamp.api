using Gestamp.API.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Gestamp.API.Data.Seed
{
    public class Seed
    {
        private readonly DBContext _context;
        public Seed(DBContext context)
        {
            _context = context;
        }

        public void SeedFiles()
        {
            if (!_context.Sales.Any())
            {
                var path = "Data/Seeds/1500000 Sales Records.csv";
                var fileData = System.IO.File.ReadAllText(path);
                string[] lines = fileData.Split(Environment.NewLine).Skip(1).ToArray();
                foreach (string line in lines)
                {
                    List<string> fields = line.Split(',').ToList<string>();
                    // se salta las lineas en blanco
                    if (fields[0] == "")
                    {
                        continue;
                    }

                    int numFiels = fields.Count;

                    // Se puede reutilizar el objeto sale
                    Sale sale = new Sale();

                    for (int field = 0; field < numFiels; field++)
                    {
                        // Se puede crear un objeto dynamic sale
                        sale.Region = fields[field++];
                        sale.Country = fields[field++];
                        sale.ItemType = fields[field++];
                        sale.SaleChannel = fields[field++];
                        sale.OrderPriority = fields[field++];
                        sale.OrderDate = DateTime.ParseExact(fields[field++], @"M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        sale.OrderId = long.Parse(fields[field++],
                                        System.Globalization.NumberFormatInfo.InvariantInfo);
                        sale.ShipDate = DateTime.ParseExact(fields[field++], @"M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        sale.UnitsSold = int.Parse(fields[field++],
                                        System.Globalization.NumberFormatInfo.InvariantInfo);
                        sale.UnitPrice = double.Parse(fields[field++],
                                        System.Globalization.NumberFormatInfo.InvariantInfo);
                        sale.UnitCost = double.Parse(fields[field++],
                                        System.Globalization.NumberFormatInfo.InvariantInfo);
                        sale.TotalRevenue = double.Parse(fields[field++],
                                        System.Globalization.NumberFormatInfo.InvariantInfo);
                        sale.TotalCost = double.Parse(fields[field++],
                                        System.Globalization.NumberFormatInfo.InvariantInfo);
                        sale.TotalProfit = double.Parse(fields[field++],
                                        System.Globalization.NumberFormatInfo.InvariantInfo);

                        _context.Add(sale);
                    }
                }
                _context.SaveChanges();
            }
        }
    }
}
