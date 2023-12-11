using GraphQL;
using GraphQL.Types;
using Library.BookStore;

namespace Library.Graph;

public class BookCollectionType : ObjectGraphType<BookCollection>
{
    public BookCollectionType()
    {
        Field(x => x.Id, nullable: false)
            .Name("id")
            .Description("Unique collection id");
        Field(x => x.Name, nullable: false)
            .Name("name")
            .Description("Collection name");
        Field<ListGraphType<BookType>>(name: "books")
            .Returns<IReadOnlyCollection<Book>>()
            .Resolve(r => r.Source.Books)
            .Description("Books in collection");
        Field<BookType>(name: "book")
            .Argument<int>("id")
            .Returns<Book>()
            .Resolve(r => r.Source.GetBook(r.GetArgument<int>("id")))
            .Description("Book in collection");
    }
}
