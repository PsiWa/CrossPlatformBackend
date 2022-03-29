using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Seregin_Backend.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UName { get; set; }
        public string USurame { get; set; }
        public string UEmail { get; set; }
        [JsonIgnore]
        public ICollection<DesignProject> Projects { get; set; }
    }
}
