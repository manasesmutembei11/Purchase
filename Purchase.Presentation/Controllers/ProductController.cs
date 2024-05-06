using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Purchase.Domain.Contracts;
using Purchase.Domain.Paging;
using Purchase.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Purchase.Domain.IService;

namespace Purchase.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IServiceManager _service;

        public ProductController(IRepositoryManager repository, IMapper mapper, IServiceManager service)
        {
            _repository = repository;
            _mapper = mapper;
            _service = service;
        }


        [HttpGet("pagedlist")]
        public async Task<IActionResult> GetPagedList([FromQuery] PagingParameters pagingParameters)
        {
            var paged = await _repository.Product.GetPagedListAsync(pagingParameters, true);
            var data = new PagedList<ProductDTO>(
                paged.Data.Select(s => _mapper.Map<ProductDTO>(s)).ToList(),
                paged.MetaData.TotalCount,
                paged.MetaData.CurrentPage,
                paged.MetaData.PageSize);

            return Ok(data);
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductDTO product)
        {
            if (product is null)
                return BadRequest("OrderDTO object is null");
            var createdProduct = _service.ProductService.CreateProduct(product);
            return Ok(createdProduct);
        }
    }
}
