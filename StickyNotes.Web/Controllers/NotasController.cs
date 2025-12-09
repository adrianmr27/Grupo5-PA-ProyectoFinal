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
    public class NotasController : Controller
    {
        private StickyNotesEntities db = new StickyNotesEntities();

        // GET: Notas
        public ActionResult Index()
        {
            var notas = db.Notas.Include(n => n.Categorias).Include(n => n.Estados).Include(n => n.Usuarios);
            return View(notas.ToList());
        }

        // GET: Notas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var notas = db.Notas
                .Include(n => n.Categorias)
                .Include(n => n.Estados)
                .Include(n => n.Usuarios)
                .FirstOrDefault(n => n.idNota == id);

            if (notas == null)
            {
                return HttpNotFound();
            }
            return View(notas);
        }


        // GET: Notas/Create
        public ActionResult Create()
        {
            ViewBag.idCategoria = new SelectList(db.Categorias, "idCategoria", "nombre");
            ViewBag.idEstado = new SelectList(db.Estados, "idEstado", "estado");
            ViewBag.idUsuario = new SelectList(db.Usuarios, "idUsuario", "nombre");
            return View();
        }

        // POST: Notas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idNota,titulo,contenido,color,idUsuario,idCategoria,idEstado")] Notas notas)
        {
            if (ModelState.IsValid)
            {
                db.Notas.Add(notas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCategoria = new SelectList(db.Categorias, "idCategoria", "nombre", notas.idCategoria);
            ViewBag.idEstado = new SelectList(db.Estados, "idEstado", "estado", notas.idEstado);
            ViewBag.idUsuario = new SelectList(db.Usuarios, "idUsuario", "nombre", notas.idUsuario);
            return View(notas);
        }

        // GET: Notas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notas notas = db.Notas.Find(id);
            if (notas == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCategoria = new SelectList(db.Categorias, "idCategoria", "nombre", notas.idCategoria);
            ViewBag.idEstado = new SelectList(db.Estados, "idEstado", "estado", notas.idEstado);
            ViewBag.idUsuario = new SelectList(db.Usuarios, "idUsuario", "nombre", notas.idUsuario);
            return View(notas);
        }

        // POST: Notas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idNota,titulo,contenido,color,fechaCreacion,fechaModificacion,idUsuario,idCategoria,idEstado")] Notas notas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCategoria = new SelectList(db.Categorias, "idCategoria", "nombre", notas.idCategoria);
            ViewBag.idEstado = new SelectList(db.Estados, "idEstado", "estado", notas.idEstado);
            ViewBag.idUsuario = new SelectList(db.Usuarios, "idUsuario", "nombre", notas.idUsuario);
            return View(notas);
        }

        // GET: Notas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var notas = db.Notas
                .Include(n => n.Categorias)
                .Include(n => n.Estados)
                .Include(n => n.Usuarios)
                .FirstOrDefault(n => n.idNota == id);

            if (notas == null)
            {
                return HttpNotFound();
            }
            return View(notas);
        }


        //// POST: Notas/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Notas notas = db.Notas.Find(id);
        //    db.Notas.Remove(notas);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var nota = db.Notas.Find(id);
                if (nota == null)
                {
                    return HttpNotFound();
                }

                db.Notas.Remove(nota);
                db.SaveChanges();

                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }



    }
}
