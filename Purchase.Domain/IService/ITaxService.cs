using Purchase.Domain.DTOs;
using Purchase.Domain.Models;
using Purchase.Domain.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.IService
{
    public interface ITaxService
    {
        IEnumerable<Tax> GetPagedListAsync(PagingParameters pagingParameters, bool trackChanges);
        TaxDTO CreateTax(TaxDTO tax);
    }
}
