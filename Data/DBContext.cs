using Gestamp.API.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestamp.API.Data
{
    public class DBContext: DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {

        }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
