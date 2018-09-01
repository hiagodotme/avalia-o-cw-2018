using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public String Email { get; set; }
        public String Nome { get; set; }
        public String Senha { get; set; }
        public Decimal Receita { get; set; }
    }
}