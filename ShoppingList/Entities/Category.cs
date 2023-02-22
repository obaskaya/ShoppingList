using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingList.Entities
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime FinishedDate { get; set; }
        public string? Description { get; set; }
        public ICollection<List> lists { get; set; }
        //items 
        public ICollection<Item>? Items { get; set; }
    }
}
