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
            var catedratico = from cat in db.catedratico
                              select new { id_catedratico = cat.id_catedratico, nom_catedratico = cat.nombres + " " + cat.apellidos};

            var curso = from a in db.asign_curso
                        join c in db.curso on a.id_curso equals c.id_curso
                        select new { id_asign_curso = a.id_asign_curso, nom_curso = c.nom_curso };

            var carrera = from c in db.carrera
                          select new { id_carrera = c.id_carrera, nom_carrera = c.nom_carrera};

            ViewBag.lista_catedraticos = new SelectList(catedratico, "id_catedratico", "nom_catedratico");
            ViewBag.lista_carreras = new SelectList(carrera, "id_carrera", "nom_carrera");
            ViewBag.id_asign_curso = new SelectList(curso, "id_asign_curso", "nom_curso");
            return View();
        }

        public JsonResult busqCarrera(int idCatedratico)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var consulta = (from a in db.asign_curso
                            join c in db.carrera on a.id_carrera equals c.id_carrera
                            where a.id_catedratico == idCatedratico //Id de catedrático según el dropdownlist principal
                            group c by new {id_carrera =  c.id_carrera, nom_carrera =  c.nom_carrera} into carr
                            select new { id_carrera = carr.Key.id_carrera, nom_carrera = carr.Key.nom_carrera}).ToList();

            return Json(consulta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult busqCurso(int idCatedratico, int idCarrera)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var consulta = (from a in db.asign_curso
                            join c in db.curso on a.id_curso equals c.id_curso
                            where a.id_catedratico == idCatedratico && a.id_carrera == idCarrera //Id de carrera según el dropdownlist principal
                            select new { id_asign_curso = a.id_asign_curso, nom_curso = c.nom_curso }).ToList();

            return Json(consulta, JsonRequestBehavior.AllowGet);
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
