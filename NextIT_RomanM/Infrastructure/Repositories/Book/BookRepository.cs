using NextIT_RomanM.Core.Domain.Entities;
using NextIT_RomanM.Core.Domain.Interfaces;
using System.Xml;

namespace NextIT_RomanM.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly XmlDocument _xmlDocument;
        private readonly string _filePath = "Infrastructure\\Library.xml";
        public BookRepository() 
        {
            _xmlDocument = new();
        }

        public Task<Book> Create(Book book)
        {
            _xmlDocument.Load(_filePath);
            // --- Tu by sa dala použiť serializácia ---

            // Prepare Book fields
            var nameElement = _xmlDocument.CreateElement("Name");
            nameElement.InnerText = book.Name; 

            var authorElement = _xmlDocument.CreateElement("Author");
            authorElement.InnerText = book.Author;

            // Prepare Borrowed fields
            var firstNameElement = _xmlDocument.CreateElement("FirstName");
            firstNameElement.InnerText = book.Borrowed.FirstName;

            var lastNameElement = _xmlDocument.CreateElement("LastName");
            lastNameElement.InnerText = book.Borrowed.LastName;

            var fromElement = _xmlDocument.CreateElement("From");
            fromElement.InnerText = "";

            // Construct Borrowed
            var borrowedElement = _xmlDocument.CreateElement("Borrowed");

            borrowedElement.AppendChild(firstNameElement);
            borrowedElement.AppendChild(lastNameElement);
            borrowedElement.AppendChild(fromElement);

            // Construct Book
            var bookElement = _xmlDocument.CreateElement("Book");

            string nextId = GetNextId(_xmlDocument);
            bookElement.SetAttribute("id", nextId);
            bookElement.AppendChild(nameElement);
            bookElement.AppendChild(authorElement);
            bookElement.AppendChild(borrowedElement);

            // Add the book to XML file
            var rootNode = _xmlDocument.SelectSingleNode("Library");
            rootNode?.AppendChild(bookElement);

            _xmlDocument.Save(_filePath);

            // Modify the book's id and return it
            book.Id = nextId;
            return Task.FromResult(book);
        }

        public Task<List<Book>> GetAll()
        {
            _xmlDocument.Load(_filePath);

            List<Book> books = new();

            var bookNodes = _xmlDocument.SelectNodes("/Library/Book");

            if (bookNodes != null)
            {
                foreach (XmlNode bookNode in bookNodes)
                {
                    string id = bookNode.Attributes!["id"]!.Value;
                    string name = bookNode.SelectSingleNode("Name")!.InnerText;
                    string author = bookNode.SelectSingleNode("Author")!.InnerText;

                    var borrowedNode = bookNode.SelectSingleNode("Borrowed");
                    string firstName = borrowedNode!.SelectSingleNode("FirstName")!.InnerText;
                    string lastName = borrowedNode.SelectSingleNode("LastName")!.InnerText;
                    string from = borrowedNode.SelectSingleNode("From")!.InnerText;

                    Borrowed borrowed = new()
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        From = from,
                    };
                    
                    Book book = new()
                    {
                        Id = id,
                        Name = name,
                        Author = author,
                        Borrowed = borrowed,
                    };

                    books.Add(book);
                }
            }

            return Task.FromResult(books);
        }

        public Task Delete(string id)
        {
            _xmlDocument.Load(_filePath);

            // NOTICE: Tu som použil string constructor
            string selectBookWithId = $"/Library/Book[@id='{id}']";
            XmlNode bookNodeToDelete = _xmlDocument.SelectSingleNode(selectBookWithId)!;

            if(bookNodeToDelete != null)
            {
                XmlNode parent = bookNodeToDelete.ParentNode!;
                parent.RemoveChild(bookNodeToDelete);
            }

            _xmlDocument.Save(_filePath);

            return Task.CompletedTask;
        }

        public Task<Book?> GetById(string id)
        {
            _xmlDocument.Load(_filePath);
            
            string selectBookWithId = $"/Library/Book[@id='{id}']";
            XmlNode bookNode = _xmlDocument.SelectSingleNode(selectBookWithId)!;

            if(bookNode != null)
            {
                string name = bookNode.SelectSingleNode("Name")!.InnerText;
                string author = bookNode.SelectSingleNode("Author")!.InnerText;

                XmlNode borrowedNode = bookNode.SelectSingleNode("Borrowed")!;
                string firstName = borrowedNode.SelectSingleNode("FirstName")!.InnerText;
                string lastName = borrowedNode.SelectSingleNode("LastName")!.InnerText;
                string from = borrowedNode.SelectSingleNode("From")!.InnerText;

                Borrowed borrowed = new()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    From = from
                };

                Book book = new()
                {
                    Id = id,
                    Name = name,
                    Author = author,
                    Borrowed = borrowed,
                };

                return Task.FromResult<Book?>(book);
            }

            return Task.FromResult<Book?>(null);
        }

        public Task<Book?> Update(string id, Book book)
        {
            _xmlDocument.Load(_filePath);

            string selectBookWithId = $"/Library/Book[@id='{id}']";
            XmlNode bookNode = _xmlDocument.SelectSingleNode(selectBookWithId)!;

            if (bookNode != null)
            {
                var name = book.Name;
                var author = book.Author;
                bookNode.SelectSingleNode("Name")!.InnerText = name;
                bookNode.SelectSingleNode("Author")!.InnerText = author;

                var firstName = book.Borrowed.FirstName;
                var lastName = book.Borrowed.LastName;
                var from = book.Borrowed.From;
                XmlNode borrowedNode = bookNode.SelectSingleNode("Borrowed")!;
                borrowedNode.SelectSingleNode("FirstName")!.InnerText = firstName;
                borrowedNode.SelectSingleNode("LastName")!.InnerText = lastName;
                borrowedNode.SelectSingleNode("From")!.InnerText = from;


                Borrowed borrowed = new()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    From = from,
                };

                Book updatedBook = new()
                {
                    Id = id,
                    Name = name,
                    Author = author,
                    Borrowed = borrowed,
                };

                _xmlDocument.Save(_filePath);

                return Task.FromResult<Book?>(updatedBook);
            }

            return Task.FromResult<Book?>(null);
        }

        // Táto funkcia zistí najväčšie používané ID a od neho odvíja nasledujúce
        private static string GetNextId(XmlDocument _xmlDocument)
        {
            var bookNodes = _xmlDocument.SelectNodes("/Library/Book");

            int maxId = 0;
            foreach(XmlNode bookNode in bookNodes!)
            {
                string id = bookNode.Attributes!["id"]!.Value;
                int idValue = int.Parse(id);
                if(idValue > maxId)
                {
                    maxId = idValue;
                }
            }

            int nextId = maxId + 1;

            return nextId.ToString();
        }
    }
}
