namespace Common.Models;

public class MessageModel 
{
    public string Message { get; set; } = default!;
    public string ConnectionId { get; set; } = default!;
    public override string ToString() 
        => $"MessageModel {{Message='{Message}', ConnectionId='{ConnectionId}'}}";
}