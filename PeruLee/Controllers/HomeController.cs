using System.Data;
using System.Diagnostics;
using CDatos;
using CDatos.Implementaciones;
using CPresentacion.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PeruLee.Models;

namespace PeruLee.Controllers
{
    public class HomeController : Controller
    {
        private readonly LibroDaoImpl _libroDao;
        private readonly ILogger<HomeController> _logger;

        public HomeController(LibroDaoImpl libroDao, ILogger<HomeController> logger)
        {
            _libroDao = libroDao;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Biblioteca()
        {
            var listaLibros = _libroDao.Listar();
            return View(listaLibros);
        }
        [HttpGet]
        public IActionResult Mision()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "2")]
        public IActionResult Prestamo()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "2")]
        public IActionResult Prestamo(int idUsuario, int idLibro, DateTime fechaDevolucion)
        {
            string mensaje;

            using (SqlConnection conn = new SqlConnection(Conexion.Cn))
            {
                using (SqlCommand cmd = new SqlCommand("sp_prestar_libro", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_usuario", idUsuario);
                    cmd.Parameters.AddWithValue("@id_libro", idLibro);
                    cmd.Parameters.AddWithValue("@fecha_devolucion", fechaDevolucion);
                    SqlParameter outputParam = new SqlParameter("@mensaje", SqlDbType.NVarChar, 200)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        mensaje = outputParam.Value.ToString();
                    }
                    catch (Exception ex)
                    {
                        mensaje = "Error: " + ex.Message;
                    }
                }
            }
            ViewBag.Mensaje = mensaje;
            return View();
        }


        [HttpGet]
        [Authorize(Roles = "2")]
        public IActionResult ListarPrestamos(string estado = null)
        {
            List<PrestamoViewModel> prestamos = new List<PrestamoViewModel>();

            using (SqlConnection conn = new SqlConnection(Conexion.Cn))
            {
                using (SqlCommand cmd = new SqlCommand("sp_consultar_prestamos", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@estado", (object)estado ?? DBNull.Value);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            prestamos.Add(new PrestamoViewModel
                            {
                                IdPrestamo = reader.GetInt32(0),
                                IdUsuario = reader.GetInt32(1),
                                NombreUsuario = reader.GetString(2),
                                Email = reader.GetString(3),
                                IdLibro = reader.GetInt32(4),
                                TituloLibro = reader.GetString(5),
                                Autor = reader.GetString(6),
                                Categoria = reader.GetString(7),
                                FechaPrestamo = reader.GetDateTime(8),
                                FechaDevolucion = reader.GetDateTime(9),
                                FechaDevolucionReal = reader.IsDBNull(10) ? (DateTime?)null : reader.GetDateTime(10),
                                EstadoPrestamo = reader.GetString(11),
                                Alerta = reader.GetString(12)
                            });
                        }
                    }
                }
            }

            return View(prestamos);
        }

        [HttpGet]
        [Authorize(Roles = "2")]
        public IActionResult ListarSolicitudes()
        {
            List<SolicitudViewModel> solicitudes = new List<SolicitudViewModel>();

            using (SqlConnection con = new SqlConnection(Conexion.Cn))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("EXEC sp_listar_solicitudes", con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    solicitudes.Add(new SolicitudViewModel
                    {
                        IdSolicitud = reader.GetInt32(0),
                        IdUsuario = reader.GetInt32(1),
                        NombreUsuario = reader.GetString(2),
                        Email = reader.GetString(3),
                        Telefono = reader.GetString(4),
                        IdLibro = reader.GetInt32(5),
                        TituloLibro = reader.GetString(6),
                        Autor = reader.GetString(7),
                        Categoria = reader.GetString(8),
                        CopiasDisponibles = reader.GetInt32(9),
                        FechaSolicitud = reader.GetDateTime(10),
                        FechaExpiracion = reader.IsDBNull(11) ? (DateTime?)null : reader.GetDateTime(11),
                        FechaProcesamiento = reader.IsDBNull(12) ? (DateTime?)null : reader.GetDateTime(12),
                        EstadoSolicitud = reader.GetString(13),
                        DiasEspera = reader.GetInt32(15),
                        PosicionCola = reader.IsDBNull(16) ? (int?)null : reader.GetInt32(16)
                    });
                }


            }

            ViewBag.Solicitudes = solicitudes;
            return View();
        }
        [HttpPost]
        public IActionResult Solicitar([FromBody] SolicitudRequest request)
        {
            if (request == null || request.IdUsuario <= 0 || request.IdLibro <= 0)
            {
                return Json(new { exito = false, mensaje = "Datos inválidos." });
            }

            string mensaje = "";
            using (SqlConnection conexion = new SqlConnection(Conexion.Cn))
            {
                using (SqlCommand comando = new SqlCommand("sp_solicitar", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@id_usuario", request.IdUsuario);
                    comando.Parameters.AddWithValue("@id_libro", request.IdLibro);
                    SqlParameter mensajeParam = new SqlParameter("@mensaje", SqlDbType.NVarChar, 200)
                    {
                        Direction = ParameterDirection.Output
                    };
                    comando.Parameters.Add(mensajeParam);

                    try
                    {
                        conexion.Open();
                        comando.ExecuteNonQuery();
                        mensaje = mensajeParam.Value.ToString();
                        return Json(new { exito = true, mensaje });
                    }
                    catch (Exception ex)
                    {
                        return Json(new { exito = false, mensaje = "Error: " + ex.Message });
                    }
                }
            }
        }

        [HttpGet]
        [Authorize(Roles = "1")]
        public IActionResult Libro()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "1")]
        public IActionResult Categoria()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "1")]
        public IActionResult Autor()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "1")]
        public IActionResult Usuario()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AccesoDenegado()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}