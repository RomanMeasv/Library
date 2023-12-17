using AutoMapper;
using NextIT_RomanM.Core.Domain.Dto.Auth;
using NextIT_RomanM.Core.Domain.Dto.Book;
using NextIT_RomanM.Core.Domain.Dto.Borrowed;
using NextIT_RomanM.Core.Domain.Entities;
using NextIT_RomanM.Core.Domain.Identity.Entities;

namespace NextIT_RomanM.Core.Domain.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Auth
            CreateMap<User, LoginSuccessDto>(); // out
            CreateMap<LoginDto, User>(); // in

            // Book
            CreateMap<Book, BookDto>(); // out
            CreateMap<NewBookDto, Book>(); // in
            CreateMap<UpdateBookDto, Book>(); // in

            // Borrowed
            CreateMap<Borrowed, BorrowedDto>(); // out
            CreateMap<BorrowedDto, Borrowed>(); // in
        }
    }
}
