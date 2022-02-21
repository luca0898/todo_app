namespace TodoApp.Models.ViewModel
{
    public class TodoViewModel
    {
        public TodoViewModel(string id, string title, bool finished)
        {
            Id = id;
            Title = title;
            Finished = finished;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public bool Finished { get; set; }
    }
}
