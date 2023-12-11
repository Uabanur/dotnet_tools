namespace Library.BookStore;

public class Book
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public int? Pages { get; set; }
}