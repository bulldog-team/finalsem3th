using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class PackageStatusModel
    {
        [Key]
        public int StatusId { get; set; }
        public string Status { get; set; }

        public ICollection<PackageModel> Packages { get; set; }
    }
}