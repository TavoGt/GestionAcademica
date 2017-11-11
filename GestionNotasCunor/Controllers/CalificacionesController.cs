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
    public class CalificacionesController : Controller
    {
        private ctxNotasCunor db = new ctxNotasCunor();

        // GET: Calificaciones
        public ActionResult Index()
        {
            var calificacion = db.calificacion.Include(c => c.actividad).Include(c => c.alumno);
            return View(calificacion.ToList());
        }

        // GET: Calificaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            calificacion calificacion = db.calificacion.Find(id);
            if (calificacion == null)
            {
                return HttpNotFound();
            }
            return View(calificacion);
        }

        // GET: Calificaciones/Create
        public ActionResult Create()
        {
            var activ = from a in db.actividad
                        select new { id_actividad = a.id_actividad, nom_actividad = a.nom_actividad + " (" +a.asign_curso.curso.nom_curso+" "+ a.asign_curso.carrera.nom_carrera+") " };

            //ViewBag.id_actividad = new SelectList(db.actividad, "id_actividad", "nom_actividad");
            ViewBag.id_actividad = new SelectList(activ, "id_actividad", "nom_actividad");
            ViewBag.id_alumnno = new SelectList(db.alumno, "id_alumno", "carnet");
            return View();
        }

        // POST: Calificaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_calificacion,id_actividad,cal_obtenida,id_alumnno")] calificacion calificacion)
        {
            if (ModelState.IsValid)
            {
                db.calificacion.Add(calificacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_actividad = new SelectList(db.actividad, "id_actividad", "nom_actividad", calificacion.id_actividad);
            ViewBag.id_alumnno = new SelectList(db.alumno, "id_alumno", "carnet", calificacion.id_alumnno);
            return View(calificacion);
        }

        // GET: Calificaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            var activ = from a in db.actividad
                        select new { id_actividad = a.id_actividad, nom_actividad = a.nom_actividad + " (" + a.asign_curso.curso.nom_curso + " " + a.asign_curso.carrera.nom_carrera + ") " };

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            calificacion calificacion = db.calificacion.Find(id);
            if (calificacion == null)
            {
                return HttpNotFound();
            }
            //ViewBag.id_actividad = new SelectList(db.actividad, "id_actividad", "nom_actividad", calificacion.id_actividad);
            ViewBag.id_actividad = new SelectList(activ, "id_actividad", "nom_actividad", calificacion.id_actividad);
            ViewBag.id_alumnno = new SelectList(db.alumno, "id_alumno", "carnet", calificacion.id_alumnno);
            return View(calificacion);
        }

        // POST: Calificaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_calificacion,id_actividad,cal_obtenida,id_alumnno")] calificacion calificacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(calificacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_actividad = new SelectList(db.actividad, "id_actividad", "nom_actividad", calificacion.id_actividad);
            ViewBag.id_alumnno = new SelectList(db.alumno, "id_alumno", "carnet", calificacion.id_alumnno);
            return View(calificacion);
        }

        // GET: Calificaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            calificacion calificacion = db.calificacion.Find(id);
            if (calificacion == null)
            {
                return HttpNotFound();
            }
            return View(calificacion);
        }

        // POST: Calificaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            calificacion calificacion = db.calificacion.Find(id);
            db.calificacion.Remove(calificacion);
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
