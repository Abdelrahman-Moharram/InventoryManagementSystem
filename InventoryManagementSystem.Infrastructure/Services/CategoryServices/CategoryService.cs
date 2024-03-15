using AutoMapper;
using InventoryManagementSystem.Domain.DTOs;
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
        public async Task<IEnumerable<SimpleModule>> GetAsSelectList()
        {
            return _mapper.Map<IEnumerable<SimpleModule>>(
                    await _unitOfWork.Categories.GetAsSelectList(i=>new SimpleModule { Id = i.Id, Name=i.Name})
                );
        }



        public async Task<IEnumerable<GetCategoryDTO>> Search(string SearchQuery)
        {
            return _mapper.Map<IEnumerable<GetCategoryDTO>>(
                await _unitOfWork.Categories.FindAllAsync(
                        i=> i.Name.Contains(SearchQuery) || i.Id.Contains(SearchQuery)
                    ));

        }
        public async Task<BaseResponse> AddNew(Category newCategory, string CreatedBy)
        {
            if (string.IsNullOrEmpty(CreatedBy))
                return new BaseResponse { Message = "Can't assign Transacation To user, user id is empty", IsSucceeded = false };
            if (newCategory.Name == null)
                return new BaseResponse { Message = "Invalid Category Name" , IsSucceeded = false };

            else if (await _unitOfWork.Categories.Find(i=>i.Name == newCategory.Name) != null)
                return new BaseResponse { Message = $"Category with {newCategory.Name} Name Already Exisits", IsSucceeded = false };
            try
            {
                newCategory.CreatedBy = CreatedBy;
                await _unitOfWork.Categories.AddAsync(newCategory);
                await _unitOfWork.Save();
                return new BaseResponse { Message = $"Category {newCategory.Name} added Successfully", IsSucceeded = true };
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong while adding {newCategory.Name}", ex);
                return new BaseResponse { Message = $"Something went wrong while adding {newCategory.Name}", IsSucceeded = false };
            }

        }
        public async Task<BaseResponse> Update(Category updateCategory, string UpdatedBy)
        {
            if (string.IsNullOrEmpty(UpdatedBy))
                return new BaseResponse { Message = "Can't assign Transacation To user, user id is empty", IsSucceeded = false };
            if (updateCategory.Name == null)
                return new BaseResponse { Message = "Invalid Category Name", IsSucceeded = false };

            else if (await _unitOfWork.Categories.Find(i => i.Id == updateCategory.Id) == null)
                return new BaseResponse { Message = $"Category Doesn't Exisit", IsSucceeded = false };
            
            
            try
            {
                updateCategory.UpdatedBy = UpdatedBy;
                await _unitOfWork.Categories.UpdateAsync(updateCategory);
                await _unitOfWork.Save();
                return new BaseResponse { Message = $"Category {updateCategory.Name} Updated Successfully", IsSucceeded = true };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while updating {updateCategory.Name}", ex);
                return new BaseResponse { Message = $"Something went wrong while updating {updateCategory.Name}", IsSucceeded = false };
            }
        }
        public async Task<BaseResponse> Delete(string id, string DeletedBy)
        {
            if (string.IsNullOrEmpty(DeletedBy))
                return new BaseResponse { Message = "Can't assign Transacation To user, user id is empty", IsSucceeded = false };
            if (id == null)
                return new BaseResponse { Message = "Invalid Category id", IsSucceeded = false };

            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null)
                return new BaseResponse { Message = "this Category not found", IsSucceeded = false };
            try
            {
                category.DeletedBy = DeletedBy;
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
