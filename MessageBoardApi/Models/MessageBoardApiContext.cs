using Microsoft.EntityFrameworkCore;

namespace MessageBoardApi.Models
{
  public class MessageBoardApiContext : DbContext
  {
    public DbSet<Posts> Post { get; set; }

    public DbSet<Groups> Group { get; set; }

    public MessageBoardApiContext(DbContextOptions<MessageBoardApiContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Groups>()
        .HasData(
          new Groups { GroupId = 1, Name = "Dogs" },
          new Groups { GroupId = 2, Name = "Cats" }
        );
      builder.Entity<Posts>()
        .HasData(
          new Posts { UserId = 2, PostId = 2, Name = "Mammoth", GroupId = 2, Message = "I'm a mammoth", DateTime = "2021-02-01 12:00:00" }
        );
    }
  }
}