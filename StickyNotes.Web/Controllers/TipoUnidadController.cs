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
    public class TipoUnidadController : Controller
    {
        private StickyNotesEntities db = new StickyNotesEntities();

        // GET: TipoUnidads
        public ActionResult Index()
        {
            var tipoUnidad = db.TipoUnidad.Include(t => t.Estados);
            return View(tipoUnidad.ToList());
        }

        // GET: TipoUnidads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoUnidad tipoUnidad = db.TipoUnidad.Find(id);
            if (tipoUnidad == null)
            {
                return HttpNotFound();
            }
            return View(tipoUnidad);
        }

        // GET: TipoUnidads/Create
        public ActionResult Create()
        {
            ViewBag.idEstado = new SelectList(db.Estados, "idEstado", "estado");
            return View();
        }

        // POST: TipoUnidads/Create
  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idTipoUnidad,nombre,descripcion,idEstado")] TipoUnidad tipoUnidad)
        {
            if (ModelState.IsValid)
            {
                db.TipoUnidad.Add(tipoUnidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idEstado = new SelectList(db.Estados, "idEstado", "estado", tipoUnidad.idEstado);
            return View(tipoUnidad);
        }

        // GET: TipoUnidads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoUnidad tipoUnidad = db.TipoUnidad.Find(id);
            if (tipoUnidad == null)
            {
                return HttpNotFound();
            }
            ViewBag.idEstado = new SelectList(db.Estados, "idEstado", "estado", tipoUnidad.idEstado);
            return View(tipoUnidad);
        }

        // POST: TipoUnidads/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idTipoUnidad,nombre,descripcion,idEstado")] TipoUnidad tipoUnidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoUnidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idEstado = new SelectList(db.Estados, "idEstado", "estado", tipoUnidad.idEstado);
            return View(tipoUnidad);
        }

        // GET: TipoUnidads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoUnidad tipoUnidad = db.TipoUnidad.Find(id);
            if (tipoUnidad == null)
            {
                return HttpNotFound();
            }
            return View(tipoUnidad);
        }

        // POST: TipoUnidads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoUnidad tipoUnidad = db.TipoUnidad.Find(id);
            db.TipoUnidad.Remove(tipoUnidad);
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
