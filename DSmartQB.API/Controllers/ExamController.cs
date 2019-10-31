using DSmartQB.CORE.DTOs;
using DSmartQB.CORE.Services;
using System.Collections.Generic;
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

       


        [HttpGet, Route("api/LoadSpecifiedExam/{id}")]
        public IHttpActionResult LoadSpecifiedExam([FromUri]string id)
        {
            var result = _service.LoadSpecifiedExam(id);
            return Ok(result);
        }

        [HttpPost, Route("api/AddExam")]
        public IHttpActionResult AddExam([FromBody]ExamPost model)
        {
            var result = _service.AddSetting(model);
            return Ok(result);
        }

        [HttpPost, Route("api/UpdateSetting")]
        public IHttpActionResult UpdateSetting([FromBody]SpecifedExam model)
        {
            var result = _service.UpdateSetting(model);
            return Ok(result);
        }


        [HttpGet, Route("api/MannualItems/{id}")]
        public IHttpActionResult MannualItems(int id)
        {
            var result = _service.MannualItems(id);
            return Ok(result);
        }


        [HttpGet, Route("api/PreviewForSelect/{id}")]
        public IHttpActionResult PreviewForSelect(string Id)
        {
            var result = _service.PreviewForSelect(Id);
            return Ok(result);
        }



        [HttpPost, Route("api/ArchieveItems")]
        public IHttpActionResult ArchieveItems([FromBody]List<ArchieveItems> model)
        {
            var result = _service.ArchieveItems(model);
            return Ok(result);
        }
        

        [HttpPost, Route("api/Publish")]
        public IHttpActionResult Publish([FromBody]Publish model)
        {
            var result = _service.Publish(model);
            return Ok(result);
        }



        [HttpPost, Route("api/UpdatePreview")]
        public IHttpActionResult UpdatePreview([FromBody]PreviewObj model)
        {
            var result = _service.UpdatePreview(model);
            return Ok(result);
        }


        [HttpGet, Route("api/PreviewExam/{id}")]
        public IHttpActionResult PreviewExam(string id)
        {
            var result = _service.Preview(id);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator,Teacher")]
        [HttpDelete, Route("api/DeleteExam/{id}")]
        public IHttpActionResult DeleteExam(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model");
            }
            var result = _service.DeleteExam(id);
            return Ok(result);
        }
        

        [Authorize(Roles = "Administrator,Teacher")]
        [HttpDelete, Route("api/DeleteItemArchieve/{id}")]
        public IHttpActionResult DeleteItemArchieve(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model");
            }
            var result = _service.DeleteItemArchieve(id);
            return Ok(result);
        }


        [HttpGet, Route("api/OnlineStudentsGrid/{id}")]
        public IHttpActionResult OnlineStudentsGrid(string id)
        {
            var result = _service.OnlineStudentsGrid(id);
            return Ok(result);
        }

    }
}
