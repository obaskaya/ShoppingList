using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.CategoryApplication.Command.CreateCommand;
using ShoppingList.Application.CategoryApplication.Command.DeleteCommand;
using ShoppingList.Application.CategoryApplication.Command.UpdateCommand;
using ShoppingList.Application.CategoryApplication.Query;
using ShoppingList.Application.ItemApplication.Command.CreateCommand;
using ShoppingList.Application.ItemApplication.Command.DeleteCommand;
using ShoppingList.Application.ItemApplication.Command.UpdateCommand;
using ShoppingList.Application.ItemApplication.Query;
using ShoppingList.DbOperations;

namespace ShoppingList.Controllers
{

    [Route("api/[controller]")]
    public class ItemController : Controller
    {
        private readonly IShoppingListDbContext _context;
        private readonly IMapper _mapper;
        public ItemController(IShoppingListDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: All Item
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            GetItemQuery query = new(_context, _mapper);
            var result = await query.Handle();

            return Ok(result);
        }

        // GET Item Detail
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            GetItemByIdQuery query = new(_context, _mapper);

            query.ItemId = id;
            GetItemByIdQueryValidator validator = new GetItemByIdQueryValidator();

            validator.ValidateAndThrow(query);
            var result = await query.Handle();
            return Ok(result);
        }

        // Create Item
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateItemViewModel newItem)
        {
            CreateItemCommand command = new(_context, _mapper);
            command.Model = newItem;

            CreateItemCommandValidator validator = new CreateItemCommandValidator();
            validator.ValidateAndThrow(command);
            await command.Handle();

            return Ok();
        }

        // Update Item
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateItemViewModel updateItem)
        {
            UpdateItemCommand update = new(_context);

            update.Model = updateItem;
            update.ItemId = id;

            UpdateItemCommandValidator validator = new UpdateItemCommandValidator();
            validator.ValidateAndThrow(update);
            await update.Handle();

            return Ok();
        }

        // DELETE Item
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteItemCommand command = new(_context);
            command.ItemId = id;

            DeleteItemCommandValidator validator = new DeleteItemCommandValidator();
            validator.ValidateAndThrow(command);
            await command.Handle();

            return Ok();
        }
    }

}
