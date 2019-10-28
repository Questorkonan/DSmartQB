using DSmartQB.CORE.DTOs;
using DSmartQB.CORE.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Text;
using System.Web.Http.Cors;

namespace DSmartQB.API.Controllers
{
    [Authorize(Roles = "Administrator,Teacher")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ItemController : ApiController
    {
        ItemService _item = new ItemService();


        [HttpGet, Route("api/ItemTypes")]
        public IHttpActionResult ItemTypes()
        {
            var result = _item.ItemTypes();
            return Ok(result);
        }



        [HttpPost, Route("api/AddItem")]
        public IHttpActionResult AddItem([FromBody]ItemAddDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model");
            }

            if (model.Alternatives.Count != 0)
            {
                var correct = model.Alternatives.Where(a => a.Correct == true).ToList();
                if (!correct.Any())
                {
                    return BadRequest("Please check at leastes on correct answer");
                }

            }

            var result = _item.AddItem(model);
            return Ok(result);
        }


        [HttpPost, Route("api/UpdateMWQ")]
        public IHttpActionResult UpdateMWQ([FromBody]MCQ model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model");
            }

            if (model.Answers.Count != 0)
            {
                var correct = model.Answers.Where(a => a.Status == true).ToList();
                if (!correct.Any())
                {
                    return BadRequest("Please check at leastes on correct answer");
                }

            }

            var result = _item.EditMWQ(model);
            return Ok(result);
        }

        

        [HttpPost, Route("api/UpdateTF")]
        public IHttpActionResult UpdateTF([FromBody]TF model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model");
            }

            var result = _item.EditTF(model);
            return Ok(result);
        }


        [HttpDelete, Route("api/DeleteItem/{id}")]
        public IHttpActionResult DeleteItem([FromUri]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model");
            }

            var result = _item.DeleteItem(id);
            return Ok(result);
        }


        [HttpGet, Route("api/AssociatedItems/{id}")]
        public IHttpActionResult AssociatedItems(int id)
        {
            var result = _item.AssociatedItems(id);
            return Ok(result);
        }


        [HttpGet, Route("api/UnAssociatedItems/{id}")]
        public IHttpActionResult UnAssociatedItems(int id)
        {
            var result = _item.UnAssociatedItems(id);
            return Ok(result);
        }


        [HttpGet, Route("api/LoadItem/{id}")]
        public IHttpActionResult LoadItem(string id)
        {
            var result = _item.LoadItem(id);
            return Ok(result);
        }

        [HttpGet, Route("api/LoadItemTF/{id}")]
        public IHttpActionResult LoadItemTF(string id)
        {
            var result = _item.LoadItemTF(id);
            return Ok(result);
        }



        [HttpPost, Route("api/UploadItem")]
        public IHttpActionResult UploadItem()
        {
            string[] Levels = new string[] { "Mild", "Normal", "Strong" };

            var httpRequest = HttpContext.Current.Request;
            var form = httpRequest.Form;
            string CreatedBy = "";
            string QuestionType = "";
            try
            {

                if (httpRequest.Files.Count > 0)
                {
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFileBase = httpRequest.Files[file];
                        if (postedFileBase != null)
                        {

                            foreach (string key in form.AllKeys)
                            {
                                CreatedBy = form["CreatedBy"];
                                QuestionType = form["qType"];
                            }

                            string todaysDate = DateTime.Now.ToString("dd-mm-yyyy-hh-mm-ss");
                            string fileName = postedFileBase.FileName;
                            string fileExtension = fileName.Substring(fileName.LastIndexOf('.') + 1).ToLower();
                            fileName = HttpContext.Current.Server.MapPath(string.Format("~/Uploads/{0}.{1}", "Import_" + todaysDate, fileExtension));
                            postedFileBase.SaveAs(fileName);

                            byte[] byteArray = File.ReadAllBytes(fileName);
                            using (MemoryStream mem = new MemoryStream())
                            {
                                mem.Write(byteArray, 0, byteArray.Length);
                                using (WordprocessingDocument doc = WordprocessingDocument.Open(mem, true))
                                {
                                    MainDocumentPart mainPart = doc.MainDocumentPart;
                                    IEnumerable<Table> tables = mainPart.Document.Descendants<Table>();

                                    var index = 0;
                                    string message = "";
                                    StringBuilder errorBuilder = new StringBuilder();
                                    errorBuilder.AppendLine("<div class='alert alert-danger'  role='alert'>");

                                    bool flag = false;

                                    foreach (var table in tables)
                                    {
                                        index++;

                                        IEnumerable<TableRow> rows = table.Descendants<TableRow>();
                                        TableRow QuestionTheme = rows.First();
                                        TableRow ILOS = rows.ElementAt(1);
                                        TableRow Level = rows.ElementAt(2);
                                        TableRow Duration = rows.ElementAt(3);
                                        List<string> correctIlos = new List<string>();

                                        TableCell QuestionThemeCell = QuestionTheme.Descendants<TableCell>().ElementAt(1);
                                        TableCell ILOSCell = ILOS.Descendants<TableCell>().ElementAt(1);
                                        TableCell difficuiltyLevelCell = Level.Descendants<TableCell>().ElementAt(1);
                                        TableCell TimeCell = Duration.Descendants<TableCell>().ElementAt(1);

                                        TableRow ET = rows.ElementAt(4);
                                        TableCell ETCell = ET.Descendants<TableCell>().ElementAt(1);

                                        double expectedTime = 0;
                                        double.TryParse(ETCell.InnerText.Trim(), out expectedTime);

                                        TableRow ShuffleT = rows.ElementAt(5);
                                        TableCell ShuffleTCell = ShuffleT.Descendants<TableCell>().ElementAt(1);
                                        bool shuffle = ShuffleTCell.InnerText.Trim().Equals("True", StringComparison.OrdinalIgnoreCase) ? true : false;


                                        string Question = QuestionThemeCell.InnerText.Trim();
                                        string DiffLevel = difficuiltyLevelCell.InnerText.Trim();
                                        string Time = TimeCell.InnerText.Trim();
                                        List<string> ilos = _item.StringToList(ILOSCell.InnerText.Trim(), ',');




                                        if (string.IsNullOrEmpty(Question))
                                        {
                                            flag = true;
                                            errorBuilder.AppendLine("<p>Question in Table No <b>(" + index + ")</b> is empty</p>");
                                        }
                                        if (string.IsNullOrEmpty(DiffLevel))
                                        {
                                            flag = true;
                                            errorBuilder.AppendLine("<p>Question in Table No <b>(" + index + ")</b> is has no Difficulty Level</p>");
                                        }
                                        if (!Levels.Contains(DiffLevel))
                                        {
                                            flag = true;
                                            errorBuilder.AppendLine("<p>Question in Table No <b>(" + index + ")</b> difficulty level [" + DiffLevel + "] is wrong</p>");

                                        }
                                        if (string.IsNullOrEmpty(Time))
                                        {
                                            flag = true;
                                            errorBuilder.AppendLine("<p>Question in Table No <b>(" + index + ")</b> is has no Time</p>");
                                        }
                                        if (!ilos.Any())
                                        {
                                            flag = true;
                                            errorBuilder.AppendLine("<p>Question in Table No <b>(" + index + ")</b> is has no ILOS</p>");
                                        }
                                        else
                                        {
                                            flag = false;


                                            foreach (var ilo in ilos)
                                            {
                                                ReturnMessage check = _item.CheckILO(ilo);
                                                if (check.Key == 1)
                                                {
                                                    correctIlos.Add(check.ReturnId);
                                                }
                                                else
                                                {
                                                    errorBuilder.AppendLine("<p>Question in Table No <b>(" + index + ")</b> , " + check + " </p>");

                                                }

                                            }


                                        }


                                        List<KeyValuePair<string, bool>> fileAnswers = new List<KeyValuePair<string, bool>>();

                                        List<AlternativesDto> answers = new List<AlternativesDto>();

                                        for (int i = 4; i < rows.Count(); i++)
                                        {
                                            TableRow row = rows.ElementAt(i);

                                            string converter = row.Descendants<TableCell>().First().InnerText;

                                            if (converter.Contains("MACROBUTTON"))
                                            {
                                                converter = converter.Replace("MACROBUTTON", "").Trim();
                                            }
                                            if (converter.Contains("ProtectForm"))
                                            {
                                                converter = converter.Replace("ProtectForm", "").Trim();
                                            }

                                            fileAnswers.Add(new KeyValuePair<string, bool>(row.Descendants<TableCell>().ElementAt(2).InnerText.Trim(),
                                               converter.Equals("T", StringComparison.OrdinalIgnoreCase) ||
                                               converter.Equals("True", StringComparison.OrdinalIgnoreCase)
                                            ));


                                        }

                                        foreach (var ans in fileAnswers)
                                        {
                                            answers.Add(new AlternativesDto { Text = ans.Key, Correct = ans.Value });
                                        }


                                        if (!answers.Where(a => a.Correct == true).Any())
                                        {
                                            flag = true;
                                            errorBuilder.AppendLine("<p>Question in Table No <b>(" + index + ")</b> is has no correct answers<p>");

                                        }



                                        message = errorBuilder.ToString();

                                        if (flag == false)
                                        {
                                            ItemTypeDto Qtype = new ItemTypeDto
                                            {
                                                Id = QuestionType,
                                                Type = ""
                                            };

                                            foreach (var ilo in correctIlos)
                                            {

                                                QuestionDto qDto = new QuestionDto
                                                {
                                                    Item = Question,
                                                    Ilo = ilo,
                                                    UserId = CreatedBy,
                                                    Duration = Time,
                                                    level = DiffLevel,
                                                    Type = Qtype
                                                };

                                                ItemAddDto ido = new ItemAddDto()
                                                {
                                                    Question = qDto,
                                                    Alternatives = answers
                                                };

                                                _item.AddItem(ido);

                                            }

                                        }


                                        errorBuilder.AppendLine("</hr>");

                                    }

                                    errorBuilder.AppendLine("</div>");

                                    if (flag == true)
                                    {
                                        return BadRequest(message);
                                    }

                                    return Ok();
                                }

                            }
                        }


                    }
                }

            }
            catch (System.Exception ex)
            {

                throw;
            }
            return Ok();
        }



        [HttpPost, Route("api/TFUpload")]
        public IHttpActionResult TFUpload()
        {
            string[] Levels = new string[] { "Mild", "Normal", "Strong" };

            var httpRequest = HttpContext.Current.Request;
            var form = httpRequest.Form;
            string CreatedBy = "";
            string QuestionType = "";
            try
            {

                if (httpRequest.Files.Count > 0)
                {
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFileBase = httpRequest.Files[file];
                        if (postedFileBase != null)
                        {

                            foreach (string key in form.AllKeys)
                            {
                                CreatedBy = form["CreatedBy"];
                                QuestionType = form["qType"];
                            }

                            string todaysDate = DateTime.Now.ToString("dd-mm-yyyy-hh-mm-ss");
                            string fileName = postedFileBase.FileName;
                            string fileExtension = fileName.Substring(fileName.LastIndexOf('.') + 1).ToLower();
                            fileName = HttpContext.Current.Server.MapPath(string.Format("~/Uploads/{0}.{1}", "Import_" + todaysDate, fileExtension));
                            postedFileBase.SaveAs(fileName);

                            byte[] byteArray = File.ReadAllBytes(fileName);
                            using (MemoryStream mem = new MemoryStream())
                            {
                                mem.Write(byteArray, 0, byteArray.Length);
                                using (WordprocessingDocument doc = WordprocessingDocument.Open(mem, true))
                                {
                                    MainDocumentPart mainPart = doc.MainDocumentPart;
                                    IEnumerable<Table> tables = mainPart.Document.Descendants<Table>();

                                    var index = 0;
                                    string message = "";
                                    StringBuilder errorBuilder = new StringBuilder();
                                    errorBuilder.AppendLine("<div class='alert alert-danger'  role='alert'>");

                                    bool flag = false;

                                    foreach (var table in tables)
                                    {
                                        index++;

                                        IEnumerable<TableRow> rows = table.Descendants<TableRow>();
                                        TableRow QuestionTheme = rows.First();
                                        TableRow ILOS = rows.ElementAt(1);
                                        TableRow Level = rows.ElementAt(2);
                                        TableRow Duration = rows.ElementAt(3);
                                        List<string> correctIlos = new List<string>();

                                        TableCell QuestionThemeCell = QuestionTheme.Descendants<TableCell>().ElementAt(1);
                                        TableCell ILOSCell = ILOS.Descendants<TableCell>().ElementAt(1);
                                        TableCell difficuiltyLevelCell = Level.Descendants<TableCell>().ElementAt(1);
                                        TableCell TimeCell = Duration.Descendants<TableCell>().ElementAt(1);

                                        TableRow ET = rows.ElementAt(4);
                                        TableCell ETCell = ET.Descendants<TableCell>().ElementAt(1);

                                        double expectedTime = 0;
                                        double.TryParse(ETCell.InnerText.Trim(), out expectedTime);

                                        TableRow ShuffleT = rows.ElementAt(5);
                                        TableCell ShuffleTCell = ShuffleT.Descendants<TableCell>().ElementAt(1);
                                        bool shuffle = ShuffleTCell.InnerText.Trim().Equals("True", StringComparison.OrdinalIgnoreCase) ? true : false;


                                        string Question = QuestionThemeCell.InnerText.Trim();
                                        string DiffLevel = difficuiltyLevelCell.InnerText.Trim();
                                        string Time = TimeCell.InnerText.Trim();
                                        List<string> ilos = _item.StringToList(ILOSCell.InnerText.Trim(), ',');




                                        if (string.IsNullOrEmpty(Question))
                                        {
                                            flag = true;
                                            errorBuilder.AppendLine("<p>Question in Table No <b>(" + index + ")</b> is empty</p>");
                                        }
                                        if (string.IsNullOrEmpty(DiffLevel))
                                        {
                                            flag = true;
                                            errorBuilder.AppendLine("<p>Question in Table No <b>(" + index + ")</b> is has no Difficulty Level</p>");
                                        }
                                        if (!Levels.Contains(DiffLevel))
                                        {
                                            flag = true;
                                            errorBuilder.AppendLine("<p>Question in Table No <b>(" + index + ")</b> difficulty level [" + DiffLevel + "] is wrong</p>");

                                        }
                                        if (string.IsNullOrEmpty(Time))
                                        {
                                            flag = true;
                                            errorBuilder.AppendLine("<p>Question in Table No <b>(" + index + ")</b> is has no Time</p>");
                                        }
                                        if (!ilos.Any())
                                        {
                                            flag = true;
                                            errorBuilder.AppendLine("<p>Question in Table No <b>(" + index + ")</b> is has no ILOS</p>");
                                        }
                                        else
                                        {
                                            flag = false;


                                            foreach (var ilo in ilos)
                                            {
                                                ReturnMessage check = _item.CheckILO(ilo);
                                                if (check.Key == 1)
                                                {
                                                    correctIlos.Add(check.ReturnId);
                                                }
                                                else
                                                {
                                                    errorBuilder.AppendLine("<p>Question in Table No <b>(" + index + ")</b> , " + check + " </p>");

                                                }

                                            }


                                        }


                                        List<KeyValuePair<string, bool>> fileAnswers = new List<KeyValuePair<string, bool>>();

                                        List<AlternativesDto> answers = new List<AlternativesDto>();

                                        for (int i = 4; i < rows.Count(); i++)
                                        {
                                            TableRow row = rows.ElementAt(i);

                                            string converter = row.Descendants<TableCell>().First().InnerText;

                                            if (converter.Contains("MACROBUTTON"))
                                            {
                                                converter = converter.Replace("MACROBUTTON", "").Trim();
                                            }
                                            if (converter.Contains("ProtectForm"))
                                            {
                                                converter = converter.Replace("ProtectForm", "").Trim();
                                            }

                                            fileAnswers.Add(new KeyValuePair<string, bool>(row.Descendants<TableCell>().ElementAt(2).InnerText.Trim(),
                                               converter.Equals("T", StringComparison.OrdinalIgnoreCase) ||
                                               converter.Equals("True", StringComparison.OrdinalIgnoreCase)
                                            ));


                                        }

                                        foreach (var ans in fileAnswers)
                                        {
                                            answers.Add(new AlternativesDto { Text = ans.Key, Correct = ans.Value });
                                        }


                                        if (!answers.Where(a => a.Correct == true).Any())
                                        {
                                            flag = true;
                                            errorBuilder.AppendLine("<p>Question in Table No <b>(" + index + ")</b> is has no correct answers<p>");

                                        }



                                        message = errorBuilder.ToString();

                                        if (flag == false)
                                        {
                                            ItemTypeDto Qtype = new ItemTypeDto
                                            {
                                                Id = QuestionType,
                                                Type = ""
                                            };

                                            foreach (var ilo in correctIlos)
                                            {

                                                QuestionDto qDto = new QuestionDto
                                                {
                                                    Item = Question,
                                                    Ilo = ilo,
                                                    UserId = CreatedBy,
                                                    Duration = Time,
                                                    level = DiffLevel,
                                                    Type = Qtype,
                                                    TFStatus = answers.FirstOrDefault(a => a.Correct == true).Text
                                                };

                                                ItemAddDto ido = new ItemAddDto()
                                                {
                                                    Question = qDto,
                                                    Alternatives = new List<AlternativesDto>()
                                                };

                                                _item.AddItem(ido);

                                            }

                                        }


                                        errorBuilder.AppendLine("</hr>");

                                    }

                                    errorBuilder.AppendLine("</div>");

                                    if (flag == true)
                                    {
                                        return BadRequest(message);
                                    }

                                    return Ok();
                                }

                            }
                        }


                    }
                }

            }
            catch (Exception ex)
            {

                throw;
            }
            return Ok();
        }


    }
}
