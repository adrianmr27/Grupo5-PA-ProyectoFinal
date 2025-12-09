using StickyNotes.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//TODO: Liskov Substitution Principle (LSP)


namespace StickyNotes.Data.Repository
{
    public interface IRepositoryTags : IRepositoryBase<Tags>
    {
    }

    public class RepositoryTags : RepositoryBase<Tags>, IRepositoryTags
    {
        public RepositoryTags() : base()
        {
        }
    }
}
