using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELI.Common.Models
{
    public class Vehicle
    {
        /// <summary>
        /// Placa del vehículo
        /// </summary>
        [Required]
        public string LicensePlate { get; set; }
        /// <summary>
        /// Marca del vehículo
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// Modelo del vehículo
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Tipo de vehículo
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Número de pasajeros
        /// </summary>
        public byte NumberOfPassengers { get; set; }
        /// <summary>
        /// Fecha de fabricación
        /// </summary>
        public DateTime ManufacturingDate { get; set; }
        /// <summary>
        /// Antiguedad del vehículo
        /// </summary>
        public string Antiquity => $"{DateTime.Today.Year - ManufacturingDate.Year} años.";
    }
}
