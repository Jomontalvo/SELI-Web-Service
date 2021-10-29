using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELI.Common.Models
{
    /// <summary>
    /// Tipo de persona en salvoconducto
    /// </summary>
    public enum PersonType : byte
    {
        Official = 0,
        Driver = 1,
        Public = 2
    }

    /// <summary>
    /// Género de la persona
    /// </summary>
    public enum Gender : byte
    {
        Male,
        Female
    }
    /// <summary>
    /// Modelo Persona
    /// </summary>
    public class Person
    {

        /// <summary>
        /// Cédula de identidad o pasaporte
        /// </summary>
        public string DocumentId { get; set; }
        /// <summary>
        /// Apellidos
        /// </summary>
        public string Surnames { get; set; }
        /// <summary>
        /// Nombres
        /// </summary>
        public string Names { get; set; }
        /// <summary>
        /// Nombre completo
        /// </summary>
        public string FullName => $"{Surnames}, {Names}";
        /// <summary>
        /// Género
        /// </summary>
        public Gender Gender { get; set; }
        /// <summary>
        /// Tipo de persona
        /// </summary>
        public PersonType Type { get; set; }
    }
}
