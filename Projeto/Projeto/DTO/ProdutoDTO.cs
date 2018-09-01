using Projeto.Models;
using Projeto.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Projeto.DTO
{
    public class ProdutoDTO
    {
        /**
         * Retorna uma lista de produtos não vendidos
         */
        public static List<Produto> RecuperaProdutosNaoVendidos()
        {
            List<Produto> produtos = new List<Produto>();

            using (DataBase db = new DataBase())
            {
                SqlCommand sc = db.GetSqlCommand();
                sc.CommandText = "SELECT * FROM Produto WHERE Vendido = 0";
                SqlDataReader hasUser = sc.ExecuteReader();
                while (hasUser.Read())
                {
                    Produto p = new Produto()
                    {
                        Nome = (string)hasUser["Nome"],
                        Preco = (Decimal)hasUser["Preco"],
                        ProdutoId = (int)hasUser["ProdutoId"],
                        UsuarioId = (int)hasUser["UsuarioId"],
                        Vendido = (bool)hasUser["Vendido"],

                    };
                    produtos.Add(p);
                }
            }

            return produtos;
        }

        /**
         * Cadastra um novo produto
         */
        public static void Cadastrar(Produto p)
        {
            using (DataBase db = new DataBase())
            {
                // inserindo produto
                SqlCommand sc = db.GetSqlCommand();
                sc.CommandText = "INSERT INTO Produto (Nome, Preco, UsuarioId, Vendido) VALUES (@Nome, @Preco, @UsuarioId, @Vendido)";
                sc.Parameters.AddWithValue("Nome", p.Nome);
                sc.Parameters.AddWithValue("Preco", p.Preco);
                sc.Parameters.AddWithValue("UsuarioId", p.UsuarioId);
                sc.Parameters.AddWithValue("Vendido", 0);
                sc.ExecuteNonQuery();
            }
        }

        /**
         * Registra a compra
         */
        public static void RegistraCompra(Produto p)
        {
            // Atualizando a receita do usuário
            UsuarioDTO.AddReceita(p.UsuarioId, p.Preco);

            using (DataBase db = new DataBase())
            {

                // Atualizando venda
                SqlCommand sc = db.GetSqlCommand();
                sc.CommandText = "UPDATE Produto SET Vendido = 1 WHERE ProdutoId=@ProdutoId";
                sc.Parameters.AddWithValue("ProdutoId", p.ProdutoId);
                sc.ExecuteNonQuery();
            }
        }
    }
}