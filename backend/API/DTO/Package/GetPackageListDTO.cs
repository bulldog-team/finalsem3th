using System;

namespace API.DTO.Package
{
    public class GetPackageListDTO
    {
        public int PackageId { get; set; }
        public string SenderName { get; set; }
        public string ReceiveName { get; set; }

        public DateTime DateSent { get; set; }

        public int TotalPrice { get; set; }
        public bool IsPaid { get; set; }

        public string Type { get; set; }
        public string PackageStatus { get; set; }

    }
}