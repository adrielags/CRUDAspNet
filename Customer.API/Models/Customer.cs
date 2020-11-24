using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerVet.API.Models
{
    /// <summary>
    /// Model do cliente 
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public Customer() { }
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="nome">Nome</param>
        /// <param name="document">Documento</param>
        /// <param name="postalCode">CEP</param>
        /// <param name="email">Email</param>
        public Customer(int id, string nome, string document, string postalCode, string email)
        {
            Id = id;
            Nome = nome;
            Document = document;
            PostalCode = postalCode;
            Email = email;
        }

        /// <summary>
        /// Identificador
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nome do cliente
        /// </summary>
        [Required(ErrorMessage = "O nome do usuário é obrigatório", AllowEmptyStrings = false)]
        [StringLength(100)]
        public String Nome { get; set; }

        /// <summary>
        /// Documento CEP OU CNPJ
        /// </summary>
        [Required(ErrorMessage = "O campo documento é obrigatório", AllowEmptyStrings = false)]
        public String Document { get; set; }

        /// <summary>
        /// Código Postal
        /// </summary>
        public String PostalCode { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required(ErrorMessage = "O campo e-mail é obrigatório", AllowEmptyStrings = false)]
        public String Email { get; set; }

        /// <summary>
        /// Estado ativo ou inativo
        /// </summary>
        [Required(ErrorMessage = "O campo estado é obrigatório", AllowEmptyStrings = false)]
        public String State { get; set;}

        /// <summary>
        /// Numeros de contato
        /// </summary>
        public IEnumerable<ContactNumber> ContactNumbers { get; set; }
    }
}
