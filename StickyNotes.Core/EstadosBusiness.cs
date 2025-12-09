using System;
using StickyNotes.Data.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StickyNotes.Data;

namespace StickyNotes.Core
{
    public class EstadosBusiness
    {

        private readonly IRepositoryEstados _repositoryEstados;

        // Constructor
        public EstadosBusiness() {
            _repositoryEstados = new RepositoryEstados();

        }

        // Upsert (Update / Insert)
        public bool SaveOrUpdate(Estados estados)
        {
            if (estados.idEstado <= 0)
                _repositoryEstados.Add(estados);
            else
                _repositoryEstados.Update(estados);

            return true;
        }

        public bool Delete(int id)
        {
            _repositoryEstados.Delete(id); 
            return true;
        }

        public IEnumerable<Estados> GetEstados(int id)
        {
            return id <= 0
                ? _repositoryEstados.GetAll()
                : new List<Estados>() { _repositoryEstados.GetById(id) };
        }
    }
}
