using MediatR;

public class MessageHandler : IRequestHandler<MessageRequest, string>
{
  public Task<string> Handle(MessageRequest request, CancellationToken token)
  {
    return Task.FromResult($"Hello {request.Name} from handler");
  }
}
