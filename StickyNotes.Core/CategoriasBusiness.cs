using System;
using StickyNotes.Data.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StickyNotes.Data;

namespace StickyNotes.Core
{
    public class CategoriasBusiness
    {

        private readonly IRepositoryCategorias _repositoryCategorias;

        // Constructor
        public CategoriasBusiness() {
            _repositoryCategorias = new RepositoryCategorias();

        }

        // Upsert (Update / Insert)
        public bool SaveOrUpdate(Categorias categorias)
        {
            if (categorias.idCategoria <= 0)
                _repositoryCategorias.Add(categorias);
            else
                _repositoryCategorias.Update(categorias);

            return true;
        }

        public bool Delete(int id)
        {
            _repositoryCategorias.Delete(id); 
            return true;
        }

        public IEnumerable<Categorias> GetCategorias(int id)
        {
            return id <= 0
                ? _repositoryCategorias.GetAll()
                : new List<Categorias>() { _repositoryCategorias.GetById(id) };
        }
    }
}
