using NextIT_RomanM.Core.Domain.Entities;

namespace NextIT_RomanM.Core.Domain.Interfaces
{
    public interface IBookRepository
    {
        Task<Book> Create(Book book);
        Task<List<Book>> GetAll();
        Task Delete(string id);
        Task<Book?> GetById(string id);
        Task<Book?> Update(string id, Book book);
    }
}
