using Newtonsoft.Json;
using System.Net;
using System.Web.Mvc;

namespace GrupoEstudosMusical.MVC.Results
{
    public class JsonErrorResult : JsonResult
    {
        private readonly object _data;
        private readonly string _errorMessage;

        public JsonErrorResult(string errorMessage) : this(null, errorMessage) { }

        public JsonErrorResult(object data, string errorMessage)
        {
            _data = data;
            _errorMessage = errorMessage;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;

            response.ContentType = string.IsNullOrEmpty(ContentType) ? "application/json" : ContentType;
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            Data = new DataResult
            {
                Success = false,
                Content = _data,
                ErrorMessage = _errorMessage,
                SuccessMessage = ""
            };

            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.Write(JsonConvert.SerializeObject(Data));
        }
    }
}