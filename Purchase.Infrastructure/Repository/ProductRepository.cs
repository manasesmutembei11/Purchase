using LinqKit;
using Purchase.Domain.Contracts;
using Purchase.Domain.Models;
using Purchase.Domain.Paging;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Purchase.Infrastructure.Repository
{
    public class ProductRepository : RepositoryBase<Product, Guid>, IProductRepository
    {
        public ProductRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public Task<PagedList<Product>> GetPagedListAsync(PagingParameters pagingParameters, bool trackChanges)
        {
            var predicate = PredicateBuilder.New<Product>(true);
            if (!string.IsNullOrWhiteSpace(pagingParameters.Search))
            {
                predicate = predicate.And(s => s.Name.Contains(pagingParameters.Search) || s.Code.Contains(pagingParameters.Search));
            }
            var data = FindByCondition(predicate, trackChanges).OrderBy(s => s.Name);
            return PagedList<Product>.ToPagedListAsync(data, pagingParameters.PageNumber, pagingParameters.PageSize);
        }


        public void DeleteTax(Product product) => Delete(product);

    }
}
