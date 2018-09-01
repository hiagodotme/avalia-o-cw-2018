using Projeto.Models;
using System;
using System.Web.Mvc;
using Projeto.DTO;

namespace Projeto.Controllers
{
    public class UsuarioController : GenericController
    {
        public ActionResult Login()
        {
            return GenericView(false);
        }

        public ActionResult Cadastro()
        {
            return GenericView(false);
        }

        public ActionResult Autenticar(Usuario u)
        {
            try
            {
                Usuario user = UsuarioDTO.VerificaUsuario(u);
                Session["Auth"] = user;
                return RedirectToAction("", "", null);
            } catch (Exception e)
            {
                MakeError(e.Message);
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            Session.Remove("Auth");
            MakeSuccess("Sessão encerrada com sucesso.");
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Cadastrar(Usuario u)
        {
            try
            {
                UsuarioDTO.Cadastrar(u);
                MakeSuccess("Usuário foi inserido com sucesso.");
                return RedirectToAction("Login");
            } catch (Exception e)
            {
                MakeError(e.Message);
                return RedirectToAction("Cadastro");
            }
        }
    }
}