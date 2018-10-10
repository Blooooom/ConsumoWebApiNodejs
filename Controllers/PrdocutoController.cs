using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tienda.Models;

namespace Tienda.Controllers
{
    public class PrdocutoController : ApiController
    {
        // GET: api/Prdocuto
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            SQLConexion _conexion = new SQLConexion();
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            DataTableReader _dtr = null;
            List<ProductoModel> _list = new List<ProductoModel>();
            try
            {
                _conexion.Conectar(System.Configuration.ConfigurationManager.ConnectionStrings["MiBD"].ToString());
                _Parametros.Add(new SqlParameter("@id", id));
                _conexion.PrepararProcedimiento("sp_GetProductById", _Parametros);
                _dtr = _conexion.EjecutarTableReader();
                if (_dtr.HasRows)
                {
                    while (_dtr.Read())
                    {
                        ProductoModel _producto = new ProductoModel()
                        {
                            //Se recuperan los valores de acuerdo al alias que se definio en el procedimiento almacenado
                            IdProducto = Int32.Parse(_dtr["IdProducto"].ToString()),
                            Nombre = _dtr["Nombre"].ToString(),
                            Precio = Double.Parse(_dtr["Precio"].ToString())
                        };
                        _list.Add(_producto); 
                    }
                    _dtr.Close();
                    return Ok(_list);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                _conexion.Desconectar();
                _conexion = null;
                _dtr = null;
            }
            return Ok(_list);
        }
    }
}
