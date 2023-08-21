namespace CarAPI.Models
{
    public class CarDTO
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Brand { get; set; }
        public int YearManufacture { get; set; }
        public string? Color { get; set; }
    }
}
