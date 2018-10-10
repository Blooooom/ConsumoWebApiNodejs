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
    public class DetalleVentaController : ApiController
    {
        // GET: api/DetalleVenta
        [HttpGet]
        public IHttpActionResult GetId(string fecha)
        {
            SQLConexion _conexion = new SQLConexion();
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            DataTableReader _dtr = null;
            List<DetalleVenta> _list = new List<DetalleVenta>();
            List<ProductoDetalleVentaModel> _dtv = new List<ProductoDetalleVentaModel>();
            DetalleVenta detalleVenta = new DetalleVenta();
            try
            {
                _conexion.Conectar(System.Configuration.ConfigurationManager.ConnectionStrings["MiBD"].ToString());
                _Parametros.Add(new SqlParameter("@fecha", fecha));
                _conexion.PrepararProcedimiento("sp_GetByDateTime2", _Parametros);
                _dtr = _conexion.EjecutarTableReader();
                if (_dtr.HasRows)
                {
                    
                    while (_dtr.Read())
                    {

                        //Se recuperan los valores de acuerdo al alias que se definio en el procedimiento almacenado
                        detalleVenta.IdVenta = Int32.Parse(_dtr["IdVenta"].ToString());
                        detalleVenta.TotalVenta = Double.Parse(_dtr["TotalCompra"].ToString());
                        ProductoDetalleVentaModel _producto = new ProductoDetalleVentaModel()
                        {
                            IdProducto = Int32.Parse(_dtr["IdProducto"].ToString()),
                            Nombre = _dtr["Nombre"].ToString(),
                            Cantidad = Int32.Parse(_dtr["Cantidad"].ToString()),
                            Precio = Double.Parse(_dtr["PrecioUnitario"].ToString())
                        };
                        _dtv.Add(_producto);
                        detalleVenta.Productos = _dtv;
                    }
                    
                    _dtr.Close();
                    _list.Add(detalleVenta);
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

        [HttpPost]
        public IHttpActionResult post(List<ProductoDetalleVentaModel> Lista)
        {
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            SQLConexion conn = new SQLConexion();
            var dt = new DataTable();
            dt.Columns.Add("IdProducto", typeof(Int32));
            dt.Columns.Add("Cantidad", typeof(Int32));
            //DataRow dc = dt.NewRow();
            try
            {
                for (int i = 0; i < Lista.Count; i++)
                {
                    DataRow dc = dt.NewRow();
                    dc[0] = Lista[i].IdProducto;
                    dc[1] = Lista[i].Cantidad;
                    dt.Rows.Add(dc);
                }

                conn.Conectar(System.Configuration.ConfigurationManager.ConnectionStrings["MiBD"].ToString());
                _Parametros.Add(new SqlParameter("@detalle", dt));
                _Parametros[0].SqlDbType = SqlDbType.Structured;
                _Parametros[0].TypeName = "dbo.Detalle";
                conn.PrepararProcedimiento("sp_Insert", _Parametros);
                conn.EjecutarTableReader();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Desconectar();
                conn = null;
            }
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult PutId(int idDetVen, int cantidad)
        {
            SQLConexion _conexion = new SQLConexion();
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            DataTableReader _dtr = null;
            try
            {
                _conexion.Conectar(System.Configuration.ConfigurationManager.ConnectionStrings["MiBD"].ToString());
                _Parametros.Add(new SqlParameter("@id", idDetVen));
                _Parametros.Add(new SqlParameter("@cant", cantidad));
                _conexion.PrepararProcedimiento("sp_Update", _Parametros);
                _dtr = _conexion.EjecutarTableReader();
                
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
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            SQLConexion _conexion = new SQLConexion();
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            DataTableReader _dtr = null;
            try
            {
                _conexion.Conectar(System.Configuration.ConfigurationManager.ConnectionStrings["MiBD"].ToString());
                _Parametros.Add(new SqlParameter("@id", id));
                _conexion.PrepararProcedimiento("sp_DeleteDet", _Parametros);
                _dtr = _conexion.EjecutarTableReader();

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
            return Ok();
        }
    }
}
