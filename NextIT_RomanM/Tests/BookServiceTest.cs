using AutoMapper;
using Moq;
using NextIT_RomanM.Core.Application.Interfaces;
using NextIT_RomanM.Core.Application.Services;
using NextIT_RomanM.Core.Domain.Entities;
using NextIT_RomanM.Core.Domain.Interfaces;

namespace NextIT_RomanM.Tests
{
    public class BookServiceTest
    {
        // NOTICE: Názov metódy zodpovedá čo sa testuje, aké je nastavenie a čo sa očakáva od testu
        [Fact]
        public void CreateBookService_WithNonNullParameters_ExpectArgumentNullException()
        {
            // Arrange
            var mockRepository = new Mock<IBookRepository>();
            var mockMapper = new Mock<IMapper>();

            // Act
            IBookService bookService = new BookService(mockMapper.Object, mockRepository.Object);

            // Assert
            Assert.NotNull(bookService);
            Assert.True(bookService is BookService);
        }

        // NOTICE: Táto metóda je async lebo moje repositories vracajú Task
        // NOTICE: Táto metóda nemá tak dlhý názov lebo sa testuje ako by mala konkrétna implementácia fungovať
        [Fact]
        public async void GetAll()
        {
            // Arrange
            var mockRepository = new Mock<IBookRepository>();
            var mockMapper = new Mock<IMapper>();

            List<Book> mockLibrary = new()
            {
                new Book
                {
                    Id = "1",
                    Name = "Starec a more",
                    Author = "Ernest Hemingway",
                    Borrowed = new()
                    {
                        FirstName = "Peter",
                        LastName = "Prvý",
                        From = "25.3.2016",
                    },
                },
                new Book
                {
                    Id = "2",
                    Name = "Rómeo a Júlia",
                    Author = "William Shakespeare",
                    Borrowed = new()
                    {
                        FirstName = "Lukáš",
                        LastName = "Druhý",
                        From = "16.6.2018",
                    }
                },
            };

            mockRepository.Setup(repo => repo.GetAll()).Returns(Task.FromResult(mockLibrary));

            IBookService bookService = new BookService(mockMapper.Object, mockRepository.Object);

            // Act
            // NOTICE: používam await
            List<Book> library = await bookService.GetAll();

            // Assert
            Assert.NotNull(library);
            Assert.True(library is List<Book>);
            Assert.Equal(mockLibrary, library);
            Assert.Equal(mockLibrary.Count, library.Count);
            // NOTICE: Tu testujem či sa metóda zavolala PRESNE raz
            mockRepository.Verify(repo => repo.GetAll(), Times.Once);
        }

        // TODO: Ďalej by som pokračoval testami ostatných metód...
        // GetById returns Book? so test if the null is ever getting returned if the book doesn't exist (edge cases)
    }
}
