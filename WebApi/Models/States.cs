namespace WebApi.Models
{
    public class States
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double population { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }
    }
}
