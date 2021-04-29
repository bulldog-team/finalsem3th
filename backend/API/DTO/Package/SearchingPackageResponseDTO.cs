using System;

namespace API.DTO.Package
{
    public class SearchingPackageResponseDTO
    {
        public int PackageId { get; set; }
        public string SenderAddress { get; set; }
        public string ReceiveAddress { get; set; }

        public int UserId { get; set; }

        public string PackageType { get; set; }

        public DateTime DateSent { get; set; }
        public Nullable<DateTime> DateReceived { get; set; }
        public int TotalPrice { get; set; }
        public int Distance { get; set; }

        public int Pincode { get; set; }

        public int Weight { get; set; }

        public string PackageStatus { get; set; }

        public bool IsPaid { get; set; }

    }
}