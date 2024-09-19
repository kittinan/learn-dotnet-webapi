using System.Text.Json.Serialization;
using TunApi.Models;
public class TodoDto
{
    //[JsonPropertyName("id")]
    public int id { get; set; }
    public string titlex { get; set; }
    public string description { get; set; }
    //public bool isCompleted { get; set; } = true;
    public DateTime? created { get; set; }
    //public List<TodoFileDto> todoFiles { get; set; }

    public string status { get; set; }

    // Constructor to map Parent model to ParentDto
    public TodoDto(Todo todo)
    {
        id = todo.Id;
        titlex = todo.Title;
        description = todo.Description;
        status = todo.Status.ToString();
    }
}
