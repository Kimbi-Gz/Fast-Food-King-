namespace FastFoodKing.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<OrdenDetail> OrdenDetails { get; set; }


    }
}
