using Senai.BookStore.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.BookStore.WebApi.Repositories
{
    public class AutorRepository
    {
        private string StringConexao = "Data Source=.\\SqlExpress; initial catalog=M_BookStore;User Id=sa;Pwd=132;";

        public List<AutorDomain> Listar()
        {

            List<AutorDomain> autores = new List<AutorDomain>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string Query = "SELECT IdAutor, Nome , Email, Ativo, DataNascimento FROM Autores ORDER BY Nome DESC";

                con.Open();
                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        AutorDomain autor = new AutorDomain
                        {
                            IdAutor = Convert.ToInt32(rdr["IdAutor"]),
                            Nome = rdr["Nome"].ToString(),
                            Email = rdr["Email"].ToString(),
                            Ativo = Convert.ToBoolean(rdr["Ativo"]),
                            DataNascimento = Convert.ToDateTime(rdr["DataNascimento"])
                        };
                        autores.Add(autor);
                    }

                }

            }
            return autores;
        }
    }
}
