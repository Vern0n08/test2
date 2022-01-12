using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfService.DAL.Repositories;

namespace WcfService.DAL
{
    public class UnitOfWork : IDisposable
    {
        private StoreDbContext _dbContext;
        public IProductRepository Products { get; set; }

        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
            Products = new ProductRepository(dbContext);
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
