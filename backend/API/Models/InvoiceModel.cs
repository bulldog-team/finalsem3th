using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class InvoiceModel
    {
        [Key]
        public int InvoiceId { get; set; }
        public int TotalPrice { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;

        [ForeignKey("PackageModel")]
        public int PackageId { get; set; }
        public PackageModel PackageModel { get; set; }

    }
}