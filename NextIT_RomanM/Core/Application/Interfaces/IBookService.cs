using NextIT_RomanM.Core.Domain.Dto.Book;
using NextIT_RomanM.Core.Domain.Entities;

namespace NextIT_RomanM.Core.Application.Interfaces
{
    public interface IBookService
    {
        Task<Book> Create(NewBookDto book);
        Task<Book?> GetById(string id);
        Task<List<Book>> GetAll();
        Task<Book?> Update(string id, UpdateBookDto dto);
        Task Delete(string id);
    }
}
