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

        // ==============================
        // LISTADO DE NOTAS
        // ==============================
        public ActionResult Index()
        {
            var notasFijadas = db.Notas
                .Where(n => n.fijada)
                .Include(n => n.Categorias)
                .Include(n => n.Estados)
                .Include(n => n.Usuarios)
                .ToList();

            var notasNoFijadas = db.Notas
                .Where(n => !n.fijada)
                .Include(n => n.Categorias)
                .Include(n => n.Estados)
                .Include(n => n.Usuarios)
                .ToList();

            ViewBag.NotasFijadas = notasFijadas;

            return View(notasNoFijadas);
        }

        // ==============================
        // DETALLES
        // ==============================
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var notas = db.Notas
                .Include(n => n.Categorias)
                .Include(n => n.Estados)
                .Include(n => n.Usuarios)
                .FirstOrDefault(n => n.idNota == id);

            if (notas == null)
                return HttpNotFound();

            return View(notas);
        }

        // ==============================
        // CREAR
        // ==============================
        public ActionResult Create()
        {
            ViewBag.idCategoria = new SelectList(db.Categorias, "idCategoria", "nombre");
            ViewBag.idEstado = new SelectList(db.Estados, "idEstado", "estado");
            ViewBag.idUsuario = new SelectList(db.Usuarios, "idUsuario", "nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idNota,titulo,contenido,color,idUsuario,idCategoria,idEstado,fijada")] Notas notas)
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

        // ==============================
        // EDITAR
        // ==============================
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var notas = db.Notas.Find(id);
            if (notas == null)
                return HttpNotFound();

            ViewBag.idCategoria = new SelectList(db.Categorias, "idCategoria", "nombre", notas.idCategoria);
            ViewBag.idEstado = new SelectList(db.Estados, "idEstado", "estado", notas.idEstado);
            ViewBag.idUsuario = new SelectList(db.Usuarios, "idUsuario", "nombre", notas.idUsuario);

            return View(notas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idNota,titulo,contenido,color,fechaCreacion,fechaModificacion,idUsuario,idCategoria,idEstado,fijada")] Notas notas)
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

        // ==============================
        // ELIMINAR
        // ==============================
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var notas = db.Notas
                .Include(n => n.Categorias)
                .Include(n => n.Estados)
                .Include(n => n.Usuarios)
                .FirstOrDefault(n => n.idNota == id);

            if (notas == null)
                return HttpNotFound();

            return View(notas);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var nota = db.Notas.Find(id);
                if (nota == null)
                    return HttpNotFound();

                db.Notas.Remove(nota);
                db.SaveChanges();

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // ==============================
        // FIJAR / DESFIJAR (PIN)
        // ==============================
        [HttpPost]
        public ActionResult TogglePin(int id)
        {
            var nota = db.Notas.Find(id);

            if (nota == null)
                return HttpNotFound();

            nota.fijada = !nota.fijada;
            db.SaveChanges();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        // ==============================
        // NOTAS FIJADAS
        // ==============================
        public ActionResult Pinned()
        {
            var notasFijadas = db.Notas
                .Where(n => n.fijada)
                .Include(n => n.Categorias)
                .Include(n => n.Estados)
                .Include(n => n.Usuarios)
                .ToList();

            return View(notasFijadas);
        }
    }
}
