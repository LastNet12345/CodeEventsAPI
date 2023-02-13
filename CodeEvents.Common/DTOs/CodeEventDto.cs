

namespace CodeEvents.Api.Core.DTOs
{
#nullable disable

    /// <summary>
    /// A code event with Name, Date and other stuff
    /// </summary>
    public class CodeEventDto
    {
        /// <summary>
        /// Name of the event
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Date of the event
        /// </summary>
        public DateTime EventDate { get; set; }
        public int Length { get; set; }
        public string LocationAddress { get; set; }
        public string LocationCityTown { get; set; }
        public string LocationStateProvince { get; set; }
        public string LocationPostalCode { get; set; }
        public string LocationCountry { get; set; }
       
        public ICollection<LectureDto> Lectures { get; set; }
    }
}
