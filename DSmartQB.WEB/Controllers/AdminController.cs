using DSmartQB.CORE.Services;
using System.Web.Security;
using System.Web.Mvc;
using DSmartQB.WEB.Models;

namespace DSmartQB.WEB.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Access(LoginVM model)
        {
            AccountService _account = new AccountService();

            var user = _account.CheckUser(model.Username, model.Password);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(model.Username, false);

            }

            return View("Dashboard");
        }

        [Authorize(Roles = "Administrator,Teacher")]
        public ActionResult Dashboard()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Group()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Teacher()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Student()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Subject()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult CongitiveLevel()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult CreateSubject()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult EditSubject()
        {
            return View();
        }

        [Authorize(Roles = "Administrator,Teacher")]
        public ActionResult Item()
        {
            return View();
        }

        [Authorize(Roles = "Administrator,Teacher")]
        public ActionResult CreateItem()
        {
            return View();
        }

        [Authorize(Roles = "Administrator,Teacher")]
        public ActionResult MCQ()
        {
            return View();
        }

        [Authorize(Roles = "Administrator,Teacher")]
        public ActionResult MRQ()
        {
            return View();
        }

        [Authorize(Roles = "Administrator,Teacher")]
        public ActionResult TF()
        {
            return View();
        }

        [Authorize(Roles = "Administrator,Teacher")]
        public ActionResult Exam()
        {
            return View();
        }

        [Authorize(Roles = "Administrator,Teacher")]
        public ActionResult CreateExam()
        {
            return View();
        }


        [Authorize(Roles = "Administrator,Teacher")]
        public ActionResult CreateMannual()
        {
            return View();
        }
        
        [Authorize(Roles = "Administrator,Teacher")]
        public ActionResult University()
        {
            return View();
        }

        [Authorize(Roles = "Administrator,Teacher")]
        public ActionResult OnlineStudents()
        {
            return View();
        }


        [Authorize(Roles = "Administrator,Teacher")]
        public ActionResult CreateBluePrint()
        {
            return View();
        }

        [Authorize(Roles = "Administrator,Teacher")]
        public ActionResult Preview()
        {
            return View();
        }

        [Authorize(Roles = "Administrator,Teacher")]
        public ActionResult PaperSheetAr()
        {
            return View();
        }
        

        [Authorize(Roles = "Administrator,Teacher")]
        public ActionResult PaperSheetEn()
        {
            return View();
        }
        

        [Authorize(Roles = "Administrator,Teacher")]
        public ActionResult Report()
        {
            return View();
        }
    }
}