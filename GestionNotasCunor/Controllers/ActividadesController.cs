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
    public class ActividadesController : Controller
    {
        private ctxNotasCunor db = new ctxNotasCunor();

        // GET: Actividades
        public ActionResult Index()
        {
            var actividad = db.actividad.Include(a => a.asign_curso);
            return View(actividad.ToList());
        }

        // GET: Actividades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            actividad actividad = db.actividad.Find(id);
            if (actividad == null)
            {
                return HttpNotFound();
            }
            return View(actividad);
        }

        // GET: Actividades/Create
        public ActionResult Create()
        {
            var curso = from a in db.asign_curso
                        join c in db.curso on a.id_curso equals c.id_curso
                        select new { id_asign_curso = a.id_asign_curso, nom_curso = c.nom_curso };

            ViewBag.id_asign_curso = new SelectList(curso, "id_asign_curso", "nom_curso");
            return View();
        }

        // POST: Actividades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_actividad,nom_actividad,valor,fecha,id_asign_curso")] actividad actividad)
        {
            if (ModelState.IsValid)
            {
                db.actividad.Add(actividad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_asign_curso = new SelectList(db.asign_curso, "id_asign_curso", "seccion", actividad.id_asign_curso);
            return View(actividad);
        }

        // GET: Actividades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            actividad actividad = db.actividad.Find(id);
            if (actividad == null)
            {
                return HttpNotFound();
            }
            var curso = from a in db.asign_curso
                        join c in db.curso on a.id_curso equals c.id_curso
                        select new { id_asign_curso = a.id_asign_curso, nom_curso = c.nom_curso };

            ViewBag.id_asign_curso = new SelectList(curso, "id_asign_curso", "nom_curso");

            //ViewBag.id_asign_curso = new SelectList(db.asign_curso, "id_asign_curso", "seccion", actividad.id_asign_curso);
            return View(actividad);
        }

        // POST: Actividades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_actividad,nom_actividad,valor,fecha,id_asign_curso")] actividad actividad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(actividad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_asign_curso = new SelectList(db.asign_curso, "id_asign_curso", "seccion", actividad.id_asign_curso);
            return View(actividad);
        }

        // GET: Actividades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            actividad actividad = db.actividad.Find(id);
            if (actividad == null)
            {
                return HttpNotFound();
            }
            return View(actividad);
        }

        // POST: Actividades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            actividad actividad = db.actividad.Find(id);
            db.actividad.Remove(actividad);
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
