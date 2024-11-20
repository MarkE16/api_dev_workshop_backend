namespace TodoApi.Models
{
  public class TodoItemDto
  {
    public long Id { get; set; }
    public string? Name { get; set; }
    public bool Completed { get; set; }
  }
}