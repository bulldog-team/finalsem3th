using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class CategoryModel
    {
        [Key]
        public int CategoryId { get; set; }
        public string Category { get; set; }

        public ICollection<BookModel> BookModels { get; set; }
    }
}