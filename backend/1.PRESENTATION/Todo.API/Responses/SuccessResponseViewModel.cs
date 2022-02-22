namespace TodoApp.API.Responses
{
    public class SuccessResponseViewModel<T>
    {
        public T Data { get; set; }

        public SuccessResponseViewModel(T data)
        {
            Data = data;
        }
    }
}
