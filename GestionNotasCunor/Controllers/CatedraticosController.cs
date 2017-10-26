using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestionNotasCunor.Models;

namespace GestionNotasCunor.Controllers
{
    public class CatedraticosController : Controller
    {
        private ctxNotasCunor db = new ctxNotasCunor();

        // GET: Catedraticos
        public ActionResult Index()
        {
            var catedratico = db.catedratico.Include(c => c.sexo);
            return View(catedratico.ToList());
        }

        // GET: Catedraticos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catedratico catedratico = db.catedratico.Find(id);
            if (catedratico == null)
            {
                return HttpNotFound();
            }
            return View(catedratico);
        }

        // GET: Catedraticos/Create
        public ActionResult Create()
        {

            ViewBag.id_sexo = new SelectList(db.sexo, "id_sexo", "nom_sexo");
            return View();
        }

        // POST: Catedraticos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_catedratico,colegiado,nombres,apellidos,id_sexo,telefono,email,profesion")] catedratico catedratico)
        {
            if (ModelState.IsValid)
            {
                db.catedratico.Add(catedratico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_sexo = new SelectList(db.sexo, "id_sexo", "nom_sexo", catedratico.id_sexo);
            return View(catedratico);
        }

        // GET: Catedraticos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catedratico catedratico = db.catedratico.Find(id);
            if (catedratico == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_sexo = new SelectList(db.sexo, "id_sexo", "nom_sexo", catedratico.id_sexo);
            return View(catedratico);
        }

        // POST: Catedraticos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_catedratico,colegiado,nombres,apellidos,id_sexo,telefono,email,profesion")] catedratico catedratico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(catedratico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_sexo = new SelectList(db.sexo, "id_sexo", "nom_sexo", catedratico.id_sexo);
            return View(catedratico);
        }

        // GET: Catedraticos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catedratico catedratico = db.catedratico.Find(id);
            if (catedratico == null)
            {
                return HttpNotFound();
            }
            return View(catedratico);
        }

        // POST: Catedraticos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            catedratico catedratico = db.catedratico.Find(id);
            db.catedratico.Remove(catedratico);
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
