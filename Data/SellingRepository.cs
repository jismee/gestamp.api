using Gestamp.API.Helpers;
using Gestamp.API.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestamp.API.Data
{
    public class SellingRepository: ISellingRepository
    {
        private readonly DBContext _context;
        public SellingRepository(DBContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }

        public async Task<Sale> GetSaleById(int id)
        {
            var result = await _context.Sales
                .AsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<Sale> GetSaleByIdForUpdateOrDelete(int id)
        {
            var result = await _context.Sales
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<PagedList<Sale>> GetSales(SalesParams saleParams)
        {
            var sales = _context.Sales
                .OrderByDescending(u => u.OrderDate)
                .AsQueryable();

            if (saleParams.Search != null)
            {
                sales = sales.Where(u => u.Region.Contains(saleParams.Search));
            }
            

            if (!string.IsNullOrEmpty(saleParams.OrderBy))
            {
                switch (saleParams.OrderBy)
                {
                    case "created":
                        sales = sales.OrderByDescending(u => u.OrderDate);
                        break;
                    case "shiped":
                        sales = sales.OrderByDescending(u => u.ShipDate);
                        break;
                    default:
                        sales = sales.OrderByDescending(u => u.OrderPriority);
                        break;

                }
            }

            return await PagedList<Sale>.CreateAsync(sales, saleParams.PageNumber, saleParams.PageSize);
        }

    }
}
