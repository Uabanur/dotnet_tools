namespace Library.BookStore;

public class BookCollection
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required List<Book> Books { get; set; }
    public Book? GetBook(int id) => Books.FirstOrDefault(x => x.Id == id);
}