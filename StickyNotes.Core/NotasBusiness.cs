using System;
using StickyNotes.Data.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StickyNotes.Data;

namespace StickyNotes.Core
{
    public class NotasBusiness
    {

        private readonly IRepositoryNotas _repositoryNotas;

        // Constructor
        public NotasBusiness() {
            _repositoryNotas = new RepositoryNotas();

        }

        // Upsert (Update / Insert)
        public bool SaveOrUpdate(Notas notas)
        {
            if (notas.idNota <= 0)
                _repositoryNotas.Add(notas);
            else
                _repositoryNotas.Update(notas);

            return true;
        }

        public bool Delete(int id)
        {
            _repositoryNotas.Delete(id); 
            return true;
        }

        public IEnumerable<Notas> GetNotas(int id)
        {
            return id <= 0
                ? _repositoryNotas.GetAll()
                : new List<Notas>() { _repositoryNotas.GetById(id) };
        }
    }
}
