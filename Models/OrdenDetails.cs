namespace FastFoodKing.Models
{
    public class OrdenDetails
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
        public double Total { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public int phone { get; set; }
    }
}
