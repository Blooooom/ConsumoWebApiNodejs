using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tienda.Models
{
    public class VentaModel
    {
        public int IdVenta { get; set; }
        public DateTime FechayHora { get; set; }
        public string TotalVenta { get; set; }
    }
}