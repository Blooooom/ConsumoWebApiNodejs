using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tienda.Models
{
    public class ProductoDetalleVentaModel
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
    }
}