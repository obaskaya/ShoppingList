using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingList.DbOperations;

namespace ShoppingList.Application.CategoryApplication.Command.UpdateCommand
{
    public class UpdateCategoryCommand
    {
        public UpdateCategoryModel Model { get; set; }
        public int CategoryId { get; set; }

        //adding context and mapper
        private readonly ShoppingListDbContext _context;
        public UpdateCategoryCommand(ShoppingListDbContext context)
        {
            _context = context;
        }

        public async Task Handle()
        {
            var cats = _context.Categories.Include(c => c.Items).FirstOrDefault(c => c.Id == CategoryId);

            if (cats == null)
                throw new InvalidOperationException("Category doesn't exists in database");

            // updating ex data to new data
            cats.Name = Model.Name != default ? Model.Name : cats.Name;
            cats.Description = Model.Description != default ? Model.Description : cats.Description;
            cats.CreatedDate = Model.CreatedDate != default ? Model.CreatedDate : cats.CreatedDate;
            cats.FinishedDate = Model.FinishedDate != default ? Model.FinishedDate : cats.FinishedDate;

            //setting items
            cats.Items.Clear();
            cats.Items = _context.Items.Where(c => Model.Items.Contains(c.Id)).ToList();

            //implementing
            foreach (var a in cats.Items)
                _context.Entry(a).State = EntityState.Unchanged;
            await _context.SaveChangesAsync();
        }
    }
    public class UpdateCategoryModel
    {

        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime FinishedDate { get; set; }
        public string? Description { get; set; }
        public IEnumerable<int> Items { get; set; }
    }
}
