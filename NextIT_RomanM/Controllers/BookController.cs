using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using NextIT_RomanM.Core.Application.Interfaces;
using NextIT_RomanM.Core.Domain.Dto.Book;
using System.ComponentModel.DataAnnotations;

namespace NextIT_RomanM.Controllers
{
    [Produces("application/json")]
    public class BookController : BaseController
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        // NOTE: Tento "token" sa vymieňa pri každom requeste aby som vedel či je user authenticated
        private const string accessToken = "123";
        public BookController(IBookService bookService, IMapper mapper) 
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDto))]
        public async Task<IActionResult> Create([FromQuery][Required] string token, [FromBody][Required] NewBookDto dto) 
        {
            if(token is not accessToken)
            {
                return BadRequest();
            }

            var book = await _bookService.Create(dto);

            return Ok(_mapper.Map<BookDto>(book));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BookDto>))]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAll();

            return Ok(_mapper.Map<List<BookDto>>(books));
        }

        // NOTE: Tu by som mohol teoreticky vrátiť Book instance ak sa niečo vymazalo
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute][Required] string id)
        {
            await _bookService.Delete(id);

            return Ok();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDto))]
        public async Task<IActionResult> GetById([FromRoute][Required] string id)
        {
            var book = await _bookService.GetById(id);

            return Ok(_mapper.Map<BookDto>(book));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDto))]
        public async Task<IActionResult> Update([FromRoute][Required] string id, [FromBody][Required] UpdateBookDto dto)
        {
            var book = await _bookService.Update(id, dto);

            return Ok(_mapper.Map<BookDto>(book));
        }
    }
}
