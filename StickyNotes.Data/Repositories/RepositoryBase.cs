using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using StickyNotes.Data;


//TODO: Liskov Substitution Principle (LSP)
//TODO: Implementa IRepositoryBase<T>, y cualquier clase que use IRepositoryBase<T> puede trabajar con RepositoryBase<T>
//sin alterar el comportamiento esperado.


namespace StickyNotes.Data.Repository
{
    public interface IRepositoryBase<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        void Save();
    }

    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly StickyNotesEntities _context;
        protected readonly DbSet<T> _set;

        public RepositoryBase()
        {
            _context = new StickyNotesEntities();
            _set = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _set.ToList();
        }

        public T GetById(int id)
        {
            return _set.Find(id);
        }

        public void Add(T entity)
        {
            _set.Add(entity);
            Save();
        }

        public void Update(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                _set.Attach(entity);

            _context.Entry(entity).State = EntityState.Modified;
            Save();
        }

        public void Delete(int id)
        {
            T entityToDelete = _set.Find(id);
            if (entityToDelete != null)
            {
                _set.Remove(entityToDelete);
                Save();
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
