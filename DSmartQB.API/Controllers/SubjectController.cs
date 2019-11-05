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
        [HttpDelete, Route("api/DeletePlanner/{id}")]
        public IHttpActionResult DeletePlanner([FromUri]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model");
            }
            var result = new SubjectService().DeletePlanner(id);
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
        [HttpGet, Route("api/AllIlos")]
        public IHttpActionResult AllIlos()
        {
            var result = new SubjectService().AllIlos();
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
        [HttpDelete, Route("api/DeleteCongitive/{id}")]
        public IHttpActionResult DeleteCongitive(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model");
            }
            var result = new SubjectService().DeleteCongitive(id);
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
            var result = new SubjectService().DeleteSubject(id);
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
            var result = new SubjectService().DeleteIlo(id);
            return Ok(result);
        }

    }
}
