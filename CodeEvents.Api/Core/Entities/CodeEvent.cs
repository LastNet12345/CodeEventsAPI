﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEvents.Api.Core.Entities
{
#nullable disable
    public class CodeEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime EventDate { get; set; }
        public int Length { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }

        public ICollection<Lecture> Lectures { get; set; }


    }
}
