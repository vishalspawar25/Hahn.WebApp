using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Hahn.ApplicatonProcess.Data.models
{
    public class BaseEntity 
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
