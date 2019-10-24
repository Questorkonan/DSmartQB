using DSmartQB.CORE.DTOs;
using DSmartQB.CORE.Services;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DSmartQB.API.Controllers
{
    [Authorize(Roles = "Administrator,Teacher")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ExamController : ApiController
    {
        ExamService _service = new ExamService();

        
        [HttpGet, Route("api/ListExams/{id}")]
        public IHttpActionResult ListExams([FromUri]int id)
        {
            var result = _service.ListExams(id);
            return Ok(result);
        }

        [HttpPost, Route("api/AddExam")]
        public IHttpActionResult AddExam([FromBody]ExamPost model)
        {
            var result = _service.AddSetting(model);
            return Ok(result);
        }

        [HttpGet, Route("api/MannualItems/{id}")]
        public IHttpActionResult MannualItems(int id)
        {
            var result = _service.MannualItems(id);
            return Ok(result);
        }
    }
}
