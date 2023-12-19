using MediatR;

public class MessageRequest : IRequest<string>
{
  public string Name { get; set; }
  public MessageRequest(string name)
  {
    Name = name;
  }
}
