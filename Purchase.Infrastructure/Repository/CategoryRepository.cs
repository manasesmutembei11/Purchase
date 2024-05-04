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
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public Task<PagedList<Category>> GetPagedListAsync(PagingParameters pagingParameters, bool trackChanges)
        {
            var data = FindAll(trackChanges).OrderBy(e => e.Code);
            return PagedList<Category>.ToPagedListAsync(data, pagingParameters.PageNumber, pagingParameters.PageSize);
        }
    }
}
