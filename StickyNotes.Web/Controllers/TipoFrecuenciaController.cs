using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StickyNotes.Data;

namespace StickyNotes.Web.Controllers
{
    public class TipoFrecuenciaController : Controller
    {
        private StickyNotesEntities db = new StickyNotesEntities();

        // GET: TipoFrecuencias
        public ActionResult Index()
        {
            var tipoFrecuencia = db.TipoFrecuencia.Include(t => t.Estados);
            return View(tipoFrecuencia.ToList());
        }

        // GET: TipoFrecuencias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoFrecuencia tipoFrecuencia = db.TipoFrecuencia.Find(id);
            if (tipoFrecuencia == null)
            {
                return HttpNotFound();
            }
            return View(tipoFrecuencia);
        }

        // GET: TipoFrecuencias/Create
        public ActionResult Create()
        {
            ViewBag.idEstado = new SelectList(db.Estados, "idEstado", "estado");
            return View();
        }

        // POST: TipoFrecuencias/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idTipoFrecuencia,nombre,descripcion,idEstado")] TipoFrecuencia tipoFrecuencia)
        {
            if (ModelState.IsValid)
            {
                db.TipoFrecuencia.Add(tipoFrecuencia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idEstado = new SelectList(db.Estados, "idEstado", "estado", tipoFrecuencia.idEstado);
            return View(tipoFrecuencia);
        }

        // GET: TipoFrecuencias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoFrecuencia tipoFrecuencia = db.TipoFrecuencia.Find(id);
            if (tipoFrecuencia == null)
            {
                return HttpNotFound();
            }
            ViewBag.idEstado = new SelectList(db.Estados, "idEstado", "estado", tipoFrecuencia.idEstado);
            return View(tipoFrecuencia);
        }

        // POST: TipoFrecuencias/Edit/5
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idTipoFrecuencia,nombre,descripcion,idEstado")] TipoFrecuencia tipoFrecuencia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoFrecuencia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idEstado = new SelectList(db.Estados, "idEstado", "estado", tipoFrecuencia.idEstado);
            return View(tipoFrecuencia);
        }

        // GET: TipoFrecuencias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoFrecuencia tipoFrecuencia = db.TipoFrecuencia.Find(id);
            if (tipoFrecuencia == null)
            {
                return HttpNotFound();
            }
            return View(tipoFrecuencia);
        }

        // POST: TipoFrecuencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoFrecuencia tipoFrecuencia = db.TipoFrecuencia.Find(id);
            db.TipoFrecuencia.Remove(tipoFrecuencia);
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
