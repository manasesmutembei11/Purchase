using Purchase.Domain.Contracts;
using Purchase.Domain.IService;
using Purchase.Domain.Models;
using Purchase.Domain.Paging;
using Purchase.Domain.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Purchase.Infrastructure.Services
{
    //internal sealed class CategoryService : ICategoryService
    //{
    //    private readonly IRepositoryManager _repository;
    //    private readonly ILoggerManager _logger;
    //    private readonly IMapper _mapper;
    //    public CategoryService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    //    {
    //        _repository = repository;
    //        _logger = logger;
    //        _mapper = mapper;

    //    }
    //    public IEnumerable<Category> GetPagedListAsync(PagingParameters pagingParameters, bool trackChanges)
    //    {
    //        try
    //        {
    //            var categories =
    //            _repository.Category.GetPagedListAsync(pagingParameters, trackChanges);
    //            return (IEnumerable<Category>) categories;
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError($"Something went wrong in the {nameof(GetPagedListAsync)} service method {ex}");
    //            throw;
    //        }
    //    }

    //    public CategoryDTO CreateCategory(CategoryDTO category)
    //    {
    //        var categoryEntity = _mapper.Map<Category>(category);
    //        _repository.Category.CreateCategory(categoryEntity);
    //        _repository.SaveAsync();
    //        var categoryToReturn = _mapper.Map<CategoryDTO>(categoryEntity);
    //        return categoryToReturn;
    //    }

    //    public CategoryDTO GetCategory(Guid id, bool trackChanges)
    //    {
    //        var category = _repository.Category.GetCategory(id, trackChanges);
             
    //        var categoryDto = _mapper.Map<CategoryDTO>(category);
    //        return categoryDto;
    //    }


    //    public void DeleteCategory(Guid categoryId, bool trackChanges)
    //    {
    //        var category = _repository.Category.GetCategory(categoryId, trackChanges);
    //        if (category is null)
                
    //        _repository.Category.DeleteCategory(category);
    //        _repository.SaveAsync();
    //    }


    //}
}
