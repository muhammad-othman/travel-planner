using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPlanner.Presentation.ViewModels
{
    public class TripViewModel
    {
        public TripViewModel()
        {
            Sightseeings = new List<string>();
        }
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


        public ICollection<string> Sightseeings { get; set; }

        public string UserEmail { get; set; }
    }
}
