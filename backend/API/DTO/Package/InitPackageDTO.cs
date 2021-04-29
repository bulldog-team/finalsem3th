using System;

namespace API.DTO.Package
{
    public class InitPackageDTO
    {
        public string SenderName { get; set; }
        public string SenderAddress { get; set; }
        public string ReceiveName { get; set; }
        public string ReceiveAddress { get; set; }

        public string DeliveryType { get; set; }
        public DateTime DateSent { get; set; } = DateTime.Now;

        public int Pincode { get; set; }

        public int Weight { get; set; }
        public string PaymentType { get; set; }
    }
}