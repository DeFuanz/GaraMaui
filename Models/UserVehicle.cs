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
        public int user_vehicle_id { get; set; }
        public string UserId { get; set; } = default!;
        public int VehicleId { get; set; }

        public Vehicle? Vehicle { get; set; } = default!;
    }
}
