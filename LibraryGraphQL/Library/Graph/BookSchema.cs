using GraphQL.Types;

namespace Library.Graph;

public class BookSchema : Schema
{
    public BookSchema(BookQuery query, IServiceProvider provider) 
        : base(provider)
    {
        Query = query ?? throw new ArgumentNullException(nameof(query));
    }
}