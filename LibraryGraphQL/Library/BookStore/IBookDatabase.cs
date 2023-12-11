namespace Library.BookStore;

public interface IBookDatabase
{
    public Book? GetBook(int id);
    public IReadOnlyCollection<Book> GetBooks();
    public BookCollection? GetCollection(int id);
    public IReadOnlyCollection<BookCollection> GetCollections();
}