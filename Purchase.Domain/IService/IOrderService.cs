﻿using Purchase.Domain.Models;
using Purchase.Domain.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.IService
{
    public interface IOrderService
    {
        IEnumerable<Order> GetPagedListAsync(PagingParameters pagingParameters, bool trackChanges);
    }
}
