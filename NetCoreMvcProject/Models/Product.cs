namespace FirstNetCoreMvcProject.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Stock { get; set; }
        public string Amount { get; set; }
        public string Image { get; set; }
        public Brand Brand { get; set; }
    }
}
