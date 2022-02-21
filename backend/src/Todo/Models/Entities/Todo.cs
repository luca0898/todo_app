using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models.Entities
{
    public class Todo
    {
        public Todo(string title, bool finished)
        {
            Title = title;
            Finished = finished;
        }

        public string? Id { get; set; }
        public string Title { get; set; }
        public bool Finished { get; set; }
    }
}
