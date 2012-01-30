using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.IO;

namespace Slidr.net.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        [Authorize]
        public ActionResult StartSlideshow()
        {
            return View();
        }

        public ActionResult SlideNumber(int id, int width, int height)
        {
            string sourceLocation = Server.MapPath(string.Format("/Content/Images/Slide{0}.png", id));

            if (System.IO.File.Exists(sourceLocation))
            {
                using (var sourceBitmap = Bitmap.FromFile(sourceLocation))
                {
                    return new ImageResult(sourceBitmap, width, height);
                }
            }
            else
            {
                return new HttpNotFoundResult();
            }
        }
    }
}
