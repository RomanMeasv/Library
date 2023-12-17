using AutoMapper;
using NextIT_RomanM.Core.Application.Interfaces;
using NextIT_RomanM.Core.Domain.Dto.Book;
using NextIT_RomanM.Core.Domain.Entities;
using NextIT_RomanM.Core.Domain.Interfaces;

namespace NextIT_RomanM.Core.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;
        public BookService(IMapper mapper, IBookRepository bookRepository) 
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
        }
        public Task<Book> Create(NewBookDto newBookDto)
        {
            var book = _mapper.Map<Book>(newBookDto);

            return _bookRepository.Create(book);
        }

        public async Task Delete(string id)
        {
            await _bookRepository.Delete(id);
        }

        public async Task<List<Book>> GetAll()
        {
            return await _bookRepository.GetAll();
        }

        public async Task<Book?> GetById(string id)
        {
            return await _bookRepository.GetById(id);
        }

        public async Task<Book?> Update(string id, UpdateBookDto updateBookDto)
        {
            var book = _mapper.Map<Book>(updateBookDto);

            return await _bookRepository.Update(id, book);
        }
    }
}
