using DSmartQB.CORE.DTOs;
using DSmartQB.CORE.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web;
using System.IO;
using System.Text;
using System.Web.Http.Cors;

namespace DSmartQB.API.Controllers
{

    [Authorize(Roles = "Administrator,Teacher")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DashboardController : ApiController
    {

        DashboardService _service = new DashboardService();

        [HttpGet, Route("api/Cards")]
        public IHttpActionResult Cards()
        {
            var result = _service.Cards();
            return Ok(result);
        }

        [HttpGet, Route("api/Admins")]
        public IHttpActionResult Admins()
        {
            var result = _service.Admins();
            return Ok(result);
        }


        [HttpGet, Route("api/ItemsPercentage")]
        public IHttpActionResult ItemsPercentage()
        {
            var result = _service.ItemsPercentage();
            return Ok(result);
        }


        [HttpGet, Route("api/ExamsPercentage")]
        public IHttpActionResult ExamsPercentage()
        {
            var result = _service.ExamsPercentage();
            return Ok(result);
        }

    }
}
