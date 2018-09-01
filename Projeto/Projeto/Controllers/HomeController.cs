using Projeto.DTO;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto.Controllers
{
    public class HomeController : GenericController
    {
        public ActionResult Index()
        {
            ViewBag.Dados = ProdutoDTO.RecuperaProdutosNaoVendidos();
            return GenericView();   
        }

        public ActionResult AdicionarProduto()
        {
            return GenericView();
        }

        public ActionResult CriarNovoProduto(Produto p)
        {
            try
            {
                Usuario u = Session["Auth"] as Usuario;
                p.UsuarioId = u.UsuarioId;
                ProdutoDTO.Cadastrar(p);
                MakeSuccess("Produto adicionado com sucesso.");
            } catch (Exception e)
            {
                MakeError(e.Message);
            }

            return RedirectToAction("Index");
        }

        public ActionResult DetalheProduto(int Id)
        {
            try
            {
                ViewBag.Produto = ProdutoDTO.RecuperaProdutosNaoVendidos().Where(p => p.ProdutoId == Id).First();
                if(ViewBag.Produto == null)
                {
                    throw new Exception("O Produto já está vendido ou não existe!");
                }
                return GenericView();
            } catch(Exception e)
            {
                MakeError(e.Message);
                return RedirectToAction("Index");
            }
        }

        public ActionResult RegistrarCompra(int Id)
        {
            Produto p = ProdutoDTO.RecuperaProdutosNaoVendidos().Where(product => product.ProdutoId == Id).First();
            if(p.Vendido)
            {
                MakeError("O Produto já está vendido!");
                return RedirectToAction("Index");
            }

            ProdutoDTO.RegistraCompra(p);
            MakeSuccess("Produto adquirido com sucesso!");
            return RedirectToAction("Index");
        }
    }
}