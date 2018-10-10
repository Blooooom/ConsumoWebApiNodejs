using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tienda.Models
{
    public class DetalleVenta
    {
        public int IdVenta { get; set; }
        public double TotalVenta { get; set; }
        public List<ProductoDetalleVentaModel> Productos { get; set; }
    }
}