using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//TODO: Interface Segregation Principle  (ISP)
//TODO: solo se ve obligada a implementar lo que realmente necesita, sin depender de métodos o propiedades que no va a usar.


namespace AdvancedProgramming.Data
{
    public partial class Usuarios
    {
        public int idUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string contrasena { get; set; }
        public string correo { get; set; }
        public DateTime fechaRegistro { get; set; }
        public int idEstado { get; set; }
    }
}
