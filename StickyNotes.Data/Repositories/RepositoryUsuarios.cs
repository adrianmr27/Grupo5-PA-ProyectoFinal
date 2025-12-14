using StickyNotes.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//TODO: Liskov Substitution Principle (LSP)


namespace StickyNotes.Data.Repository
{
    public interface IRepositoryUsuarios : IRepositoryBase<Usuarios>
    {
        Usuarios GetByEmail(string email);

    }

    public class RepositoryUsuarios : RepositoryBase<Usuarios>, IRepositoryUsuarios
    {
        public RepositoryUsuarios() : base()
        {
        }

        public Usuarios GetByEmail(string email)
        {
            return _context.Set<Usuarios>()
                .FirstOrDefault(u => u.correo == email);
        }
    }
}
