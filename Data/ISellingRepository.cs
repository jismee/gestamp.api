using Gestamp.API.Helpers;
using Gestamp.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestamp.API.Data
{
    public interface ISellingRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<User> GetUser(int id);

        Task<Sale> GetSaleById(int id);

        Task<Sale> GetSaleByIdForUpdateOrDelete(int id);

        Task<PagedList<Sale>> GetSales(SalesParams saleParams);

    }
}
