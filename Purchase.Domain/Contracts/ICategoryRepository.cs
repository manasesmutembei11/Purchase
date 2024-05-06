using Purchase.Domain.Models;
using Purchase.Domain.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Contracts
{
    public interface ICategoryRepository
    {
        Task<PagedList<Category>> GetPagedListAsync(PagingParameters pagingParameters, bool trackChanges);

        void CreateCategory(Category category);
        Category GetCategory(Guid categoryId, bool trackChanges);


        void DeleteCategory(Category category);


    }
}
