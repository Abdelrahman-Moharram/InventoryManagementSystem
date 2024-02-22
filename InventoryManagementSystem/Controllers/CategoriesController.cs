﻿using InventoryManagementSystem.Domain.Models;
using InventoryManagementSystem.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }




        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            return Ok(await _unitOfWork.Categories.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(string id)
        {
            return Ok(await _unitOfWork.Categories.GetByIdAsync(id));
        }
    }
}