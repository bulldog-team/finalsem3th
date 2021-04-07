using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class DeliveryTypeModel
    {
        [Key]
        public int TypeId { get; set; }

        public string TypeName { get; set; }

        public int UnitPrice { get; set; }

        public ICollection<PackageModel> Packages { get; set; }

    }
}