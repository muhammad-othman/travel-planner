using System;
using System.Collections.Generic;
using System.Text;

namespace TravelPlanner.Shared.Entities
{
    public class Trip
    {
        public int Id { get; set; }

        public string Destination { get; set; }

        public string Comment { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }

        public string SightseeingsCollection { get; set; }

        public string TravelUserId { get; set; }

        public TravelUser TravelUser { get; set; }
    }
}
