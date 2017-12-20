using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPlanner.Presentation.Model.Entities
{
    public class Trip
    {
        public int Id { get; set; }

        [Required]
        public string Destination { get; set; }

        public string Comment { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public double Lat { get; set; }

        [Required]
        public double Lng { get; set; }


        public string SightseeingsCollection { get; set; }

        public string TravelUserId { get; set; }

        [ForeignKey("TravelUserId")]
        [Required]
        public TravelUser TravelUser { get; set; }
    }
}
