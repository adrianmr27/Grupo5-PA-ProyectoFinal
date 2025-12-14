using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StickyNotes.Data;
using Microsoft.AspNet.Identity;

namespace StickyNotes.Web.Controllers
{
    public class NotasController : Controller
    {
        private StickyNotesEntities db = new StickyNotesEntities();

        // Obtener usuario en sesión
        private int GetUsuarioId()
        {
            var email = User.Identity.GetUserName();

            if (email == null)
                throw new Exception("No hay usuario logueado.");

            var usuario = db.Usuarios.FirstOrDefault(u => u.correo == email);

            if (usuario == null)
                throw new Exception("Usuario no existe en la BD");

            return usuario.idUsuario;
        }

        // ==============================
        // LISTADO DE NOTAS
        // ==============================
        [Authorize]
        public ActionResult Index()
        {
            int idUsuario = GetUsuarioId();

            // Excluir notas con estado Inactivo (idEstado = 2) de "Mis Notas"
            var notasFijadas = db.Notas
                .Where(n => n.fijada && n.idUsuario == idUsuario && (n.idEstado == null || n.idEstado != 2))
                .Include(n => n.Categorias)
                .Include(n => n.Estados)
                .ToList();

            var notasNoFijadas = db.Notas
                .Where(n => !n.fijada && n.idUsuario == idUsuario && (n.idEstado == null || n.idEstado != 2))
                .Include(n => n.Categorias)
                .Include(n => n.Estados)
                .ToList();

            ViewBag.NotasFijadas = notasFijadas;
            ViewBag.IdEstadoCompletado = 2; // Para mostrar/ocultar el botón
            return View(notasNoFijadas);
        }

        // ==============================
        // DETALLES
        // ==============================
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            int idUsuario = GetUsuarioId();

            var nota = db.Notas
                .Include(n => n.Categorias)
                .Include(n => n.Estados)
                .FirstOrDefault(n => n.idNota == id && n.idUsuario == idUsuario);

            if (nota == null)
                return HttpNotFound();

            return View(nota);
        }

        // ==============================
        // CREAR
        // ==============================
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.idCategoria = new SelectList(db.Categorias, "idCategoria", "nombre");
            ViewBag.idEstado = new SelectList(db.Estados, "idEstado", "estado");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idNota,titulo,contenido,color,idCategoria,idEstado,fijada")] Notas notas)
        {
            if (ModelState.IsValid)
            {
                notas.idUsuario = GetUsuarioId();
                notas.fechaCreacion = DateTime.Now;

                db.Notas.Add(notas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCategoria = new SelectList(db.Categorias, "idCategoria", "nombre", notas.idCategoria);
            ViewBag.idEstado = new SelectList(db.Estados, "idEstado", "estado", notas.idEstado);

            return View(notas);
        }

        // ==============================
        // EDITAR
        // ==============================
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            int idUsuario = GetUsuarioId();

            var notas = db.Notas.FirstOrDefault(n => n.idNota == id && n.idUsuario == idUsuario);

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
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            int idUsuario = GetUsuarioId();

            var nota = db.Notas
                .FirstOrDefault(n => n.idNota == id && n.idUsuario == idUsuario);

            if (nota == null)
                return HttpNotFound();

            return View(nota);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            int idUsuario = GetUsuarioId();

            var nota = db.Notas
                .FirstOrDefault(n => n.idNota == id && n.idUsuario == idUsuario);

            if (nota == null)
                return HttpNotFound();

            db.Notas.Remove(nota);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // ==============================
        // FIJAR / DESFIJAR (PIN)
        // ==============================
        [Authorize]
        [HttpPost]
        public ActionResult TogglePin(int id)
        {
            int idUsuario = GetUsuarioId();

            var nota = db.Notas
                .FirstOrDefault(n => n.idNota == id && n.idUsuario == idUsuario);

            if (nota == null)
                return HttpNotFound();

            nota.fijada = !nota.fijada;
            nota.fechaModificacion = DateTime.Now;

            db.SaveChanges();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        // ==============================
        // NOTAS FIJADAS
        // ==============================
        [Authorize]
        public ActionResult Pinned()
        {
            int idUsuario = GetUsuarioId();

            var notasFijadas = db.Notas
                .Where(n => n.fijada && n.idUsuario == idUsuario)
                .Include(n => n.Categorias)
                .Include(n => n.Estados)
                .ToList();

            return View(notasFijadas);
        }

        // ==============================
        // MARCAR COMO COMPLETADA
        // ==============================
        [Authorize]
        [HttpPost]
        public ActionResult ToggleComplete(int id)
        {
            int idUsuario = GetUsuarioId();

            var nota = db.Notas
                .FirstOrDefault(n => n.idNota == id && n.idUsuario == idUsuario);

            if (nota == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            // Actualizar la nota a estado Inactivo (idEstado = 2)
            nota.idEstado = 2; // Inactivo
            nota.fechaModificacion = DateTime.Now;

            db.SaveChanges();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        // ==============================
        // NOTAS COMPLETADAS
        // ==============================
        [Authorize]
        public ActionResult Completed(string search = "")
        {
            int idUsuario = GetUsuarioId();

            // Obtener todas las notas con estado Inactivo (idEstado = 2) del usuario
            var query = db.Notas
                .Where(n => n.idEstado == 2 && n.idUsuario == idUsuario)
                .Include(n => n.Categorias)
                .Include(n => n.Estados);

            // Filtrar por búsqueda si se proporciona
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(n => 
                    (n.titulo != null && n.titulo.Contains(search)) || 
                    (n.contenido != null && n.contenido.Contains(search)) ||
                    (n.Categorias != null && n.Categorias.nombre != null && n.Categorias.nombre.Contains(search))
                );
            }

            // Ordenar después del filtro
            var notasCompletadas = query.OrderByDescending(n => n.fechaModificacion ?? n.fechaCreacion ?? DateTime.MinValue);

            ViewBag.SearchTerm = search;
            return View(notasCompletadas.ToList());
        }
    }
}