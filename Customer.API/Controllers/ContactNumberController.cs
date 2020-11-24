using CustomerVet.API.Data;
using CustomerVet.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerVet.API.Controllers
{
    /// <summary>
    /// Controller para contatos telefônicos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ContactNumberController : ControllerBase
    {
        /// <summary>
        /// Interface para acesso ao banco
        /// </summary>
        public readonly IRepository repo;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="repo"></param>
        public ContactNumberController(IRepository repo)
        {
            this.repo = repo;
        }

        /// <summary>
        /// Adiciona novo contato
        /// </summary>
        /// <param name="contactNumber"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(ContactNumber contactNumber)
        {          
            if (repo.IsNumeroCadastradoForCustomer(contactNumber.CustomerId, contactNumber.TelephoneNumber))
            {
                return BadRequest("Número de contato já cadastrado");
            }

            repo.Add(contactNumber);
            if (repo.SaveChanges())
            {
                return Ok(contactNumber);
            }

            return BadRequest("Cliente não cadastrado");
        }

        /// <summary>
        /// Put para novo contato
        /// </summary>
        /// <param name="id">id contato</param>
        /// <param name="contactNumber">numero contato</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, ContactNumber contactNumber)
        {
            var alterContact = repo.GetContactById(id);

            if (alterContact == null)
            {
                return BadRequest("Contato não encontrado");
            }

            repo.Add(contactNumber);
            if (repo.SaveChanges())
            {
                return Ok(contactNumber);
            }

            return BadRequest("Contado não atualizado");
        }

        /// <summary>
        /// Patch para contato
        /// </summary>
        /// <param name="id">id contato</param>
        /// <param name="contactNumber">numero contato</param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ContactNumber contactNumber) 
        {
            var alterContact = repo.GetContactById(id);

            if (alterContact == null)
            {
                return BadRequest("Contato não encontrado");
            }

            repo.Add(contactNumber);
            if (repo.SaveChanges())
            {
                return Ok(contactNumber);
            }

            return BadRequest("Contado não atualizado");
        }

        /// <summary>
        /// Deleta fcontato
        /// </summary>
        /// <param name="id">id contato</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var contact = repo.GetContactById(id);
            if (contact == null)
            {
                return BadRequest("Contato de cliente não encontrado");
            }
            repo.Delete(contact);
            if (repo.SaveChanges())
            {
                return Ok(contact);
            }

            return BadRequest("Contato não removido");
        }
    }
}
