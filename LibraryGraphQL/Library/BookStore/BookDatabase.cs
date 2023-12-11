
namespace Library.BookStore;

public class BookDatabase : IBookDatabase
{

    private readonly List<BookCollection> _collections;
    private readonly List<Book> _books;
    public BookDatabase()
    {
        _collections = [
            new BookCollection { Id = 1 ,Name = "ABC", Books = [
            new Book { Id = 1, Title = "Book A", Pages = 10},
            new Book { Id = 2, Title = "Book B", Pages = 20},
            new Book { Id = 3, Title = "Book C", Pages = 30},
            ] },
            new BookCollection { Id = 2, Name = "DEF", Books = [
            new Book { Id = 4, Title = "Book D", Pages = 40},
            new Book { Id = 5, Title = "Book E", Pages = 50},
            new Book { Id = 6, Title = "Book F", Pages = 60},
            ] },
        ];

        _books = _collections.SelectMany(x => x.Books ).ToList();
    }


    public Book? GetBook(int id) => _books.FirstOrDefault(x => x.Id == id);

    public IReadOnlyCollection<Book> GetBooks() => _books.AsReadOnly();
    public BookCollection? GetCollection(int id) => _collections.FirstOrDefault(x => x.Id == id);
    public IReadOnlyCollection<BookCollection> GetCollections() => _collections.AsReadOnly();
}