using DSmartQB.CORE.DTOs;
using DSmartQB.CORE.Services;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DSmartQB.API.Controllers
{
    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SubjectController : ApiController
    {
        SubjectService _service = new SubjectService();


        [Authorize(Roles = "Administrator")]
        [HttpGet, Route("api/ListSubjects/{id}")]
        public IHttpActionResult ListSubjects([FromUri]int id)
        {
            var result = _service.ListSubjects(id);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator,Teacher")]
        [HttpGet, Route("api/LoadCourses")]
        public IHttpActionResult LoadCourses()
        {
            var result = _service.LoadCourses();
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet, Route("api/BasicInformations/{id}")]
        public IHttpActionResult BasicInformations([FromUri]string id)
        {
            var result = _service.BasicInformations(id);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost, Route("api/AddSubject")]
        public IHttpActionResult AddSubject([FromBody]CourseDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model");
            }
            var result = _service.AddCourse(model);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost, Route("api/EditSubject")]
        public IHttpActionResult EditSubject([FromBody]CourseDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model");
            }
            var result = _service.EditCourse(model);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost, Route("api/AddCongitive")]
        public IHttpActionResult AddCongitive([FromBody]string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model");
            }
            var result = _service.AddCongitiveLevel(name);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet, Route("api/ListCongitives")]
        public IHttpActionResult ListCongitives()
        {
            var result = _service.ListCongitives();
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost, Route("api/UpdateCongitive")]
        public IHttpActionResult UpdateCongitive([FromBody]CongitiveDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Fill Empty Records");
            }
            var result = _service.Update(model);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost, Route("api/AddPlanner")]
        public IHttpActionResult AddPlanner([FromBody]PlannerDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model");
            }
            var result = _service.AddPlanner(model);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost, Route("api/UpldatePlanner")]
        public IHttpActionResult UpldatePlanner([FromBody]PlannerBind model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model");
            }
            var result = _service.UpdatePlanner(model);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost, Route("api/UpdateIlo")]
        public IHttpActionResult UpdateIlo([FromBody]ILODto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model");
            }
            var result = _service.UpdateIlo(model);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete, Route("api/DeletePlanner/{id}")]
        public IHttpActionResult DeletePlanner([FromUri]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model");
            }
            var result = _service.DeletePlanner(id);
            return Ok(result);
        }


        [Authorize(Roles = "Administrator")]
        [HttpGet, Route("api/ListPlanners/{id}")]
        public IHttpActionResult ListPlanners(string id)
        {
            var result = _service.ListPlanner(id);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet, Route("api/ListIlos/{id}")]
        public IHttpActionResult ListIlos(string id)
        {
            var result = _service.ListIlos(id);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet, Route("api/LoadIlos/{id}")]
        public IHttpActionResult LoadIlos(string id)
        {
            var result = _service.LoadIlos(id);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost, Route("api/AddILO")]
        public IHttpActionResult AddILO([FromBody]ILODto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model");
            }
            var result = _service.AddILO(model);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete, Route("api/DeleteCongitive/{id}")]
        public IHttpActionResult DeleteCongitive(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model");
            }
            var result = _service.DeleteCongitive(id);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete, Route("api/DeleteSubject/{id}")]
        public IHttpActionResult DeleteSubject(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model");
            }
            var result = _service.DeleteSubject(id);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete, Route("api/DeleteIlo/{id}")]
        public IHttpActionResult DeleteIlo(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model");
            }
            var result = _service.DeleteIlo(id);
            return Ok(result);
        }

    }
}
