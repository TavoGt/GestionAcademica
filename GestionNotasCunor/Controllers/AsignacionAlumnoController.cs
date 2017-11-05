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
        int codCarrera = 0;

        // GET: AsignacionAlumno
        public ActionResult Index()
        {
            var alumno = from m in db.alumno
                       select new { id_alumno = m.id_alumno, nombres = m.nombres + " " + m.apellidos };
            ViewBag.alumno = alumno;

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

            //Consulta para mostrar cursos por carrera
            var buscarCurso = (from a in db.asign_curso
                              join c in db.curso on a.id_curso equals c.id_curso
                              where a.id_carrera == codCarrera //Id de carrera según el dropdownlist principal
                              select new { id_asign_curso = a.id_asign_curso, nom_curso = c.nom_curso}).ToList();
            
            ViewBag.buscarCurso = new SelectList(buscarCurso, "id_asign_curso", "nom_curso");
            ViewBag.lista_cursos = buscarCurso.ToList();
            ViewBag.lista_carreras = db.carrera;
            return View();
        }

        public JsonResult busqCurso(int idCarrera) {
            codCarrera = idCarrera;
            db.Configuration.ProxyCreationEnabled = false;
            var consulta = (from a in db.asign_curso
                            join c in db.curso on a.id_curso equals c.id_curso
                            where a.id_carrera == idCarrera //Id de carrera según el dropdownlist principal
                            select new { id_asign_curso = a.id_asign_curso, nom_curso = c.nom_curso }).ToList();

            return Json(consulta, JsonRequestBehavior.AllowGet);
        }

        // POST: AsignacionAlumno/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(/*[Bind(Include = "id_asign_alumno,id_alumno,ciclo,anio,fecha")]*/ asign_alumno asign_alumno)
        {
            if (ModelState.IsValid)
            {
                asign_alumno.anio = DateTime.Today.Year; //LLenando el año de forma automática
                asign_alumno.fecha = DateTime.Today; //Llenando la fecha de forma automática
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
