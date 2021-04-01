namespace API.DTO.Book
{
    public class AddNewBookDTO
    {
        public string BookName { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Thumbnail { get; set; }
    }
}