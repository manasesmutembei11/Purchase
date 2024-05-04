using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Purchase.Domain.Contracts;
using Purchase.Domain.Paging;
using Purchase.Presentation.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public OrderItemController(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet("pagedlist")]
        public async Task<IActionResult> GetPagedList([FromQuery] PagingParameters pagingParameters)
        {
            var paged = await _repository.OrderItem.GetPagedListAsync(pagingParameters, true);
            var data = new PagedList<OrderItemDTO>(
                paged.Data.Select(s => _mapper.Map<OrderItemDTO>(s)).ToList(),
                paged.MetaData.TotalCount,
                paged.MetaData.CurrentPage,
                paged.MetaData.PageSize);

            return Ok(data);
        }
    }
}
