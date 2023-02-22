using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingList.Entities
{
    public class List
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        //Category
        public ICollection<Category>? Categories { get; set; }
        //Items
        public ICollection<Item>? Items { get; set; }

        //Completed Controller
        public bool Completed { get; set; } = true;

    }
}
