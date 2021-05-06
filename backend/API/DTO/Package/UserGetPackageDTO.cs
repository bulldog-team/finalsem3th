using System;

namespace API.DTO.Package
{
    public class UserGetPackageDTO
    {
        public int PackageId { get; set; }
        public string SenderName { get; set; }
        public string SenderAddress { get; set; }
        public string ReceiveName { get; set; }
        public string ReceiveAddress { get; set; }

        public string CreatedBy { get; set; }

        public string DeliveryType { get; set; }
        public DateTime DateSent { get; set; }
        public Nullable<DateTime> DateReceived { get; set; } = null;

        public string paymentType { get; set; }

        public int TotalPrice { get; set; }
        public int Distance { get; set; }

        public int Pincode { get; set; }

        public int Weight { get; set; }

        public string Status { get; set; }

        public bool IsPaid { get; set; }

    }
}