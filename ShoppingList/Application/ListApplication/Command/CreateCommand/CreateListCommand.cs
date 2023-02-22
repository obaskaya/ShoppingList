using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Application.ListApplication.Query;
using ShoppingList.DbOperations;
using ShoppingList.Entities;

namespace ShoppingList.Application.ListApplication.Command.CreateCommand
{
    public class CreateListCommand
    {
        public CreateListModel Model { get; set; }

        //adding context and mapper
        private readonly ShoppingListDbContext _context;
        private readonly IMapper _mapper;
        public CreateListCommand(ShoppingListDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task Handle()
        {
            var list = _context.lists.FirstOrDefault(c => c.Name == Model.Name);

            if (list is not null)
                throw new InvalidOperationException("List already exist in database");

            list = _mapper.Map<List>(Model);
            _context.lists.Add(list);
                
            foreach (var a in list.Categories)
                _context.Entry(a).State = EntityState.Unchanged;

            foreach (var a in list.Items)
                _context.Entry(a).State = EntityState.Unchanged;
            await _context.SaveChangesAsync();
        }
    }
    public class CreateListModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public IEnumerable<int> Categories { get; set; }
        public IEnumerable<int> Items { get; set; }
        public bool Completed { get; set; }
    }
}
