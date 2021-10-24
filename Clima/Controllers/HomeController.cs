using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Clima.Models;
using System.Web.UI.WebControls;
using System.Data.Entity.SqlServer;

namespace Clima.Controllers
{
    public class HomeController : Controller
    {

        private BaseEntities db = new BaseEntities();
        
        public ActionResult Index()
        {
            ViewBag.CidadeId = new SelectList(db.Cidade, "Id", "Nome");

            TempMin();

            TempMax();


            return View();
        }

        public void TempMin()
        {
            GridView gViewMin = new GridView();
            var tempMin = (from p in db.PevisaoClima
                           join c in db.Cidade on p.CidadeId equals c.Id
                           join e in db.Estado on c.EstadoId equals e.Id
                           orderby p.TemperaturaMinima ascending
                           where p.DataPrevisao == DateTime.Today
                           select new { c.Nome, p.TemperaturaMinima, p.TemperaturaMaxima }).Take(3);

            gViewMin.DataSource = tempMin.ToList();
            gViewMin.DataBind();
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                using (System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw))
                {
                    gViewMin.RenderControl(htw);
                    ViewBag.GridViewStringMin = sw.ToString();
                }
            }
        }

        public void TempMax()
        {
            GridView gViewMax = new GridView();
            var tempMax = (from p in db.PevisaoClima
                           join c in db.Cidade on p.CidadeId equals c.Id
                           join e in db.Estado on c.EstadoId equals e.Id
                           orderby p.TemperaturaMaxima descending
                           where p.DataPrevisao == DateTime.Today
                           select new { c.Nome, p.TemperaturaMinima, p.TemperaturaMaxima }).Take(3);

            gViewMax.DataSource = tempMax.ToList();
            gViewMax.DataBind();
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                using (System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw))
                {
                    gViewMax.RenderControl(htw);
                    ViewBag.GridViewStringMax = sw.ToString();
                }
            }
        }


        [HttpPost]
        public ActionResult Index([Bind(Include = "CidadeId")] PevisaoClima pevisaoClima)
        {
            ViewBag.CidadeId = new SelectList(db.Cidade, "Id", "Nome");

            TempMin();

            TempMax();

            GridView gViewPrevisao = new GridView();
            var sevenDaysAgo = DateTime.Now.AddDays(-7);
            var tempPrevisao = from p in db.PevisaoClima
                               join c in db.Cidade on p.CidadeId equals c.Id
                               join e in db.Estado on c.EstadoId equals e.Id
                               orderby p.DataPrevisao ascending
                               where p.CidadeId == pevisaoClima.CidadeId && sevenDaysAgo <= p.DataPrevisao
                               select new { c.Nome, e.Uf, p.DataPrevisao, p.Clima, p.TemperaturaMinima, p.TemperaturaMaxima };

            gViewPrevisao.DataSource = tempPrevisao.ToList();
            gViewPrevisao.DataBind();
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                using (System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw))
                {
                    gViewPrevisao.RenderControl(htw);
                    ViewBag.GridViewStringPrev = sw.ToString();
                }
            }

            return View();

            
        }
        public ActionResult About()
        {
            return View();
        }

    }
}