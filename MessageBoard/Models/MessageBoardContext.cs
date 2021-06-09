using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MessageBoard.Models
{
  public class MessageBoardContext : DbContext 
  {
    public MessageBoardContext(DbContextOptions<MessageBoardContext> options)
      : base(options)
      {
      }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Group>()
        .HasData(

          new Group { GroupId = 1 , GroupName = "Test Group", GroupDescription = "Test Description"}
        );
      // builder.Entity<Message>()
      //   .HasData(

      //     new Message { MessageId = 1 , GroupId = 1, MessageBody = "Test MessageBody", Sender = "Test Sender", DateTime = "Test Date"}
      //   );
    }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Message> Messages {get; set; }
  }

}