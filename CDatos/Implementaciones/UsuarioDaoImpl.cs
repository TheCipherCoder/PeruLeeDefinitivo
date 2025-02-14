using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CEntidad;
using Microsoft.Data.SqlClient;

namespace CDatos.Implementaciones
{
    public class UsuarioDaoImpl : IDao<Usuario>
    {
        //private readonly Conexion _conexion;

        //public UsuarioDaoImpl(Conexion conexion)
        //{
        //    _conexion = conexion;
        //}
        private const string INSERT = "INSERT INTO tbl_usuario (nombre, apellido, email, contrasena, telefono, imagen, rol) VALUES (@Nombre, @Apellido, @Email, @Contrasena, @Telefono, @Imagen, @Rol)";
        private const string UPDATE = "UPDATE tbl_usuario SET nombre = @Nombre, apellido = @Apellido, email = @Email, contrasena = @Contrasena, telefono = @Telefono, imagen = @Imagen, rol = @Rol WHERE id_usuario = @IdUsuario";
        private const string DELETE = "DELETE FROM tbl_usuario WHERE id_usuario = @IdUsuario";
        private const string SELECT_ALL = "SELECT id_usuario, nombre, apellido, email, contrasena, telefono, imagen, rol, fecha_registro FROM tbl_usuario";
        private const string SELECT_BY_ID = "SELECT id_usuario, nombre, apellido, email, contrasena, telefono, imagen, rol, fecha_registro FROM tbl_usuario WHERE id_usuario = @IdUsuario";
        private const string SELECT_BY_CREDENTIALS = "SELECT id_usuario, nombre, apellido, email, contrasena, telefono, imagen, rol, fecha_registro FROM tbl_usuario WHERE email = @Email AND contrasena = @Contrasena";

        public void Crear(Usuario entity)
        {
            using (SqlConnection con = new SqlConnection(Conexion.Cn))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(INSERT, con))
                {
                    cmd.Parameters.AddWithValue("@Nombre", entity.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", entity.Apellido);
                    cmd.Parameters.AddWithValue("@Email", entity.Email);
                    cmd.Parameters.AddWithValue("@Contrasena", entity.Contrasena);
                    cmd.Parameters.AddWithValue("@Telefono", (object)entity.Telefono ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Imagen", entity.Imagen);
                    cmd.Parameters.AddWithValue("@Rol", entity.Rol);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Actualizar(Usuario entity)
        {
            using (SqlConnection con = new SqlConnection(Conexion.Cn))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(UPDATE, con))
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", entity.IdUsuario);
                    cmd.Parameters.AddWithValue("@Nombre", entity.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", entity.Apellido);
                    cmd.Parameters.AddWithValue("@Email", entity.Email);
                    cmd.Parameters.AddWithValue("@Contrasena", entity.Contrasena);
                    cmd.Parameters.AddWithValue("@Telefono", (object)entity.Telefono ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Imagen", entity.Imagen);
                    cmd.Parameters.AddWithValue("@Rol", entity.Rol);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Eliminar(int id)
        {
            using (SqlConnection con = new SqlConnection(Conexion.Cn))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(DELETE, con))
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Usuario> Listar()
        {
            List<Usuario> usuarios = new List<Usuario>();
            using (SqlConnection con = new SqlConnection(Conexion.Cn))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(SELECT_ALL, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuarios.Add(new Usuario
                            {
                                IdUsuario = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Apellido = reader.GetString(2),
                                Email = reader.GetString(3),
                                Contrasena = reader.GetString(4),
                                Telefono = reader.IsDBNull(5) ? null : reader.GetString(5),
                                Imagen = reader.GetString(6),
                                Rol = reader.GetInt32(7),
                                FechaRegistro = reader.GetDateTime(8)
                            });
                        }
                    }
                }
            }
            return usuarios;
        }

        public Usuario Obtener(int? id)
        {
            Usuario usuario = null;
            using (SqlConnection con = new SqlConnection(Conexion.Cn))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(SELECT_BY_ID, con))
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = new Usuario
                            {
                                IdUsuario = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Apellido = reader.GetString(2),
                                Email = reader.GetString(3),
                                Contrasena = reader.GetString(4),
                                Telefono = reader.IsDBNull(5) ? null : reader.GetString(5),
                                Imagen = reader.GetString(6),
                                Rol = reader.GetInt32(7),
                                FechaRegistro = reader.GetDateTime(8)
                            };
                        }
                    }
                }
            }
            return usuario;
        }

        public Usuario ObtenerPorCredenciales(string email, string contrasena)
        {
            Usuario usuario = null;
            using (SqlConnection con = new SqlConnection(Conexion.Cn))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(SELECT_BY_CREDENTIALS, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Contrasena", contrasena);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = new Usuario
                            {
                                IdUsuario = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Apellido = reader.GetString(2),
                                Email = reader.GetString(3),
                                Contrasena = reader.GetString(4),
                                Telefono = reader.IsDBNull(5) ? null : reader.GetString(5),
                                Imagen = reader.GetString(6),
                                Rol = reader.GetInt32(7),
                                FechaRegistro = reader.GetDateTime(8)
                            };
                        }
                    }
                }
            }
            return usuario;
        }
    }
}