using System.ComponentModel.DataAnnotations;

namespace FastFoodKing.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
        public string UserId { get; set; }
        public Users  Users { get; set; }
        [Required, MinLength(1)]
        public int Count { get; set; }
    }
}
