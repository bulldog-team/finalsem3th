using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class InvoiceModel
    {
        public int InvoiceId { get; set; }
        public int TotalPrice { get; set; }

        [ForeignKey("PackageId")]
        public PackageModel PackageModel { get; set; }
        public int PackageId { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}