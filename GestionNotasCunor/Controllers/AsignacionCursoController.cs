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
    public class AsignacionCursoController : Controller
    {
        private ctxNotasCunor db = new ctxNotasCunor();

        // GET: AsignacionCurso
        public ActionResult Index()
        {
            var asign_curso = db.asign_curso.Include(a => a.carrera).Include(a => a.catedratico).Include(a => a.curso);
            return View(asign_curso.ToList());
        }

        // GET: AsignacionCurso/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asign_curso asign_curso = db.asign_curso.Find(id);
            if (asign_curso == null)
            {
                return HttpNotFound();
            }
            return View(asign_curso);
        }

        // GET: AsignacionCurso/Create
        public ActionResult Create()
        {
            ViewBag.id_carrera = new SelectList(db.carrera, "id_carrera", "nom_carrera");

            var cats = from m in db.catedratico
                       select new { id_catedratico = m.id_catedratico, nombres = m.nombres + " " + m.apellidos };

            ViewBag.id_catedratico = new SelectList(cats, "id_catedratico", "nombres");
            ViewBag.id_curso = new SelectList(db.curso, "id_curso", "nom_curso");
            return View();
        }

        // POST: AsignacionCurso/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_carrera,id_curso,seccion,id_catedratico,salon")] asign_curso asign_curso)
        {
            if (ModelState.IsValid)
            {
                db.asign_curso.Add(asign_curso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_carrera = new SelectList(db.carrera, "id_carrera", "nom_carrera", asign_curso.id_carrera);
            ViewBag.id_catedratico = new SelectList(db.catedratico, "id_catedratico", "nombres", asign_curso.id_catedratico);
            ViewBag.id_curso = new SelectList(db.curso, "id_curso", "nom_curso", asign_curso.id_curso);
            return View(asign_curso);
        }

        // GET: AsignacionCurso/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asign_curso asign_curso = db.asign_curso.Find(id);
            if (asign_curso == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_carrera = new SelectList(db.carrera, "id_carrera", "nom_carrera", asign_curso.id_carrera);
            ViewBag.id_catedratico = new SelectList(db.catedratico, "id_catedratico", "colegiado", asign_curso.id_catedratico);
            ViewBag.id_curso = new SelectList(db.curso, "id_curso", "cod_curso", asign_curso.id_curso);
            return View(asign_curso);
        }

        // POST: AsignacionCurso/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_carrera,id_curso,seccion,id_catedratico,salon")] asign_curso asign_curso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asign_curso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_carrera = new SelectList(db.carrera, "id_carrera", "nom_carrera", asign_curso.id_carrera);
            ViewBag.id_catedratico = new SelectList(db.catedratico, "id_catedratico", "colegiado", asign_curso.id_catedratico);
            ViewBag.id_curso = new SelectList(db.curso, "id_curso", "cod_curso", asign_curso.id_curso);
            return View(asign_curso);
        }

        // GET: AsignacionCurso/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asign_curso asign_curso = db.asign_curso.Find(id);
            if (asign_curso == null)
            {
                return HttpNotFound();
            }
            return View(asign_curso);
        }

        // POST: AsignacionCurso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            asign_curso asign_curso = db.asign_curso.Find(id);
            db.asign_curso.Remove(asign_curso);
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
