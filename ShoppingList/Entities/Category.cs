using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingList.Entities
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly CreatedDate { get; set; }
        public DateOnly FinishedDate { get; set; }
        public string? Description { get; set; }
        public ICollection<List> lists { get; set; }
        //items 
        public ICollection<Item>? Items { get; set; }
    }
}
