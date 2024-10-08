﻿namespace AssignmentJobPortal.Entities
{
    public class Companies
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public List<Jobs>? Jobs { get; set; }
    }
}
