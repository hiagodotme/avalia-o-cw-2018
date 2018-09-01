using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto.Controllers
{
    public class UsuarioController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        public ActionResult Autenticar(Usuario u)
        {
            DBUtils.DB d = new DBUtils.DB();
            using (SqlConnection a = d.getConn())
            {
                SqlCommand sc = new SqlCommand();
                sc.Connection = a;
                sc.CommandText = "SELECT * FROM Usuario WHERE Email=@Email and Senha=@Senha";
                sc.Parameters.AddWithValue("Nome", u.Nome);
                sc.Parameters.AddWithValue("Email", u.Email);
                sc.Parameters.AddWithValue("Senha", u.Senha);
                sc.Parameters.AddWithValue("Receita", 0);
                sc.ExecuteNonQuery();
            }

            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Cadastrar(Usuario u)
        {
            DBUtils.DB d = new DBUtils.DB();
            using (SqlConnection a = d.getConn())
            {
                SqlCommand sc = new SqlCommand();
                sc.Connection = a;
                sc.CommandText = "INSERT INTO Usuario (Nome, Email, Senha, Receita) VALUES (@Nome, @Email, @Senha, @Receita)";
                sc.Parameters.AddWithValue("Nome", u.Nome);
                sc.Parameters.AddWithValue("Email", u.Email);
                sc.Parameters.AddWithValue("Senha", u.Senha);
                sc.Parameters.AddWithValue("Receita", 0);
                sc.ExecuteNonQuery();
            }

            return RedirectToAction("Login");
        }
    }
}