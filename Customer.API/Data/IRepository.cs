using CustomerVet.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerVet.API.Data
{
    /// <summary>
    /// Interface do repositório
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Adicionar
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        void Add<T>(T entity) where T:class;

        /// <summary>
        /// Atualizar
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        void Update<T>(T entity) where T : class;

        /// <summary>
        /// Remover
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        void Delete<T>(T entity) where T : class;

        /// <summary>
        /// Salvar mudanças
        /// </summary>
        /// <returns></returns>
        bool SaveChanges();

        /// <summary>
        /// Recupera todos os clientes
        /// </summary>
        /// <param name="todosContatos"></param>
        /// <returns></returns>
        Customer[] GetAllCustomers(bool todosContatos);

        /// <summary>
        /// Recupera cliente por Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="todosContatos"></param>
        /// <returns></returns>
        Customer[] GetCustomerById(int id, bool todosContatos);

        /// <summary>
        /// Recupera cliente por documento
        /// </summary>
        /// <param name="document"></param>
        /// <param name="todosContatos"></param>
        /// <returns></returns>
        Customer[] GetCustomerByDocument(string document, bool todosContatos);
        /// <summary>
        /// Verifica se documento é valido
        /// </summary>
        /// <param name="id"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        public bool IsDocumentUpdateValid(int id, string document);

        /// <summary>
        /// Verifica se número já foi cadastrado para cliente
        /// </summary>
        /// <param name="idCustomer"></param>
        /// <param name="numeroContato"></param>
        /// <returns></returns>
        public bool IsNumeroCadastradoForCustomer(int idCustomer, string numeroContato);

        /// <summary>
        /// Recupera contato por id
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        public ContactNumber GetContactById(int contactId);

        /// <summary>
        /// Busca por cliente
        /// </summary>
        /// <param name="searchInput"></param>
        /// <param name="todosContatos"></param>
        /// <returns></returns>
        public Customer[] SearchCustomer(string searchInput, bool todosContatos);
    }
}
