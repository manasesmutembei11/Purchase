﻿using Purchase.Domain.Models;
using Purchase.Domain.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Contracts
{
    public interface ICustomerRepository : IRepositoryBase<Customer, Guid>
    {

        Task<PagedList<Customer>> GetPagedListAsync(PagingParameters pagingParameters, bool trackChanges);

       

    }
}
