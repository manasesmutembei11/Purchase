using Purchase.Domain.Models;
using Purchase.Domain.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Contracts
{
    public interface IProductRepository
    {
        Task<PagedList<Product>> GetPagedListAsync(PagingParameters pagingParameters, bool trackChanges);

        void CreateProduct(Product product);

        Product GetProduct(Guid id, bool trackChanges);
        void DeleteProduct(Product product);

    }
}
