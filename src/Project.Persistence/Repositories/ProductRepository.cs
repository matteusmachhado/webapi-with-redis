using Microsoft.Extensions.Caching.Distributed;
using Project.Domain.Entities;
using Project.Domain.Interfaces.Repositories;
using Project.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistence.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ProjectDbContext db, IDistributedCache cache) : base(db, cache)
        {
        }
    }
}
