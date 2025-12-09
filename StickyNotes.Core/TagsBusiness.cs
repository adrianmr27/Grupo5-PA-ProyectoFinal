using System;
using StickyNotes.Data.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StickyNotes.Data;

namespace StickyNotes.Core
{
    public class TagsBusiness
    {

        private readonly IRepositoryTags _repositoryTags;

        // Constructor
        public TagsBusiness() {
            _repositoryTags = new RepositoryTags();

        }

        // Upsert (Update / Insert)
        public bool SaveOrUpdate(Tags tags)
        {
            if (tags.idTag <= 0)
                _repositoryTags.Add(tags);
            else
                _repositoryTags.Update(tags);

            return true;
        }

        public bool Delete(int id)
        {
            _repositoryTags.Delete(id); 
            return true;
        }

        public IEnumerable<Tags> GetCategorias(int id)
        {
            return id <= 0
                ? _repositoryTags.GetAll()
                : new List<Tags>() { _repositoryTags.GetById(id) };
        }
    }
}
