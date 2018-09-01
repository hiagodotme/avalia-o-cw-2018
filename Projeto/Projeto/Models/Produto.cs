using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public int UsuarioId { get; set; }
        public String Nome { get; set; }
        public Decimal Preco { get; set; }
        public bool Vendido { get; set; }
    }
}