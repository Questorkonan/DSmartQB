using DSmartQB.CORE.Services;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DSmartQB.API.Controllers
{

    [Authorize(Roles = "Administrator,Teacher")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DashboardController : ApiController
    {


        [HttpGet, Route("api/Cards")]
        public IHttpActionResult Cards()
        {
            var result = new DashboardService().Cards();
            return Ok(result);
        }

        [HttpGet, Route("api/Admins")]
        public IHttpActionResult Admins()
        {
            var result = new DashboardService().Admins();
            return Ok(result);
        }


        [HttpGet, Route("api/ItemsPercentage")]
        public IHttpActionResult ItemsPercentage()
        {
            var result = new DashboardService().ItemsPercentage();
            return Ok(result);
        }


        [HttpGet, Route("api/ExamsPercentage")]
        public IHttpActionResult ExamsPercentage()
        {
            var result = new DashboardService().ExamsPercentage();
            return Ok(result);
        }

    }
}
