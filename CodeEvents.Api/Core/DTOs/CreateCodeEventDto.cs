namespace CodeEvents.Api.Core.DTOs
{
#nullable disable
    public class CreateCodeEventDto
    {
        public string Name { get; set; }
        public DateTime EventDate { get; set; }
        public int Length { get; set; }
        public string LocationAddress { get; set; }
        public string LocationCityTown { get; set; }
        public string LocationStateProvince { get; set; }
        public string LocationPostalCode { get; set; }
        public string LocationCountry { get; set; }
    }
}
