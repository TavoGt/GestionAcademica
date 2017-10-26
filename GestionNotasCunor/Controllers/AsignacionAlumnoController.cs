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
    public class AsignacionAlumnoController : Controller
    {
        private ctxNotasCunor db = new ctxNotasCunor();

        // GET: AsignacionAlumno
        public ActionResult Index()
        {
            var asign_alumno = db.asign_alumno.Include(a => a.alumno);
            return View(asign_alumno.ToList());
        }

        // GET: AsignacionAlumno/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asign_alumno asign_alumno = db.asign_alumno.Find(id);
            if (asign_alumno == null)
            {
                return HttpNotFound();
            }
            return View(asign_alumno);
        }

        // GET: AsignacionAlumno/Create
        public ActionResult Create()
        {
            ViewBag.id_alumno = new SelectList(db.alumno, "id_alumno", "carnet");
            ViewBag.id_carrera = new SelectList(db.carrera, "id_carrera", "nom_carrera");
            ViewBag.lista_carreras = db.carrera;

            return View();
        }

        public JsonResult busqCurso(int idCarrera) {
            var consulta = (from m in db.curso
                            select new { m.id_curso, m.nom_curso, m.no_ciclo, m.cod_curso}).ToList();

            return Json(consulta, JsonRequestBehavior.AllowGet);
        }

        // POST: AsignacionAlumno/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_asign_alumno,id_alumno,ciclo,anio,fecha")] asign_alumno asign_alumno)
        {
            if (ModelState.IsValid)
            {
                db.asign_alumno.Add(asign_alumno);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_alumno = new SelectList(db.alumno, "id_alumno", "carnet", asign_alumno.id_alumno);
            return View(asign_alumno);
        }

        // GET: AsignacionAlumno/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asign_alumno asign_alumno = db.asign_alumno.Find(id);
            if (asign_alumno == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_alumno = new SelectList(db.alumno, "id_alumno", "carnet", asign_alumno.id_alumno);
            return View(asign_alumno);
        }

        // POST: AsignacionAlumno/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_asign_alumno,id_alumno,ciclo,anio,fecha")] asign_alumno asign_alumno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asign_alumno).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_alumno = new SelectList(db.alumno, "id_alumno", "carnet", asign_alumno.id_alumno);
            return View(asign_alumno);
        }

        // GET: AsignacionAlumno/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asign_alumno asign_alumno = db.asign_alumno.Find(id);
            if (asign_alumno == null)
            {
                return HttpNotFound();
            }
            return View(asign_alumno);
        }

        // POST: AsignacionAlumno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            asign_alumno asign_alumno = db.asign_alumno.Find(id);
            db.asign_alumno.Remove(asign_alumno);
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
