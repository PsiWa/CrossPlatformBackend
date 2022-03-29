using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Seregin_Backend.Models
{
    public class DesignProject
    {
        public int DesignProjectID { get; set; }
        public int AptID { get; set; }
        public int UserID { get; set; }
        public User Usr { get; set; }
        [JsonIgnore]
        public Apartment Apt { get; set; }
    }
}
