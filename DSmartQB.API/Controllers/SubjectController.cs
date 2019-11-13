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


        [Authorize(Roles = "Administrator")]
        [HttpGet, Route("api/ListSubjects/{id}")]
        public IHttpActionResult ListSubjects([FromUri]int id)
        {
            var result = new SubjectService().ListSubjects(id);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator,Teacher")]
        [HttpGet, Route("api/LoadCourses")]
        public IHttpActionResult LoadCourses()
        {
            var result = new SubjectService().LoadCourses();
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet, Route("api/BasicInformations/{id}")]
        public IHttpActionResult BasicInformations([FromUri]string id)
        {
            var result = new SubjectService().BasicInformations(id);
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
            var result = new SubjectService().AddCourse(model);
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
            var result = new SubjectService().EditCourse(model);
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
            var result = new SubjectService().AddCongitiveLevel(name);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet, Route("api/ListCongitives")]
        public IHttpActionResult ListCongitives()
        {
            var result = new SubjectService().ListCongitives();
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
            var result = new SubjectService().Update(model);
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
            var result = new SubjectService().AddPlanner(model);
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
            var result = new SubjectService().UpdatePlanner(model);
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
            var result = new SubjectService().UpdateIlo(model);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost, Route("api/DeletePlanner")]
        public IHttpActionResult DeletePlanner([FromBody]Remove remove)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model");
            }
            var result = new SubjectService().DeletePlanner(remove.Id);
            return Ok(result);
        }


        [Authorize(Roles = "Administrator")]
        [HttpGet, Route("api/ListPlanners/{id}")]
        public IHttpActionResult ListPlanners(string id)
        {
            var result = new SubjectService().ListPlanner(id);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet, Route("api/AllIlos/{id}")]
        public IHttpActionResult AllIlos([FromUri] string id)
        {
            var result = new SubjectService().AllIlos(id);
            return Ok(result);
        }


        [Authorize(Roles = "Administrator")]
        [HttpGet, Route("api/ListIlos/{id}")]
        public IHttpActionResult ListIlos(string id)
        {
            var result = new SubjectService().ListIlos(id);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet, Route("api/LoadIlos/{id}")]
        public IHttpActionResult LoadIlos(string id)
        {
            var result = new SubjectService().LoadIlos(id);
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
            var result = new SubjectService().AddILO(model);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost, Route("api/DeleteCongitive")]
        public IHttpActionResult DeleteCongitive([FromBody]Remove remove)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model");
            }
            var result = new SubjectService().DeleteCongitive(remove.Id);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete, Route("api/DeleteSubject")]
        public IHttpActionResult DeleteSubject([FromBody]Remove remove)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model");
            }
            var result = new SubjectService().DeleteSubject(remove.Id);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost, Route("api/DeleteIlo")]
        public IHttpActionResult DeleteIlo([FromBody]Remove remove)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model");
            }
            var result = new SubjectService().DeleteIlo(remove.Id);
            return Ok(result);
        }

    }
}
