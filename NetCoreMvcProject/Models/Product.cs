namespace NetCoreMvcProject.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Stock { get; set; }
        public string Price { get; set; }
        public string Image { get; set; }
        public Brand Brand { get; set; }
    }
}
