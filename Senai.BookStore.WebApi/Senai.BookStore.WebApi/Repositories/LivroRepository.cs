using Senai.BookStore.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.BookStore.WebApi.Repositories
{
    public class LivroRepository
    {
        private string StringConexao = "Data Source=.\\SqlExpress; initial catalog=M_BookStore;User Id=sa;Pwd=132;";

        public void Cadastrar(LivroDomain livro)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string Query = "INSERT INTO Livros (Titulo, IdAutor, IdGenero) VALUES (@Titulo, @IdAutor, @IdGenero);";

                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@Titulo", livro.Titulo);
                cmd.Parameters.AddWithValue("@IdAutor", livro.AutorId);
                cmd.Parameters.AddWithValue("@IdGenero", livro.GeneroId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<LivroDomain> Listar()
        {
            List<LivroDomain> livros = new List<LivroDomain>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string Query = "SELECT L.IdLivro, L.Titulo, L.IdAutor, A.Nome AS NomeAutor FROM Livros A INNER JOIN Autores A ON L.IdAutor = A.IdAutor";

                con.Open();

                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        LivroDomain livro = new LivroDomain
                        {
                            IdLivro = Convert.ToInt32(sdr["IdLivro"]),
                            Titulo = sdr["Titulo"].ToString(),
                            Autor = new AutorDomain
                            {
                                IdAutor = Convert.ToInt32(sdr["IdAutor"]),
                                Nome = sdr["NomeAutor"].ToString()
                            }
                        };
                        livros.Add(livro);
                    }

                }
            }
            return livros;
        }

        public LivroDomain BuscarPorId(int id)
        {
            string Query = "SELECT IdLivro, Titulo FROM Livros WHERE IdLivro = @IdLivro";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.Parameters.AddWithValue("@IdLivro", id);
                    sdr = cmd.ExecuteReader();

                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            LivroDomain livro = new LivroDomain
                            {
                                IdLivro = Convert.ToInt32(sdr["IdLivro"]),
                                Titulo = sdr["Titulo"].ToString()
                            };
                            return livro;
                        }
                    }

                    return null;
                }
            }

        }
    }
}
