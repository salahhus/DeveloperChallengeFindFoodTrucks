namespace FindFoodTrucksWebApp.Models
{
    public class ResultModel<T> where T : class
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public List<Error> Errors { get; set; } = new List<Error>();
    }

    public class Error 
    {
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
