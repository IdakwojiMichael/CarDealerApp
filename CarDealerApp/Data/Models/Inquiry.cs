namespace CarDealerApp.Data.Models
{
    public class Inquiry
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public int CarId { get; set; }
    }
}
