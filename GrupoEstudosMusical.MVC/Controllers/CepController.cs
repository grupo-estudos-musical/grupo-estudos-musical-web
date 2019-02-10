using MosaicoSolutions.ViaCep;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GrupoEstudosMusical.MVC.Controllers
{
    public class CepController : Controller
    {
        private readonly IViaCepService _viaCepService;

        public CepController(IViaCepService viaCepService)
        {
            _viaCepService = viaCepService;
        }

        public async Task<ActionResult> Consultar(string cep)
        {
            if (string.IsNullOrEmpty(cep))
                return HttpNotFound();
            var endereco = await _viaCepService.ObterEnderecoAsync(cep.Replace("-", ""));
            return Json(endereco, JsonRequestBehavior.AllowGet);
        }
    }
}