using StickyNotes.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StickyNotes.Web.Controllers
{
    public class ControllerBase : Controller
    {

        protected readonly EstadosBusiness EstadosBusiness;
        protected readonly UsuariosBusiness UsuariosBusiness;

        public ControllerBase()
        {
            EstadosBusiness = new EstadosBusiness();
            UsuariosBusiness = new UsuariosBusiness();
        }

        protected void ViewEstados()
        {
            ViewBag.idEstado = new SelectList(EstadosBusiness.GetEstados(id:0), "idEstado", "estado");
        }

    }
}