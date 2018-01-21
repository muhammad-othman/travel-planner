using System;
using System.Collections.Generic;
using System.Text;

namespace TravelPlanner.Shared.Entities
{
    public class TravelUserLoginInfo
    {
        public int Id { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderDisplayName { get; set; }
    }
}
