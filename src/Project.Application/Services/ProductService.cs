using Project.Application.Interfaces;
using Project.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Services
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(IUnitOfWork uow) : base(uow)
        {
        }
    }
}
