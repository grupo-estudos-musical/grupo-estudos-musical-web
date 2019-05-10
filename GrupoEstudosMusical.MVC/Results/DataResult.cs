namespace GrupoEstudosMusical.MVC.Results
{
    public class DataResult
    {
        public bool Success { get; set; }
        public object Content { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
    }
}