using SpaceCamp.Application.Features.Profiles;
using System;
using System.Collections.Generic;

namespace SpaceCamp.Application.Features.Activities
{
    public class ActivityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string City { get; set; }
        public string Venue { get; set; }
        public string HostUsername { get; set; }
        public bool IsCancelled { get; set; }
        public ICollection<AttendeeDto> Attendees { get; set; }
    }
}
