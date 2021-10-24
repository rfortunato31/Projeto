using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Clima.Models;

namespace Clima.Controllers
{
    public class PrevisaoClimasController : Controller
    {
        private BaseEntities db = new BaseEntities();

        // GET: PrevisaoClimas
        public ActionResult Index()
        {
            return View(db.PevisaoClima.ToList());
        }

        // GET: PrevisaoClimas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PevisaoClima pevisaoClima = db.PevisaoClima.Find(id);
            if (pevisaoClima == null)
            {
                return HttpNotFound();
            }
            return View(pevisaoClima);
        }

        // GET: PrevisaoClimas/Create
        public ActionResult Create()
        {
            ViewBag.CidadeId = new SelectList(db.Cidade, "Id", "Nome");
            return View();
        }

        // POST: PrevisaoClimas/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Clima,CidadeId,DataPrevisao,TemperaturaMinima,TemperaturaMaxima")] PevisaoClima pevisaoClima)
        {
            if (ModelState.IsValid)
            {
                db.PevisaoClima.Add(pevisaoClima);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CidadeId = new SelectList(db.Cidade, "Id", "Nome", pevisaoClima.CidadeId);
            return View(pevisaoClima);
        }

        // GET: PrevisaoClimas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PevisaoClima pevisaoClima = db.PevisaoClima.Find(id);
            if (pevisaoClima == null)
            {
                return HttpNotFound();
            }
            ViewBag.CidadeId = new SelectList(db.Cidade, "Id", "Nome", pevisaoClima.CidadeId);
            return View(pevisaoClima);
        }

        // POST: PrevisaoClimas/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Clima,CidadeId,DataPrevisao,TemperaturaMinima,TemperaturaMaxima")] PevisaoClima pevisaoClima)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pevisaoClima).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CidadeId = new SelectList(db.Cidade, "Id", "Nome", pevisaoClima.CidadeId);
            return View(pevisaoClima);
        }

        // GET: PrevisaoClimas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PevisaoClima pevisaoClima = db.PevisaoClima.Find(id);
            if (pevisaoClima == null)
            {
                return HttpNotFound();
            }
            return View(pevisaoClima);
        }

        // POST: PrevisaoClimas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PevisaoClima pevisaoClima = db.PevisaoClima.Find(id);
            db.PevisaoClima.Remove(pevisaoClima);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
