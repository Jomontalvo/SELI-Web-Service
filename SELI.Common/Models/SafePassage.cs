using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SELI.Common.Models
{
    /// <summary>
    /// Estado del salvoconducto
    /// </summary>
    public enum Status : byte
    {
        /// <summary>
        /// Salvoconducto vigente
        /// </summary>
        Active,
        /// <summary>
        /// Salvoconducto vencido
        /// </summary>
        Expired,
        /// <summary>
        /// Salvoconducto cancelado
        /// </summary>
        Canceled
    }

    public class SafePassage
    {
        /// <summary>
        /// Identity secuencial de salvoconductos (interno de la Base de Datos)
        /// </summary>
        [Required]
        public int Id { get; set; }
        /// <summary>
        /// Número de código salvoconducto 
        /// </summary>
        public int CodeId { get; set; }
        /// <summary>
        /// Código de salvoconducto de la entidad externa (correspondencia)
        /// </summary>
        public int SafePassageForeignId { get; set; }
        /// <summary>
        /// Fecha de emisión del salvoconducto
        /// </summary>
        public DateTime EmissionDate { get; set; }
        /// <summary>
        /// Fecha y hora de inicio de vigencia del salvoconducto
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// Fecha y hora de finalización de vigencia del salvoconducto
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// Estado del salvoconducto
        /// </summary>
        public Status Status { get; set; }
        /// <summary>
        /// Lugar de destino de la misión
        /// </summary>
        public string DestinationPlace { get; set; }
        /// <summary>
        /// Motivos de la misión
        /// </summary>
        public string Reasons { get; set; }
        /// <summary>
        /// Direción URL para consulta del salvoconducto (QR Code)
        /// </summary>
        public string UrlQRCode { get; set; }
        /// <summary>
        /// Indicador si vehículo asignado es institucional o prestado
        /// </summary>
        public bool IsLending { get; set; }
        /// <summary>
        /// Institución que prestó el vehículo
        /// </summary>
        public Entity LendedBy { get; set; }
        /// <summary>
        /// Funcionario solicitante del salvoconducto
        /// </summary>
        public Officer Requester { get; set; }
        /// <summary>
        /// Listado de conductores
        /// </summary>
        public List<Person> Drivers { get; set; }
        /// <summary>
        /// Vehículo asignado a la misión
        /// </summary>
        public Vehicle Vehicle { get; set; }
        /// <summary>
        /// Lista de pasajeros 
        /// </summary>
        public List<Person> Passengers { get; set; }
    }
}
