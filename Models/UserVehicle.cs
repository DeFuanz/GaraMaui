using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara.Models
{
    public class UserVehicle
    {
        public int UserVehicleId { get; set; }
        public string UserId { get; set; } = default!;
        public int VehicleId { get; set; }

        public string? VehicleName { get; set; }

        public decimal TotalGasCost { get; set; } = 0.00m;

        public decimal TotalMileage { get; set; } = 0.00m;

        public Vehicle? Vehicle { get; set; } = default!;
        public List<GasFillUp>? GasFillUp { get; set; } = default!;
    }
}
