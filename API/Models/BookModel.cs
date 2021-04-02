using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class BookModel
    {
        [Key]
        public int BookId { get; set; }

        public string BookName { get; set; }

        [ForeignKey("CategoryId")]
        public CategoryModel categoryModel { get; set; }
        public int CategoryId { get; set; }

        public string Description { get; set; }
        public string Author { get; set; }
        public string Thumbnail { get; set; }

        public string ThumnailId { get; set; }
    }
}