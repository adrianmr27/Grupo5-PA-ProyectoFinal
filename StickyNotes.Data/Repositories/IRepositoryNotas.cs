using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StickyNotes.Data.Repository;

namespace StickyNotes.Data.Repositories
{
    public interface IRepositoryNotas : IRepositoryBase<Notas>
{
    IEnumerable<Notas> GetNotasFijadas();
    IEnumerable<Notas> GetNotasNoFijadas();
    void TogglePin(int idNota);
}
}
