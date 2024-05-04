using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Purchase.Domain.Contracts;
using AutoMapper;
using Purchase.Domain.Paging;
using Purchase.Presentation.DTOs;
using Purchase.Domain.Models;
namespace Purchase.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public CustomerController(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet("pagedlist")]
        public async Task<IActionResult> GetPagedList([FromQuery] PagingParameters pagingParameters)
        {
            var paged = await _repository.Customer.GetPagedListAsync(pagingParameters, true);
            var data = new PagedList<CustomerDTO>(
                paged.Data.Select(s => _mapper.Map<CustomerDTO>(s)).ToList(),
                paged.MetaData.TotalCount,
                paged.MetaData.CurrentPage,
                paged.MetaData.PageSize);

            return Ok(data);
        }
    
    }


        
}
