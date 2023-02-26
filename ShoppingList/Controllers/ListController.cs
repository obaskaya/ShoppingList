using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.ListApplication.Command.CreateCommand;
using ShoppingList.Application.ListApplication.Command.DeleteCommand;
using ShoppingList.Application.ListApplication.Command.UpdateCommand;
using ShoppingList.Application.ListApplication.Query;
using ShoppingList.Application.ListApplication.Query.GetListById;
using ShoppingList.Application.ListApplication.Query.GetListByName;
using ShoppingList.DbOperations;

namespace ShoppingList.Controllers
{
    [Route("api/[controller]")]
    public class ListController : Controller
    {
        private readonly IShoppingListDbContext _context;
        private readonly IMapper _mapper;
        public ListController(IShoppingListDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: All Lists
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            GetListQuery query = new(_context, _mapper);
            var result = await query.Handle();

            return Ok(result);
        }

        // GET Lists Detail
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            GetListByIdQuery query = new(_context, _mapper);

            query.ListId = id;
            GetListByIdQueryValidator validator = new GetListByIdQueryValidator();

            validator.ValidateAndThrow(query);
            var result = await query.Handle();
            return Ok(result);
        }

        // Get List By Name
        [HttpGet("search")]
        public async Task<IActionResult> Get(string name)
        {
            GetListByNameQuery query = new(_context, _mapper);

            query.ListName = name;
            GetListByNameQueryValidator validator = new GetListByNameQueryValidator();

            validator.ValidateAndThrow(query);
            var result = await query.Handle();
            return Ok(result);
        }


        // POST Lists
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateListModel newList)
        {
            CreateListCommand command = new(_context, _mapper);
            command.Model = newList;

            CreateListCommandValidator validator = new CreateListCommandValidator();
            validator.ValidateAndThrow(command);
            await command.Handle();

            return Ok();
        }

        // PUT Lists
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateListModel updateList)
        {
            UpdateListCommand update = new(_context);

            update.Model = updateList;
            update.ListId = id;

            UpdateListCommandValidator validator = new UpdateListCommandValidator();
            validator.ValidateAndThrow(update);
            await update.Handle();

            return Ok();
        }

        // DELETE Lists
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteListCommand command = new(_context);
            command.ListId = id;

            DeleteListCommandValidator validator = new DeleteListCommandValidator();
            validator.ValidateAndThrow(command);
            await command.Handle();

            return Ok();
        }
    }
}
