using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Purchase.Domain.Models;
using Purchase.Domain.Paging;

namespace Purchase.Domain.IService
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetPagedListAsync(PagingParameters pagingParameters, bool trackChanges);

    }

}
