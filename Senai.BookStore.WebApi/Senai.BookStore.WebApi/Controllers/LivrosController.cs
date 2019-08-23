using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.BookStore.WebApi.Domains;
using Senai.BookStore.WebApi.Repositories;

namespace Senai.BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        LivroRepository LivroRepository = new LivroRepository();

        [HttpPost]
        public IActionResult Cadastrar(AutorDomain autorDomain)
        {
            LivroRepository.Cadastrar(livroDomain);
            return Ok();
        }

        [HttpGet]
        public IEnumerable<AutorDomain> Listar()
        {
            return LivroRepository.Listar();
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            LivroDomain Livro = LivroRepository.BuscarPorId(id);
            if (Livro == null)
            {
                return NotFound();
            }
            return Ok(Livro);
        }

        [HttpPut]
        public IActionResult Atualizar(LivroDomain livroDomain)
        {
            LivroRepository.Alterar(livroDomain);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            LivroRepository.Deletar(id);
            return Ok();
        }

    }
}