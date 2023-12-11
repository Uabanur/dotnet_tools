using GraphQL.Types;
using Library.BookStore;

namespace Library.Graph;

public class BookType : ObjectGraphType<Book>
{
    public BookType()
    {
        Field(x => x.Id, nullable: false)
            .Name("id")
            .Description("Unique book id");
        Field(x => x.Title, nullable: false)
            .Name("title")
            .Description("Book title");
        Field(x => x.Pages, nullable: true)
            .Name("pages")
            .Description("Amount of pages");
    }
}
