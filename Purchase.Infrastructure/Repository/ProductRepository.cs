using Purchase.Domain.Contracts;
using Purchase.Domain.Models;
using Purchase.Domain.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Infrastructure.Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public Task<PagedList<Product>> GetPagedListAsync(PagingParameters pagingParameters, bool trackChanges)
        {
            var data = FindAll(trackChanges).OrderBy(e => e.Name);
            return PagedList<Product>.ToPagedListAsync(data, pagingParameters.PageNumber, pagingParameters.PageSize);
        }
    }
}
