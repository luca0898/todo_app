namespace TodoApp.Domain.Entities
{
    public class Todo
    {
        public Todo(string title, bool finished)
        {
            Title = title;
            Finished = finished;
        }

        public string? Id { get; set; }
        public bool Deleted { get; set; }

        public string Title { get; set; }
        public bool Finished { get; set; }
    }
}
