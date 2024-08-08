using SportSync.Models;
using System.Collections.Generic;

namespace SportSync.ViewModels
{
    public class UsuariosViewModel
    {
        public IEnumerable<Usuario> Usuarios { get; set; }
        public Dictionary<int, string> Roles { get; set; }
    }
}
