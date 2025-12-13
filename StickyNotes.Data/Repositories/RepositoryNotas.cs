using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StickyNotes.Data.Repository;


//TODO: Liskov Substitution Principle (LSP)


namespace StickyNotes.Data.Repository
{
    public interface IRepositoryNotas : IRepositoryBase<Notas>
    {
        IEnumerable<Notas> GetNotasFijadas();
        IEnumerable<Notas> GetNotasNoFijadas();
        void TogglePin(int idNota);
    }

    public class RepositoryNotas : RepositoryBase<Notas>, IRepositoryNotas
    {
        public RepositoryNotas() : base()
        {
        }

        public IEnumerable<Notas> GetNotasFijadas()
        {
            return _set.Where(n => n.fijada).ToList();
        }

        public IEnumerable<Notas> GetNotasNoFijadas()
        {
            return _set.Where(n => !n.fijada).ToList();
        }

        public void TogglePin(int idNota)
        {
            var nota = _set.Find(idNota);
            if (nota != null)
            {
                nota.fijada = !nota.fijada;
                Save();
            }
        }
    }
}
