using System.ComponentModel;

using System.ComponentModel.DataAnnotations;

namespace MessageBoardApi.Models
{
  public class Groups
  {
    [Key]
    public int GroupId { get; set;}
    public string Name { get; set; }

    List<Posts> Posts { get; set; }

  }
}