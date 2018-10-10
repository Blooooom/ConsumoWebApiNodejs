using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tienda.Models
{
    public class ProductoModel
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
    }
}