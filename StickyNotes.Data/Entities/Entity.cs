using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//TODO: Interface Segregation Principle  (ISP)
//TODO: la interfaz está “segregada”: las clases no se ven forzadas a implementar cosas que no necesitan.


namespace StickyNotes.Data.Entities
{
    public class Entity : IEntity
    {
        public string Description { get; set; }
    }

    public interface IEntity
    {
        string Description { get; set; }
    }


}
