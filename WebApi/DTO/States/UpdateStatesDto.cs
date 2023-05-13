namespace WebApi.DTO.States
{
    public class UpdateStatesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double population { get; set; }
        public int CountryId { get; set; }
    }
}
