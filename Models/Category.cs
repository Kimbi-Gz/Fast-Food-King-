namespace FastFoodKing.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Menu> Menus { get; set; }
        public object Menu { get; internal set; }
    }
}
