using StickyNotes.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//TODO: Liskov Substitution Principle (LSP)


namespace StickyNotes.Data.Repository
{
    public interface IRepositoryEstados : IRepositoryBase<Estados>
    {
    }

    public class RepositoryEstados : RepositoryBase<Estados>, IRepositoryEstados
    {
        public RepositoryEstados() : base()
        {
        }
    }
}
