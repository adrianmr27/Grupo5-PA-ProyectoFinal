using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using StickyNotes.Core;
using StickyNotes.Data;

namespace StickyNotes.Web.Controllers
{
    public class UsuariosController : ControllerBase
    {

        // GET: Usuarios
        public ActionResult Index()
        {
            var usuarios = UsuariosBusiness.GetUsuarios(id:0);
            return View(usuarios.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usuarios = UsuariosBusiness.GetUsuarios((int)id);

            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewEstados();
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idUsuario,nombre,primerApellido,segundoApellido,nombreUsuario,contrasena,correo,fechaNacimiento,fechaRegistro,idEstado")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                UsuariosBusiness.SaveOrUpdate(usuarios);
                //db.Usuarios.Add(usuarios);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewEstados();
            return View(usuarios);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usuarios = UsuariosBusiness.GetUsuarioById((int)id);

            if (usuarios == null)
            {
                return HttpNotFound();
            }

            ViewEstados();
            return View(usuarios);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idUsuario,nombre,primerApellido,segundoApellido,nombreUsuario,contrasena,correo,fechaNacimiento,fechaRegistro,idEstado")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                UsuariosBusiness.SaveOrUpdate(usuarios);
                //db.Entry(usuarios).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewEstados();
            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usuarios = UsuariosBusiness.GetUsuarioById((int)id);

            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UsuariosBusiness.Delete(id);
            //Usuarios usuarios = db.Usuarios.Find(id);
            //db.Usuarios.Remove(usuarios);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
