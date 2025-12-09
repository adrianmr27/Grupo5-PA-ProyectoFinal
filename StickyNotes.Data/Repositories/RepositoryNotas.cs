using StickyNotes.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//TODO: Liskov Substitution Principle (LSP)


namespace StickyNotes.Data.Repository
{
    public interface IRepositoryNotas : IRepositoryBase<Notas>
    {
    }

    public class RepositoryNotas : RepositoryBase<Notas>, IRepositoryNotas
    {
        public RepositoryNotas() : base()
        {
        }
    }
}
