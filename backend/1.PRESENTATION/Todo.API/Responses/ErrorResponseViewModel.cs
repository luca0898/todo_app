namespace TodoApp.API.Responses
{
    public class ErrorResponseViewModel
    {
        public string Message { get; set; }

        public ErrorResponseViewModel(string message)
        {
            Message = message;
        }
    }
}
