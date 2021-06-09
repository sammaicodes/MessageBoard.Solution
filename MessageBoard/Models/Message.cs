namespace MessageBoard.Models
{
  public class Message
  {
    public string MessageBody { get; set; }
    public int MessageId { get; set; }
    public int GroupId { get; set; }
    public string Sender { get; set; }
    public string DateTime { get; set; }
    public virtual Group Group { get; set; }
  }
}