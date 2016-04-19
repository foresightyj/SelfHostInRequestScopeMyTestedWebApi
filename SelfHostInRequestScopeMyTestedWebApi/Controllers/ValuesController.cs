using SelfHostWebApiNinjectInRequestScope.Models;
using System.Net.Http;
using System.Web.Http;

namespace SelfHostInRequestScopeMyTestedWebApi.Controllers
{
    [RoutePrefix("api")]
    public class ValuesController : ApiController
    {
        private readonly ICalc _calc;
        private readonly ICalc _calc2;

        public ValuesController(ICalc calc, ICalc calc2)
        {
            _calc = calc;
            _calc2 = calc2;
        }

        [Route("values/{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var res1 = _calc.Add(id, id);
            var res2 = _calc2.Add(id, id);
            var calc3 = (ICalc)(Request.GetDependencyScope().GetService(typeof(ICalc)));
            //If InRequestScope works as expected, all ICalc instances here will be the same object, with the same ID (==1).
            //But in fact, in unit test, when using the Self Hosting option, the result is 1 + 2 + 3 == 6
            var res = _calc.Id + _calc2.Id + calc3.Id;
            return Ok(res);
        }
    }
}