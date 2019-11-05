using DSmartQB.CORE.DTOs;
using DSmartQB.CORE.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DSmartQB.API.Controllers
{
    [Authorize(Roles = "Administrator,Teacher")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ExamController : ApiController
    {

        [HttpGet, Route("api/ListExams/{id}")]
        public IHttpActionResult ListExams([FromUri]int id)
        {
            var result = new ExamService().ListExams(id);
            return Ok(result);
        }




        [HttpGet, Route("api/LoadSpecifiedExam/{id}")]
        public IHttpActionResult LoadSpecifiedExam([FromUri]string id)
        {
            var result = new ExamService().LoadSpecifiedExam(id);
            return Ok(result);
        }

        [HttpPost, Route("api/AddExam")]
        public IHttpActionResult AddExam([FromBody]ExamPost model)
        {
            var result = new ExamService().AddSetting(model);
            return Ok(result);
        }

        [HttpPost, Route("api/UpdateSetting")]
        public IHttpActionResult UpdateSetting([FromBody]SpecifedExam model)
        {
            var result = new ExamService().UpdateSetting(model);
            return Ok(result);
        }


        [HttpGet, Route("api/MannualItems/{id}")]
        public IHttpActionResult MannualItems(int id)
        {
            var result = new ExamService().MannualItems(id);
            return Ok(result);
        }

        [HttpPost, Route("api/MannualItems")]
        public IHttpActionResult BluePrint([FromBody]BluePrintParams model)
        {
            var result = new ExamService().BluePrint(model);
            return Ok(result);
        }


        [HttpGet, Route("api/PreviewForSelect/{id}")]
        public IHttpActionResult PreviewForSelect(string Id)
        {
            var result = new ExamService().PreviewForSelect(Id);
            return Ok(result);
        }



        [HttpPost, Route("api/ArchieveItems")]
        public IHttpActionResult ArchieveItems([FromBody]List<ArchieveItems> model)
        {
            var result = new ExamService().ArchieveItems(model);
            return Ok(result);
        }


        [HttpPost, Route("api/Publish")]
        public IHttpActionResult Publish([FromBody]Publish model)
        {
            var result = new ExamService().Publish(model);
            return Ok(result);
        }



        [HttpPost, Route("api/UpdatePreview")]
        public IHttpActionResult UpdatePreview([FromBody]PreviewObj model)
        {
            var result = new ExamService().UpdatePreview(model);
            return Ok(result);
        }


        [HttpGet, Route("api/PreviewExam/{id}")]
        public IHttpActionResult PreviewExam(string id)
        {
            var result = new ExamService().Preview(id);
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
            var result = new ExamService().DeleteExam(id);
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
            var result = new ExamService().DeleteItemArchieve(id);
            return Ok(result);
        }


        [HttpGet, Route("api/OnlineStudentsGrid/{id}")]
        public IHttpActionResult OnlineStudentsGrid(string id)
        {
            var result = new ExamService().OnlineStudentsGrid(id);
            return Ok(result);
        }

        [HttpPost, Route("api/UniversitySetting")]
        public IHttpActionResult UniversitySetting()
        {
            var httpRequest = HttpContext.Current.Request;
            var form = httpRequest.Form;
            string UniversityName = "";
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    foreach (string key in form.AllKeys)
                    {
                        UniversityName = form[key];
                    }

                    foreach (string file in httpRequest.Files)
                    {
                        var postedFileBase = httpRequest.Files[file];
                        if (postedFileBase != null)
                        {
                            string fileLocation = HttpContext.Current.Server.MapPath("~/Logo/") + postedFileBase.FileName;

                            if (File.Exists(fileLocation))
                            {
                                File.Delete(fileLocation);
                            }
                            postedFileBase.SaveAs(fileLocation);

                            string url = "Logo/" + postedFileBase.FileName;

                            var result = new ExamService().AddUniversitySettings(UniversityName, url);
                            return Ok(result);
                        }
                    }
                }
                return BadRequest("No File Uploaded");
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.ToString());
            }
        }

    }
}
