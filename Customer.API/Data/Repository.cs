using CustomerVet.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerVet.API.Data
{
    /// <summary>
    /// Repositório
    /// </summary>
    public class Repository : IRepository

    {
        /// <summary>
        /// Contexto de acesso ao cliente no banco
        /// </summary>
        private readonly CustomerContext context;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="context"></param>
        public Repository(CustomerContext context) 
        {
            this.context = context;
        }
        /// <summary>
        /// Adicionar
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param n
        public void Add<T>(T entity) where T : class
        {
            context.Add(entity);
        }
        /// <summary>
        /// Atualizar
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public void Update<T>(T entity) where T : class
        {
            context.Update(entity);
        }
        /// <summary>
        /// Remover
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public void Delete<T>(T entity) where T : class
        {
            context.Remove(entity);
        }
        /// <summary>
        /// Salvar mudanças
        /// </summary>
        /// <returns></returns>
        public bool SaveChanges()
        {
            return (context.SaveChanges() > 0);
        }

        /// <summary>
        /// Recupera todos os clientes
        /// </summary>
        /// <param name="includeContatos"></param>
        /// <returns></returns>
        public Customer[] GetAllCustomers(bool includeContatos = false)
        {
            IQueryable<Customer> query = context.Customers;

            if (includeContatos) 
            {
                query = query.Include(a => a.ContactNumbers);
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);

            return query.ToArray();
        }

        /// <summary>
        /// Recupera cliente por Id
        /// </summary>
        /// <param name="customerId">id cliente</param>
        /// <param name="includeContatos">incluir contatos</param>
        /// <returns></returns>
        public Customer[] GetCustomerById(int customerId, bool includeContatos = false)
        {
            IQueryable<Customer> query = context.Customers;

            if (includeContatos)
            {
                query = query.Include(a => a.ContactNumbers);

            }

            query = query.AsNoTracking()
                .OrderBy(a => a.Id)
                .Where(c => c.Id == customerId);

            return query.ToArray();
        }

        /// <summary>
        /// Recupera cliente por documento
        /// </summary>
        /// <param name="document">numero documento</param>
        /// <param name="includeContatos">true/false</param>
        /// <returns></returns>
        public Customer[] GetCustomerByDocument(string document, bool includeContatos = false)
        {
            IQueryable<Customer> query = context.Customers;

            if (includeContatos)
            {
                query = query.Include(a => a.ContactNumbers);
            }

            query = query.AsNoTracking()
                .OrderBy(a => a.Id)
                .Where(c => c.Document == document);

            return query.ToArray();
        }

        /// <summary>
        /// Procura por cliente
        /// </summary>
        /// <param name="searchInput">entrada de busca</param>
        /// <param name="includeContatos">true/false</param>
        /// <returns></returns>
        public Customer[] SearchCustomer(string searchInput, bool includeContatos = false) 
        {
            IQueryable<Customer> query = context.Customers;

            if (includeContatos)
            {
                query = query.Include(a => a.ContactNumbers);

            }
            query =  query.AsNoTracking()
                .OrderBy(a => a.Id).Where(c => c.Nome.ToLower().Contains(searchInput.ToLower()) ||
                 c.Email.ToLower().Contains(searchInput.ToLower()) ||
                 c.Document.Contains(searchInput) ||
                 c.PostalCode.Contains(searchInput));

            return query.ToArray();

        }

        /// <summary>
        /// Recupera contato por id
        /// </summary>
        /// <param name="contactId">id contato</param>
        /// <returns></returns>
        public ContactNumber GetContactById(int contactId)
        {
            IQueryable<ContactNumber> query = context.ContactNumbers;
            ContactNumber contact = query.AsNoTracking().FirstOrDefault(c => c.Id == contactId);
            return contact;
        }

        /// <summary>
        /// Verifica se documento é válido
        /// </summary>
        /// <param name="id">id </param>
        /// <param name="document">numero documento</param>
        /// <returns>true/false</returns>
        public bool IsDocumentUpdateValid(int id, string document) 
        {
            var alterCustomer = context.Customers.AsNoTracking().FirstOrDefault(c => c.Document == document && c.Id != id);

            if (alterCustomer != null) 
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Verifica se número de contato já está cadastrado para cliente
        /// </summary>
        /// <param name="idCustomer">idCliente</param>
        /// <param name="numeroContato">numero do contato</param>
        /// <returns>true/false</returns>
        public bool IsNumeroCadastradoForCustomer(int idCustomer, string numeroContato) 
        {
            var alterContact = context.ContactNumbers.AsNoTracking().FirstOrDefault(c => c.TelephoneNumber == numeroContato && c.CustomerId == idCustomer);

            if (alterContact != null) {
                return true;
            }

            return false;
        }

        
    }
}
