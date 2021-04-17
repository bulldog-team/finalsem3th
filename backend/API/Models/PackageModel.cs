using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class PackageModel
    {
        [Key]
        public int PackageId { get; set; }
        public string SenderName { get; set; }
        public string SenderAddress { get; set; }
        public string ReceiveName { get; set; }
        public string ReceiveAddress { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        public UserModel User { get; set; }

        [ForeignKey("TypeId")]
        public DeliveryTypeModel DeliveryType { get; set; }
        public int TypeId { get; set; }
        public DateTime DateSent { get; set; }
        public DateTime DateReceived { get; set; }

        public int TotalPrice { get; set; }
        public int Distance { get; set; }

        public int Pincode { get; set; }

        public int Weight { get; set; }

        [ForeignKey("StatusId")]
        public int StatusId { get; set; }
        public PackageStatusModel PackageStatus { get; set; }

        public bool IsPaid { get; set; } = false;

        [ForeignKey("InvoiceId")]
        public int InvoiceId { get; set; }
        public InvoiceModel InvoiceModel { get; set; } = new InvoiceModel();
    }
}