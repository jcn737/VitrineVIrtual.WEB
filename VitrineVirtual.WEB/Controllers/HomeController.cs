using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using VitrineVirtual.Data;
using VitrineVirtual.Model;

namespace VitrineVirtual.WEB.Controllers
{
    public class HomeController : Controller
    {
        private VitrineVirtualDBContext db = new VitrineVirtualDBContext();
        Logger log = LogManager.GetCurrentClassLogger();
        Page page = new Page();

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult MenuEmpresas()
        {
            var lstEmpresas = db.Empresas.Select(x => x.Nome_Fantasia).ToList();

            if (lstEmpresas.Any())
            {
                TempData["Lista_Empresas"] = lstEmpresas;
                return RedirectToAction("Index", lstEmpresas);
            }
            else
                return RedirectToAction("Index");


        }
    }
}