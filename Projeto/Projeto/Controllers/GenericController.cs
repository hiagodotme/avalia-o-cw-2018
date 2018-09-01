using Projeto.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto.Controllers
{
    public abstract class GenericController : Controller
    {
        protected void MakeError(string error)
        {
            Session["VB_Error"] = error;
        }

        protected void MakeInformation(string info)
        {
            Session["VB_Information"] = info;
        }

        protected void MakeSuccess(string success)
        {
            Session["VB_Success"] = success;
        }

        protected dynamic GetViewBag()
        {
            dynamic obj = ViewBag;

            if (Session["VB_Error"] != null)
            {
                obj.Erro = Session["VB_Error"];
                Session.Remove("VB_Error");
            }

            if (Session["VB_Success"] != null)
            {
                obj.Sucesso = Session["VB_Success"];
                Session.Remove("VB_Success");
            }

            if (Session["VB_Information"] != null)
            {
                obj.Informacao = Session["VB_Information"];
                Session.Remove("VB_Information");
            }

            return obj;
        }

        protected ActionResult GenericView() {
            return GenericView(true);
        }

        protected ActionResult GenericView(bool forceAuth)
        {
            // Verificando se o usuário tá conectado
            if (forceAuth && Session["Auth"] == null)
            {
                MakeInformation("É necessário estar conectado para usar a aplicação");
                return RedirectToAction("Login", "Usuario", null);
            }
            else
            {
                ViewBag.Alerts = GetViewBag();
                if (Session["Auth"] != null)
                {
                    // Atualizando sempre para obter a receita do usuário
                    ViewBag.Usuario = Session["Auth"];
                    ViewBag.Usuario = UsuarioDTO.RecuperaUsuarioByEmail(ViewBag.Usuario.Email);
                }

                return View();
            }
        }
    }
}