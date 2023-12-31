using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara.Models
{
    public class GasFillUp
    {
        public int GasFillUpId { get; set; }

        [DataType(DataType.Date)]
        public DateTime FillUpDate { get; set; }

        public decimal GallonsFilled { get; set; }

        public decimal PricePerGallon { get; set; }
    }
}
