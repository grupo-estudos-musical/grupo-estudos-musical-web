using Newtonsoft.Json;
using System.Net;
using System.Web.Mvc;

namespace GrupoEstudosMusical.MVC.Results
{
    public class JsonSuccesResult : JsonResult
    {
        private readonly object _data;
        private readonly string _successMessage;

        public JsonSuccesResult(string succesMessage) : this(null, succesMessage) { }

        public JsonSuccesResult(object data, string successMessage)
        {
            _data = data;
            _successMessage = successMessage;
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
                Success = true,
                Content = _data,
                ErrorMessage = "",
                SuccessMessage = _successMessage
            };

            response.StatusCode = (int)HttpStatusCode.OK;
            response.Write(JsonConvert.SerializeObject(Data));
        }
    }
}