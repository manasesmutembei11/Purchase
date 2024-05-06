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
using Microsoft.AspNetCore.Authorization;
using Purchase.Domain.Models;

namespace Purchase.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IServiceManager _service;

        public CategoryController(IRepositoryManager repository, IMapper mapper, IServiceManager service)
        {
            _repository = repository;
            _mapper = mapper;
            _service = service;
        }


        [HttpGet("pagedlist")]
        public async Task<IActionResult> GetPagedList([FromQuery] PagingParameters pagingParameters)
        {
            var paged = await _repository.Category.GetPagedListAsync(pagingParameters, true);
            var data = new PagedList<CategoryDTO>(
                paged.Data.Select(s => _mapper.Map<CategoryDTO>(s)).ToList(),
                paged.MetaData.TotalCount,
                paged.MetaData.CurrentPage,
                paged.MetaData.PageSize);

            return Ok(data);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetCategory(Guid id)
        {
            var category = _service.CategoryService.GetCategory(id, trackChanges: false);
            return Ok(category);
        }


        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryDTO category)
        {
            if (category is null)
                return BadRequest("CategoryDTO object is null");
            var createdCategory = _service.CategoryService.CreateCategory(category);
            return Ok(createdCategory);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteCategory(Guid id)
        {
            _service.CategoryService.DeleteCategory(id, trackChanges: false);
            return NoContent();
        }


    }
}
