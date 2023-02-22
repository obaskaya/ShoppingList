using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingList.Entities
{
    public class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string? Description { get; set; }
        public ICollection<List> lists { get; set; }
    }
}
