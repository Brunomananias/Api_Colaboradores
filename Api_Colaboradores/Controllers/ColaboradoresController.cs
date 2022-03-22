using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Api_Colaboradores.Controllers
{
    
    [ApiController]
    public class ColaboradoresController : ControllerBase
    {
        private readonly Conexoes.SqlServer _Sql;

        public ColaboradoresController()
        {
            _Sql = new Conexoes.SqlServer();
        }

        [HttpPost("v1/Colaboradores")]
        public void InserirColaborador(Models.Colaboradores colaborador)
        {
            _Sql.InserirColaborador(colaborador);
        }

        [HttpDelete("v1/Colaboradores")]
        public void DeletarColaborador(Models.Colaboradores colaborador)
        {
            _Sql.DeletarColaborador(colaborador);
        }

        [HttpPut("v1/Colaboradores")]
        public void AtualizarColaborador(Models.Colaboradores colaborador)
        {
            _Sql.AtualizarColaborador(colaborador);
        }


        [HttpGet("v1/Colaboradores")]
        public List<Models.Colaboradores> ListarColaboradores()
        {
            return _Sql.ListarColaborador();

        }

        [HttpGet("v1/Colaboradores/{nome}")]
        public Models.Colaboradores ListarColaboradores(string nome)
        {
            return _Sql.SelecionarColaborador(nome);
        }
    }
}
