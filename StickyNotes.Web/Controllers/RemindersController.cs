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
    public class RemindersController : Controller
    {
        private StickyNotesEntities db = new StickyNotesEntities();

        // GET: Reminders
        public ActionResult Index()
        {
            var reminder = db.Reminder.Include(r => r.Estados).Include(r => r.Notas).Include(r => r.TipoUnidad).Include(r => r.TipoFrecuencia);
            return View(reminder.ToList());
        }

        // GET: Reminders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reminder reminder = db.Reminder.Find(id);
            if (reminder == null)
            {
                return HttpNotFound();
            }
            return View(reminder);
        }

        // GET: Reminders/Create
        public ActionResult Create()
        {
            ViewBag.idEstado = new SelectList(db.Estados, "idEstado", "estado");
            ViewBag.idNota = new SelectList(db.Notas, "idNota", "titulo");
            ViewBag.idTipoUnidad = new SelectList(db.TipoUnidad, "idTipoUnidad", "nombre");
            ViewBag.idTipoFrecuencia = new SelectList(db.TipoFrecuencia, "idTipoFrecuencia", "nombre");
            return View();
        }

        // POST: Reminders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idReminder,intervalo,idTipoUnidad,idTipoFrecuencia,idNota,idEstado")] Reminder reminder)
        {
            if (ModelState.IsValid)
            {
                db.Reminder.Add(reminder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idEstado = new SelectList(db.Estados, "idEstado", "estado", reminder.idEstado);
            ViewBag.idNota = new SelectList(db.Notas, "idNota", "titulo", reminder.idNota);
            ViewBag.idTipoUnidad = new SelectList(db.TipoUnidad, "idTipoUnidad", "nombre", reminder.idTipoUnidad);
            ViewBag.idTipoFrecuencia = new SelectList(db.TipoFrecuencia, "idTipoFrecuencia", "nombre", reminder.idTipoFrecuencia);
            return View(reminder);
        }

        // GET: Reminders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reminder reminder = db.Reminder.Find(id);
            if (reminder == null)
            {
                return HttpNotFound();
            }
            ViewBag.idEstado = new SelectList(db.Estados, "idEstado", "estado", reminder.idEstado);
            ViewBag.idNota = new SelectList(db.Notas, "idNota", "titulo", reminder.idNota);
            ViewBag.idTipoUnidad = new SelectList(db.TipoUnidad, "idTipoUnidad", "nombre", reminder.idTipoUnidad);
            ViewBag.idTipoFrecuencia = new SelectList(db.TipoFrecuencia, "idTipoFrecuencia", "nombre", reminder.idTipoFrecuencia);
            return View(reminder);
        }

        // POST: Reminders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idReminder,intervalo,idTipoUnidad,idTipoFrecuencia,idNota,idEstado")] Reminder reminder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reminder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idEstado = new SelectList(db.Estados, "idEstado", "estado", reminder.idEstado);
            ViewBag.idNota = new SelectList(db.Notas, "idNota", "titulo", reminder.idNota);
            ViewBag.idTipoUnidad = new SelectList(db.TipoUnidad, "idTipoUnidad", "nombre", reminder.idTipoUnidad);
            ViewBag.idTipoFrecuencia = new SelectList(db.TipoFrecuencia, "idTipoFrecuencia", "nombre", reminder.idTipoFrecuencia);
            return View(reminder);
        }

        // GET: Reminders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reminder reminder = db.Reminder.Find(id);
            if (reminder == null)
            {
                return HttpNotFound();
            }
            return View(reminder);
        }

        // POST: Reminders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reminder reminder = db.Reminder.Find(id);
            db.Reminder.Remove(reminder);
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
