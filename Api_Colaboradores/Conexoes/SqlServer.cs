using Api_Colaboradores.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Api_Colaboradores.Conexoes
{
    public class SqlServer
    {
        private readonly SqlConnection _conexao;

        public SqlServer()
        {
            this._conexao = new SqlConnection(@"Server=DESKTOP-05IP2B2\SQLEXPRESS ;Database=LOJA;User Id=sa;Password=Pardini2021!;");
        }

        public void InserirColaborador(Models.Colaboradores colaborador)
        {
            try
            {
                _conexao.Open();

                string sql = @"INSERT INTO Colaboradores
                                    (Nome, Cargo, Empresa)
                                    VALUES
                                    (@Nome, @Cargo, @Empresa);";
                using(SqlCommand cmd = new SqlCommand(sql, _conexao))
                {
                    
                    cmd.Parameters.AddWithValue("Nome", colaborador.Nome);
                    cmd.Parameters.AddWithValue("Cargo", colaborador.Cargo);
                    cmd.Parameters.AddWithValue("Empresa", colaborador.Empresa);

                    cmd.ExecuteNonQuery();

                }
            }
            finally
            {
                _conexao.Close();
            }
        }
        public bool VerificarExistenciaColaborador(string nome)
        {
            try
            {
                _conexao.Open();

                string query = @"select Count(Nome) AS total 
                                 from Colaboradores WHERE Nome = @Nome;";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Nome", nome);

                    return Convert.ToBoolean(cmd.ExecuteScalar());
                }
            }
            finally
            {
                _conexao.Close();
            }
        }
        public void AtualizarColaborador(Models.Colaboradores colaborador)
        {
            try
            {
                _conexao.Open();

                string query = @"UPDATE Colaboradores
                                   SET Nome = @Nome
                                      ,Cargo = @Cargo
                                      ,Empresa = @Empresa
                                      
                                 WHERE Nome = @Nome";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("Nome", colaborador.Nome);
                    cmd.Parameters.AddWithValue("Cargo", colaborador.Cargo);
                    cmd.Parameters.AddWithValue("Empresa", colaborador.Empresa);

                    cmd.ExecuteNonQuery();

                }
            }
            finally
            {
                _conexao.Close();
            }
        }

        public void DeletarColaborador(Models.Colaboradores colaborador)
        {
            try
            {
                _conexao.Open();

                string query = @"DELETE FROM Colaboradores
                                 WHERE Id = @Id";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Id", colaborador.Id);
                    cmd.ExecuteNonQuery();

                }
            }
            finally
            {
                _conexao.Close();
            }
        }

        public List<Models.Colaboradores> ListarColaborador()
        {
            var colaboradores = new List<Models.Colaboradores>();
            try
            {
                _conexao.Open();

                string query = @"Select * FROM Colaboradores";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    var rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        var colaborador = new Models.Colaboradores();

                        colaborador.Id = (int)rdr["Id"];
                        colaborador.Nome = rdr["Nome"].ToString();
                        colaborador.Cargo = rdr["Cargo"].ToString();
                        colaborador.Empresa = rdr["Empresa"].ToString();




                       colaboradores.Add(colaborador);
                    }
                }
            }
            finally
            {
                _conexao.Close();
            }

            return colaboradores;
        }

        public Models.Colaboradores SelecionarColaborador(string nome)
        {
            try
            {
                _conexao.Open();

                string query = @"Select * FROM Colaboradores
                                 WHERE Nome = @Nome";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Nome", nome);
                    var rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        var colaborador = new Models.Colaboradores();
                        colaborador.Nome = rdr["Nome"].ToString();
                        colaborador.Cargo = rdr["Cargo"].ToString();
                        colaborador.Empresa = rdr["Empresa"].ToString();


                        return colaborador;
                    }
                    else
                    {
                        throw new InvalidOperationException("Nome " + nome + " não encontrado!");
                    }
                }
            }
            finally
            {
                _conexao.Close();
            }
        }
    }
}
