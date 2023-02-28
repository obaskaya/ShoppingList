using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.CategoryApplication.Command.CreateCommand;
using ShoppingList.Application.CategoryApplication.Command.DeleteCommand;
using ShoppingList.Application.CategoryApplication.Command.UpdateCommand;
using ShoppingList.Application.CategoryApplication.Query;
using ShoppingList.Application.CategoryApplication.Query.GetByCreateDate;
using ShoppingList.Application.CategoryApplication.Query.GetById;
using ShoppingList.Application.CategoryApplication.Query.GetByPublishDate;

using ShoppingList.DbOperations;
using System.Data;

namespace ShoppingList.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly IShoppingListDbContext _context;
        private readonly IMapper _mapper;
        public CategoryController(IShoppingListDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: All Categories
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            GetCategoryQuery query = new(_context, _mapper);
            var result = await query.Handle();

            return Ok(result);
        }

        // GET Category Detail
        [HttpGet("{GetById}")]
        public async Task<IActionResult> GetById(int GetById)
        {
            GetCategoryByIdQuery query = new(_context, _mapper);

            query.CategoryId = GetById;
            GetCategoryByIdQueryValidator validator = new GetCategoryByIdQueryValidator();

            validator.ValidateAndThrow(query);
            var result = await query.Handle();
            return Ok(result);
        }
        // GET By create date
        [HttpGet("CreateDate")]
        public async Task<IActionResult> GetByCreateDate(string date)
        {
            GetByCreateDateQuery query = new(_context, _mapper);

            query.CreateDate = date;
            GetByCreateDateQueryValidator validator = new GetByCreateDateQueryValidator();

            validator.ValidateAndThrow(query);
            var result = await query.Handle();
            return Ok(result);
        }

        // GET By Finish date
        [HttpGet("FinishDate")]
        public async Task<IActionResult> GetByFinishDate(string date)
        {
            GetByFinishDateQuery query = new(_context, _mapper);

            query.FinDate = date;
            GetByFinishDateQueryValidator validator = new GetByFinishDateQueryValidator();

            validator.ValidateAndThrow(query);
            var result = await query.Handle();
            return Ok(result);
        }

        // Create Category
        [HttpPost("CreateCategory")]
        public async Task<IActionResult> Post([FromBody] CreateCategoryModel newCategory)
        {
            CreateCategoryCommand command = new(_context, _mapper);
            command.Model = newCategory;

            CreateCategoryCommandValidator validator = new CreateCategoryCommandValidator();
            validator.ValidateAndThrow(command);
            await command.Handle();

            return Ok();
        }

        // Update Category
        [HttpPut("{UpdateCategory}")]
        public async Task<IActionResult> Put(int UpdateCategory, [FromBody] UpdateCategoryModel updateCategory)
        {
            UpdateCategoryCommand update = new(_context);

            update.Model = updateCategory;
            update.CategoryId = UpdateCategory;

            UpdateCategoryCommandValidator validator = new UpdateCategoryCommandValidator();
            validator.ValidateAndThrow(update);
            await update.Handle();

            return Ok();
        }

        // DELETE Category
        [HttpDelete("{DeleteCategory}")]
        public async Task<IActionResult> Delete(int DeleteCategory)
        {
            DeleteCategoryCommand command = new(_context);
            command.CategoryId = DeleteCategory;

            DeleteCategoryCommandValidator validator = new DeleteCategoryCommandValidator();
            validator.ValidateAndThrow(command);
            await command.Handle();

            return Ok();
        }
    }
}
