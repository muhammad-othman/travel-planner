using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPlanner.Presentation.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool isLocked { get; set; }
        public string Picture { get; set; }
        public string Role { get; set; }
        public DateTime CreationDate { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
