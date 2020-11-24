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
    /// Controlador para cliente
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        /// <summary>
        /// Interface para acesso ao banco 
        /// </summary>
        public readonly IRepository repo;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="repo"></param>
        public CustomerController( IRepository repo) 
        {
            this.repo = repo;
        }

        /// <summary>
        /// Get todos os clientes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get() 
        {
            var result = repo.GetAllCustomers(true);
            return Ok(result);
        }

        /// <summary>
        /// Get por id de cliente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var customer = repo.GetCustomerById(id,true);
            
            if (customer == null) 
            {
                return BadRequest("O cliente não foi encontrado");
            }
            else 
            {
                return Ok(customer);
            }
        }

        /// <summary>
        /// Procura cliente por parametro de pesquisa
        /// </summary>
        /// <param name="parametroPesquisa"></param>
        /// <returns></returns>
       [HttpGet("Search")]
        public IActionResult GetBySearchInfo(string parametroPesquisa)
        {
            // Um search basicão aqui só para ver a integração com o front, adequar esse projeto futuramente pra usar filtro e paginação

            var customer = repo.SearchCustomer(parametroPesquisa, true);

             if (customer == null || !customer.Any())
             {
                 return BadRequest("Nenhum cliente encontrado");
             }

          return Ok(customer);

         }

        /// <summary>
        /// Adiciona novo cliente
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(Customer customer)
        {
            var alterCustomer = repo.GetCustomerByDocument(customer.Document, false);

            if (alterCustomer != null && alterCustomer.Length > 0) 
            {
                return BadRequest("Número de documento já cadastrado no sistema");
            }

            repo.Add(customer);
            if (repo.SaveChanges()) 
            {
                return Ok(customer);
            }

            return BadRequest("Cliente não cadastrado");
        }

        /// <summary>
        /// Put para cliente
        /// </summary>
        /// <param name="id">id cliente</param>
        /// <param name="customer">cliente</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, Customer customer)
        {
            var alterCustomer = repo.GetCustomerById(id, false);
            if (alterCustomer == null)
            {
                return BadRequest("Cliente não encontrado");
            }

            
            if (!repo.IsDocumentUpdateValid(id, customer.Document))
            {
                return BadRequest("Documento já cadastrado para outro usuário no sistema");
            }

            repo.Update(customer);
            if (repo.SaveChanges())
            {
                return Ok(customer);
            }

            return BadRequest("Cliente não atualizado");
        }

        /// <summary>
        /// Patch para cliente
        /// </summary>
        /// <param name="id">id cliente</param>
        /// <param name="customer">cliente</param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Customer customer)
        {
            var alterCustomer = repo.GetCustomerById(id,false);
            if (alterCustomer == null)
            {
                return BadRequest("Cliente não encontrado");
            }

            if (!repo.IsDocumentUpdateValid(id,customer.Document))
            {
                return BadRequest("Documento já cadastrado para outro usuário no sistema");
            }
            repo.Update(customer);
            if (repo.SaveChanges())
            {
                return Ok(customer);
            }

            return BadRequest("Cliente não atualizado");
        }

        /// <summary>
        /// Delete para cliente
        /// </summary>
        /// <param name="id">id cliente</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customer = repo.GetCustomerById(id,false);
            if (customer == null) 
            {
                return BadRequest("Cliente não encontrado");
            }
            repo.Delete(customer);
            if (repo.SaveChanges())
            {
                return Ok(customer);
            }

            return BadRequest("Cliente não removido");
        }
    }
}
