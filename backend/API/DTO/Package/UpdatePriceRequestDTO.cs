namespace API.DTO.Package
{
    public class UpdatePriceRequestDTO
    {
        public int TypeId { get; set; }

        public string TypeName { get; set; }

        public int UnitPrice { get; set; }

    }
}