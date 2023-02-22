using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.CategoryApplication.Command.CreateCommand;
using ShoppingList.Application.CategoryApplication.Command.DeleteCommand;
using ShoppingList.Application.CategoryApplication.Command.UpdateCommand;
using ShoppingList.Application.CategoryApplication.Query;
using ShoppingList.Application.ListApplication.Command.CreateCommand;
using ShoppingList.Application.ListApplication.Command.DeleteCommand;
using ShoppingList.Application.ListApplication.Command.UpdateCommand;
using ShoppingList.Application.ListApplication.Query;
using ShoppingList.DbOperations;

namespace ShoppingList.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ShoppingListDbContext _context;
        private readonly IMapper _mapper;
        public CategoryController(ShoppingListDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: All Categories
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            GetCategoryQuery query = new(_context, _mapper);
            var result = await query.Handle();

            return Ok(result);
        }

        // GET Category Detail
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            GetCategoryByIdQuery query = new(_context, _mapper);

            query.CategoryId = id;
            GetCategoryByIdQueryValidator validator = new GetCategoryByIdQueryValidator();

            validator.ValidateAndThrow(query);
            var result = await query.Handle();
            return Ok(result);
        }

        // Create Category
        [HttpPost]
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
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateCategoryModel updateCategory)
        {
            UpdateCategoryCommand update = new(_context);

            update.Model = updateCategory;
            update.CategoryId = id;

            UpdateCategoryCommandValidator validator = new UpdateCategoryCommandValidator();
            validator.ValidateAndThrow(update);
            await update.Handle();

            return Ok();
        }

        // DELETE Category
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteCategoryCommand command = new(_context);
            command.CategoryId = id;

            DeleteCategoryCommandValidator validator = new DeleteCategoryCommandValidator();
            validator.ValidateAndThrow(command);
            await command.Handle();

            return Ok();
        }
    }
}
