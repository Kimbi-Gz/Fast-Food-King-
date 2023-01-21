namespace FastFoodKing.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }

        public ICollection<Cart> Carts { get; set; }
        public object Cart { get; internal set; }
    }
}
