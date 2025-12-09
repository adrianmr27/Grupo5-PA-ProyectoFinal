using StickyNotes.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//TODO: Liskov Substitution Principle (LSP)


namespace StickyNotes.Data.Repository
{
    public interface IRepositoryCategorias : IRepositoryBase<Categorias>
    {
    }

    public class RepositoryCategorias : RepositoryBase<Categorias>, IRepositoryCategorias
    {
        public RepositoryCategorias() : base()
        {
        }
    }
}
