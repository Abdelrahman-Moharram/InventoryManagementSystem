using InventoryManagementSystem.Domain.Models;
using InventoryManagementSystem.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IBaseRepository<Category> _categoryRepository;

        public CategoriesController(IBaseRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            return Ok(await _categoryRepository.GetAll());
        }
    }
}
