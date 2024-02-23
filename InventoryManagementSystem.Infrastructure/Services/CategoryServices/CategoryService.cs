﻿using AutoMapper;
using InventoryManagementSystem.Domain.DTOs.Category;
using InventoryManagementSystem.Domain.DTOs.Response;
using InventoryManagementSystem.Domain.Models;
using InventoryManagementSystem.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;


namespace InventoryManagementSystem.Infrastructure.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CategoryService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<GetCategoryDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<GetCategoryDTO>>(await _unitOfWork.Categories.GetAllAsync());
        }
        public async Task<IEnumerable<GetCategoryDTO>> GetAllWithBaseIncludes()
        {
            return _mapper.Map<IEnumerable<GetCategoryDTO>>(await _unitOfWork.Categories.GetAllAsync(new[] { "Products" }));
        }
        public async Task<GetCategoryDTO> GetById(string id)
        {
            return _mapper.Map<GetCategoryDTO>(await _unitOfWork.Categories.GetByIdAsync(id));
        }
        public async Task<IEnumerable<GetCategoryDTO>> Search(string SearchQuery)
        {
            return _mapper.Map<IEnumerable<GetCategoryDTO>>(
                await _unitOfWork.Categories.FindAllAsync(
                        i=> i.Name.Contains(SearchQuery) || i.Id.Contains(SearchQuery)
                    ));

        }
        public async Task<BaseResponse> AddNew(AddCategoryDTO newCategoryDTO)
        {
            if (newCategoryDTO.Name == null)
                return new BaseResponse { Message = "Invalid Category Name" , IsSucceeded = false };

            else if (await _unitOfWork.Categories.Find(i=>i.Name == newCategoryDTO.Name) != null)
                return new BaseResponse { Message = $"Category with {newCategoryDTO.Name} Name Already Exisits", IsSucceeded = false };
            try
            {
                await _unitOfWork.Categories.AddAsync(_mapper.Map<Category>(newCategoryDTO));
                await _unitOfWork.Save();
                return new BaseResponse { Message = $"Category {newCategoryDTO.Name} added Successfully", IsSucceeded = true };
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong while adding {newCategoryDTO.Name}", ex);
                return new BaseResponse { Message = $"Something went wrong while adding {newCategoryDTO.Name}", IsSucceeded = false };
            }

        }
        public async Task<BaseResponse> Update(UpdateCategoryDTO updateCategoryDTO)
        {
            if (updateCategoryDTO.Name == null)
                return new BaseResponse { Message = "Invalid Category Name", IsSucceeded = false };

            else if (await _unitOfWork.Categories.Find(i => i.Id == updateCategoryDTO.Id) == null)
                return new BaseResponse { Message = $"Category Doesn't Exisit", IsSucceeded = false };
            
            await _unitOfWork.Categories.UpdateAsync(_mapper.Map<Category>(updateCategoryDTO));
            await _unitOfWork.Save();
            return new BaseResponse { Message = $"Category {updateCategoryDTO.Name} Updated Successfully", IsSucceeded = true };
            
            try
            {
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while updating {updateCategoryDTO.Name}", ex);
                return new BaseResponse { Message = $"Something went wrong while updating {updateCategoryDTO.Name}", IsSucceeded = false };
            }
        }
        public async Task<BaseResponse> Delete(string id)
        {
            if (id == null)
                return new BaseResponse { Message = "Invalid Category id", IsSucceeded = false };

            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null)
                return new BaseResponse { Message = "this Category not found", IsSucceeded = false };
            try
            {
                await _unitOfWork.Categories.DeleteAsync(category);
                await _unitOfWork.Save();
                return new BaseResponse { Message = $"Category {category.Name} Deleted Successfully", IsSucceeded = true };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while Deleting {category.Name}", ex);
                return new BaseResponse { Message = $"Something went wrong while Deleting {category.Name}", IsSucceeded = false };
            }
        }

    }
}