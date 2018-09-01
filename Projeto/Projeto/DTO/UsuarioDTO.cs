using Projeto.Models;
using Projeto.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Projeto.DTO
{
    public class UsuarioDTO
    {
        /**
         * Verifica se um usuário é válido.
         */
        public static Usuario VerificaUsuario(Usuario u)
        {
            using (DataBase db = new DataBase())
            {
                SqlCommand sc = db.GetSqlCommand();
                sc.CommandText = "SELECT * FROM Usuario WHERE Email=@Email and Senha=@Senha";
                sc.Parameters.AddWithValue("Email", u.Email);
                sc.Parameters.AddWithValue("Senha", u.Senha);
                SqlDataReader hasUser = sc.ExecuteReader();

                if (hasUser.Read())
                {
                    Usuario user = new Usuario()
                    {
                        Nome = (string)hasUser["Nome"],
                        Email = (string)hasUser["Email"],
                        Receita = (Decimal)hasUser["Receita"],
                        UsuarioId = (int)hasUser["UsuarioId"],

                    };
                    return user;
                }
                else
                {
                    throw new Exception("Usuário ou senha incorretos");
                }
            }
        }

        /**
         * Recupera um usuario pelo e-mail
         */
        public static Usuario RecuperaUsuarioByEmail(string email)
        {
            using (DataBase db = new DataBase())
            {
                SqlCommand sc = db.GetSqlCommand();
                sc.CommandText = "SELECT * FROM Usuario WHERE Email=@Email";
                sc.Parameters.AddWithValue("Email", email);
                SqlDataReader hasUser = sc.ExecuteReader();
                hasUser.Read();
                return new Usuario()
                {
                    Nome = (string)hasUser["Nome"],
                    Email = (string)hasUser["Email"],
                    Receita = (Decimal)hasUser["Receita"],
                    UsuarioId = (int)hasUser["UsuarioId"],

                };
            }
        }

        /**
         * Cadastra um novo usuário
         */
        public static void Cadastrar(Usuario u)
        {
            using (DataBase db = new DataBase())
            {
                // verificando se o usuário existe
                SqlCommand vuser = db.GetSqlCommand();
                vuser.CommandText = "SELECT UsuarioId FROM Usuario WHERE Email=@Email";
                vuser.Parameters.AddWithValue("Email", u.Email);
                SqlDataReader hasUser = vuser.ExecuteReader();
                if (hasUser.Read())
                {
                    // existe usuário, não pode cadastrar.
                    throw new Exception("Já existe um usuário com o e-mail " + u.Email + " registrado.");
                }

                // inserindo usuário
                SqlCommand sc = db.GetSqlCommand();
                sc.CommandText = "INSERT INTO Usuario (Nome, Email, Senha, Receita) VALUES (@Nome, @Email, @Senha, @Receita)";
                sc.Parameters.AddWithValue("Nome", u.Nome);
                sc.Parameters.AddWithValue("Email", u.Email);
                sc.Parameters.AddWithValue("Senha", u.Senha);
                sc.Parameters.AddWithValue("Receita", 0);
                sc.ExecuteNonQuery();
            }
        }

        /**
         * Adiciona uma nova receita para o usuário
         */
        public static void AddReceita(int usuarioId, Decimal preco)
        {
            using (DataBase db = new DataBase())
            {
                // inserindo usuário
                SqlCommand sc = db.GetSqlCommand();
                sc.CommandText = "UPDATE Usuario SET Receita = Receita+@Valor WHERE UsuarioId=@UsuarioId";
                sc.Parameters.AddWithValue("Valor", preco);
                sc.Parameters.AddWithValue("UsuarioId", usuarioId);
                sc.ExecuteNonQuery();
            }
        }
    }
}