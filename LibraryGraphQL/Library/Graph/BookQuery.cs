using GraphQL;
using GraphQL.Types;
using Library.BookStore;

namespace Library.Graph;

public class BookQuery : ObjectGraphType
{
    private readonly IBookDatabase _books;

    public BookQuery(IBookDatabase books)
    {
        _books = books ?? throw new ArgumentNullException(nameof(books));

        Field<ListGraphType<BookType>>("books")
            .Resolve(r => _books.GetBooks());
        Field<BookType>("book")
            .Argument<int>("id", nullable: false)
            .Resolve(r => _books.GetBook(r.GetArgument<int>("id")));

        Field<ListGraphType<BookCollectionType>>("collections")
            .Resolve(x => _books.GetCollections());
        Field<BookCollectionType>("collection")
            .Argument<int>("id", nullable: false)
            .Resolve(r => _books.GetCollection(r.GetArgument<int>("id")));
    }
}