using System;
using StickyNotes.Data.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StickyNotes.Data;

namespace StickyNotes.Core
{
    public class UsuariosBusiness
    {

        private readonly IRepositoryUsuarios _repositoryUsuarios;

        // Constructor
        public UsuariosBusiness() {
            _repositoryUsuarios = new RepositoryUsuarios();

        }

        // Upsert (Update / Insert)
        public bool SaveOrUpdate(Usuarios usuarios)
        {
            if (usuarios.idUsuario <= 0)
                _repositoryUsuarios.Add(usuarios);
            else
                _repositoryUsuarios.Update(usuarios);

            return true;
        }

        public bool Delete(int id)
        {
            _repositoryUsuarios.Delete(id); 
            return true;
        }

        public IEnumerable<Usuarios> GetUsuarios(int id)
        {
            return id <= 0
                ? _repositoryUsuarios.GetAll()
                : new List<Usuarios>() { _repositoryUsuarios.GetById(id) };
        }

        public Usuarios GetUsuarioById(int id)
        {
            if (id <= 0)
                return null;

            return _repositoryUsuarios.GetById(id);
        }

    }
}
