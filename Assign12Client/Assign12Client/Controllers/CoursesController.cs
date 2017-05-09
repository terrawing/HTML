using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Assign12Client.Controllers
{
    public class CoursesController : Controller
    {
        private Manager m = new Manager();

        // GET: Courses
        /* Right click the Index() and create view */
        public async Task<ActionResult> Index()
        {
            var fetchedObject = await m.CoursesGetAll();

            return View(fetchedObject);
        }

        // GET: Courses/Details/DPS907
        public ActionResult Details(string courseCode)
        {
            var fetchedObject = m.CallSoapXML(courseCode);

            if(fetchedObject == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(fetchedObject);
            }
        }
    }
}