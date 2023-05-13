namespace WebApi.DTO.States
{
    public class CreateStatesDto
    {
        public string Name { get; set; }
        public double population { get; set; }
        public int CountryId { get; set; }
    }
}
