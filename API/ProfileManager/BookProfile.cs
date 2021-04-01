using API.DTO.Book;
using API.Models;
using AutoMapper;

namespace API.ProfileManager
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookModel, GetBookDTO>().ForMember(item => item.Category, itemInGetBookDTO => itemInGetBookDTO.MapFrom(item => item.categoryModel.Category));

            CreateMap<AddNewBookDTO, BookModel>();
            CreateMap<BookModel, UpdateBookDTO>().ReverseMap();
        }
    }
}