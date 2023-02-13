namespace CodeEvents.Api.Core.DTOs
{
#nullable disable
    public class LectureDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Title of the lecture
        /// </summary>
        public string Title { get; set; }
        public int Level { get; set; }
    }
}
