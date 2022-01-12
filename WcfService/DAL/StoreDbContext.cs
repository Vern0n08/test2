using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WcfService.DAL
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext() : base("Data Source=localhost;Initial Catalog=storedb;Integrated Security=true")
        {

        }
        public virtual DbSet<Product> Products { get; set; }

    }
}