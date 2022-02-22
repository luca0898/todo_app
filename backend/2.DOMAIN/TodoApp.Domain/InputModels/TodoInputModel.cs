namespace TodoApp.Domain.InputModels
{
    public class TodoInputModel
    {
        public TodoInputModel(string title, bool finished)
        {
            Title = title;
            Finished = finished;
        }

        public string Title { get; set; }
        public bool Finished { get; set; }
    }
}
