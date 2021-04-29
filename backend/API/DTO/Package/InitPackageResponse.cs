using System;

namespace API.DTO.Package
{
    public class InitPackageResponse
    {
        public int PackageId { get; set; }
        public string SenderName { get; set; }
        public string SenderAddress { get; set; }
        public string ReceiveName { get; set; }
        public string ReceiveAddress { get; set; }

        public string DeliveryType { get; set; }
        public DateTime DateSent { get; set; }

        public int TotalPrice { get; set; }
        public int Distance { get; set; }

        public int Pincode { get; set; }

        public int Weight { get; set; }

        public bool IsPaid { get; set; }

    }
}