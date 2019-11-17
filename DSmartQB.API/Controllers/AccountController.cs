using DSmartQB.CORE.DTOs;
using DSmartQB.CORE.Services;
using System;
using System.Web.Http;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Web;
using System.Web.Http.Cors;
using System.Collections.Generic;

namespace DSmartQB.API.Controllers
{
    [Authorize(Roles = "Administrator")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountController : ApiController
    {

        [HttpPost, Route("api/CreateUser")]
        public IHttpActionResult CreateUser([FromBody]UserDto user)
        {
            user.RoleId = new AccountService().GetRoleId("Teacher");
            var result = new AccountService().AddUser(user);
            return Ok(result);
        }

        [HttpPost, Route("api/CreateStudent")]
        public IHttpActionResult CreateStudent([FromBody]UserDto user)
        {
            user.RoleId = new AccountService().GetRoleId("Student");
            var result = new AccountService().AddUser(user);
            return Ok(result);
        }

        [HttpPost, Route("api/UpdateUser")]
        public IHttpActionResult UpdateUser([FromBody]UserDto user)
        {
            var result = new AccountService().Update(user);
            return Ok(result);
        }

        [HttpPost, Route("api/ChangePassword")]
        public IHttpActionResult ChangePassword([FromBody]UserDto user)
        {
            var result = new AccountService().ChangePassword(user);
            return Ok(result);
        }

        

        [HttpPost, Route("api/UserProfile")]
        public IHttpActionResult UserProfile([FromBody]UserProfile user)
        {
            var result = new AccountService().UserProfile(user);
            return Ok(result);
        }



        [HttpGet, Route("api/ListTeachers")]
        public IHttpActionResult ListTeachers()
        {
            var result = new AccountService().ListTeachers();
            return Ok(result);
        }



        [HttpGet, Route("api/ListUsers/{id}")]// id mean page
        public IHttpActionResult ListUsers([FromUri]int id)
        {
            var result = new AccountService().ListUsers(id);
            return Ok(result);
        }

        [HttpGet, Route("api/ListStudents/{id}")]// id mean page
        public IHttpActionResult ListStudents([FromUri]int id)
        {
            var result = new AccountService().ListStudents(id);
            return Ok(result);
        }

        [HttpGet, Route("api/StudentsForGroup/{id}")]
        public IHttpActionResult StudentsForGroup([FromUri] string id)
        {
            var result = new AccountService().StudentsForGroup(id);
            return Ok(result);
        }

        [HttpPost, Route("api/AddBulkTeacher")]
        public IHttpActionResult AddBulkTeacher()
        {
            var httpRequest = HttpContext.Current.Request;
            var form = httpRequest.Form;
            var ds = new DataSet();
            ReturnMessage result = new ReturnMessage();

            #region Guid

            string path = Path.GetRandomFileName();
            path = path.Replace(".", "");
            string Guid = path.Substring(0, 8);

            #endregion


            string CreatedBy = "";
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
                                CreatedBy = form[key];
                            }

                            string fileExtension = Path.GetExtension(postedFileBase.FileName);
                            if (fileExtension == ".xls" || fileExtension == ".xlsx")
                            {
                                string fileLocation = HttpContext.Current.Server.MapPath("~/Uploads/") + postedFileBase.FileName + Guid;
                                if (File.Exists(fileLocation))
                                {
                                    File.Delete(fileLocation);
                                }
                                postedFileBase.SaveAs(fileLocation);
                                var excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                                if (fileExtension == ".xls")
                                {
                                    excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                                }
                                else if (fileExtension == ".xlsx")
                                {
                                    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                                }
                                OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                                excelConnection.Open();
                                var dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                if (dt == null)
                                {
                                    return null;
                                }
                                var excelSheets = new string[dt.Rows.Count];
                                int t = 0;
                                foreach (DataRow row in dt.Rows)
                                {
                                    excelSheets[t] = row["TABLE_NAME"].ToString();
                                    t++;
                                }

                                var excelConnection1 = new OleDbConnection(excelConnectionString);

                                string query = $"Select * from [{excelSheets[0]}]";

                                using (var dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                                {
                                    dataAdapter.Fill(ds);
                                }
                                int count = Convert.ToInt32(ds.Tables[0].Rows.Count);
                                for (int i = 0; i < count; i++)
                                {
                                    var firstname = ds.Tables[0].Rows[i][0].ToString();
                                    var lastname = ds.Tables[0].Rows[i][1].ToString();
                                    var email = ds.Tables[0].Rows[i][2].ToString();
                                    var password = ds.Tables[0].Rows[i][3].ToString();
                                    var phone = ds.Tables[0].Rows[i][5].ToString();
                                    var username = ds.Tables[0].Rows[i][4].ToString();


                                    UserDto model = new UserDto
                                    {
                                        Firstname = firstname,
                                        Lastname = lastname,
                                        Email = email,
                                        Password = password,
                                        Phone = phone,
                                        Username = username,
                                        RoleId = new AccountService().GetRoleId("Teacher")
                                    };
                                    result = new AccountService().AddUser(model);

                                }
                                return Ok(result);
                            }
                            else
                            {
                                return BadRequest("You must upload excel sheets only");
                            }
                        }
                    }
                }
                return BadRequest("No File Uploaded");
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }


        }

        [HttpPost, Route("api/AddBulkStudent")]
        public IHttpActionResult AddBulkStudent()
        {
            var httpRequest = HttpContext.Current.Request;
            var form = httpRequest.Form;
            var ds = new DataSet();
            ReturnMessage result = new ReturnMessage();

            #region Guid

            string path = Path.GetRandomFileName();
            path = path.Replace(".", "");
            string Guid = path.Substring(0, 8);

            #endregion


            string CreatedBy = "";
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
                                CreatedBy = form[key];
                            }

                            string fileExtension = Path.GetExtension(postedFileBase.FileName);
                            if (fileExtension == ".xls" || fileExtension == ".xlsx")
                            {
                                string fileLocation = HttpContext.Current.Server.MapPath("~/Uploads/") + postedFileBase.FileName + Guid;
                                if (File.Exists(fileLocation))
                                {
                                    File.Delete(fileLocation);
                                }
                                postedFileBase.SaveAs(fileLocation);
                                var excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                                if (fileExtension == ".xls")
                                {
                                    excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                                }
                                else if (fileExtension == ".xlsx")
                                {
                                    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                                }
                                OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                                excelConnection.Open();
                                var dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                if (dt == null)
                                {
                                    return null;
                                }
                                var excelSheets = new string[dt.Rows.Count];
                                int t = 0;
                                foreach (DataRow row in dt.Rows)
                                {
                                    excelSheets[t] = row["TABLE_NAME"].ToString();
                                    t++;
                                }

                                var excelConnection1 = new OleDbConnection(excelConnectionString);

                                string query = $"Select * from [{excelSheets[0]}]";

                                using (var dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                                {
                                    dataAdapter.Fill(ds);
                                }
                                int count = Convert.ToInt32(ds.Tables[0].Rows.Count);
                                for (int i = 0; i < count; i++)
                                {
                                    var firstname = ds.Tables[0].Rows[i][0].ToString();
                                    var lastname = ds.Tables[0].Rows[i][1].ToString();
                                    var email = ds.Tables[0].Rows[i][2].ToString();
                                    var password = ds.Tables[0].Rows[i][3].ToString();
                                    var phone = ds.Tables[0].Rows[i][4].ToString();
                                    var username = ds.Tables[0].Rows[i][5].ToString();


                                    UserDto model = new UserDto
                                    {
                                        Firstname = firstname,
                                        Lastname = lastname,
                                        Email = email,
                                        Password = password,
                                        Phone = phone,
                                        Username = username,
                                        RoleId = new AccountService().GetRoleId("Student")
                                    };
                                    result = new AccountService().AddUser(model);

                                }
                                return Ok(result);
                            }
                            else
                            {
                                return BadRequest("You must upload excel sheets only");
                            }
                        }
                    }
                }
                return BadRequest("No File Uploaded");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }

        [HttpGet, Route("api/GetUserById/{id}")]
        public IHttpActionResult GetUserById([FromUri]string id)
        {
            var result = new AccountService().GetById(id);
            return Ok(result);
        }

        [HttpPost, Route("api/DeleteUser")]
        public IHttpActionResult DeleteUser([FromBody]Remove remove)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Fill Empty Records");
            }
            var result = new AccountService().Delete(remove.Id);
            return Ok(result);
        }



        [HttpPost, Route("api/DeleteListUsers")]
        public IHttpActionResult DeleteListUsers([FromBody]List<string> remove)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Fill Empty Records");
            }
            var result = new AccountService().DeleteAll(remove);
            return Ok(result);
        }


    }
}
