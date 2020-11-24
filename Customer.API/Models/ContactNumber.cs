using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CustomerVet.API.Models
{
    /// <summary>
    /// Model numero contato
    /// </summary>
    public class ContactNumber
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public ContactNumber() { }

        /// <summary>
        /// Construtor 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customerId"></param>
        /// <param name="telephoneNumber"></param>
        public ContactNumber(int id, int customerId, string telephoneNumber)
        {
            Id = id;
            CustomerId = customerId;
            TelephoneNumber = telephoneNumber;
        }

        /// <summary>
        /// Identificador
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id de cliente associado
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Cliente associado ao contato
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Numero do telefone 
        /// </summary>
        public String TelephoneNumber { get; set; }

    }
}
