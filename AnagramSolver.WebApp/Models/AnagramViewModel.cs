namespace AnagramSolver.WebApp.Models
{
    public class AnagramViewModel
    {
        public string Query { get; set; } = "";
        public List<string> Results { get; set; } = new();
        public string? Error { get; set; }
    }
}
