using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEvents.Api.Core.Entities
{
#nullable disable
    public class Lecture
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Level { get; set; }

        public int CodeEventId { get; set; }
        public CodeEvent CodeEvent { get; set; }
        public int? SpeakerId { get; set; }
        public Speaker Speaker { get; set; }
    }
}
