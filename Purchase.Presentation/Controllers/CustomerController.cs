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
using Purchase.Domain.DTOs;
using Purchase.Domain.Models;
using Purchase.Domain.IService;
namespace Purchase.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IServiceManager _service;

        public CustomerController(IRepositoryManager repository, IMapper mapper, IServiceManager service)
        {
            _repository = repository;
            _mapper = mapper;
            _service = service;
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

        [HttpPost]
        public IActionResult CreateCustomer([FromBody] CustomerDTO customer)
        {
            if (customer is null)
                return BadRequest("CustomerDTO object is null");
            var createdCustomer = _service.CustomerService.CreateCustomer(customer);
            return Ok(createdCustomer);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetCustomer(Guid id)
        {
            var customer = _service.CustomerService.GetCustomer(id, trackChanges: false);
            return Ok(customer);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteCustomer(Guid id)
        {
            _service.CustomerService.DeleteCustomer(id, trackChanges: false);
            return NoContent();
        }

    }


        
}
