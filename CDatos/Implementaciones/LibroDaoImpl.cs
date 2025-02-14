using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using CEntidad;

namespace CDatos.Implementaciones
{
    public class LibroDaoImpl : IDao<Libro>
    {

        //private readonly Conexion conexion;

        //public LibroDaoImpl(Conexion conexion)
        //{
        //    conexion = conexion;
        //}

        public Libro Obtener(int? id)
        {
            Libro libro = null;
            using (SqlConnection con = new SqlConnection(Conexion.Cn))
            {
                con.Open();
                string query = "SELECT * FROM tbl_libro WHERE id_libro = @Id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            libro = new Libro
                            {
                                IdLibro = (int)reader["id_libro"],
                                Titulo = reader["titulo"].ToString(),
                                Caratula = reader["caratula"].ToString(),
                                FechaPublicacion = reader["fecha_publicacion"] as DateTime?,
                                IdCategoriaFk = reader["id_categoria_fk"] as int?,
                                IdAutorFk = reader["id_autor_fk"] as int?,
                                CopiasDisponibles = (int)reader["copias_disponibles"],
                                Estado = reader["estado"].ToString()
                            };
                        }
                    }
                }
            }
            return libro;
        }

        public List<Libro> Listar()
        {
            List<Libro> libros = new List<Libro>();
            using (SqlConnection con = new SqlConnection(Conexion.Cn))
            {
                con.Open();
                string query = "SELECT * FROM tbl_libro";
                using (SqlCommand cmd = new SqlCommand(query, con))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        libros.Add(new Libro
                        {
                            IdLibro = (int)reader["id_libro"],
                            Titulo = reader["titulo"].ToString(),
                            Caratula = reader["caratula"].ToString(),
                            FechaPublicacion = reader["fecha_publicacion"] as DateTime?,
                            IdCategoriaFk = reader["id_categoria_fk"] as int?,
                            IdAutorFk = reader["id_autor_fk"] as int?,
                            CopiasDisponibles = (int)reader["copias_disponibles"],
                            Estado = reader["estado"].ToString()
                        });
                    }
                }
            }
            return libros;
        }

        public void Crear(Libro entity)
        {
            using (SqlConnection con = new SqlConnection(Conexion.Cn))
            {
                con.Open();
                string query = "INSERT INTO tbl_libro (titulo, caratula, fecha_publicacion, id_categoria_fk, id_autor_fk, copias_disponibles, estado) " +
                               "VALUES (@Titulo, @Caratula, @FechaPublicacion, @IdCategoria, @IdAutor, @CopiasDisponibles, @Estado)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Titulo", entity.Titulo);
                    cmd.Parameters.AddWithValue("@Caratula", entity.Caratula ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@FechaPublicacion", entity.FechaPublicacion ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@IdCategoria", entity.IdCategoriaFk ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@IdAutor", entity.IdAutorFk ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CopiasDisponibles", entity.CopiasDisponibles);
                    cmd.Parameters.AddWithValue("@Estado", entity.Estado);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Actualizar(Libro entity)
        {
            using (SqlConnection con = new SqlConnection(Conexion.Cn))
            {
                con.Open();
                string query = "UPDATE tbl_libro SET titulo = @Titulo, caratula = @Caratula, fecha_publicacion = @FechaPublicacion, " +
                               "id_categoria_fk = @IdCategoria, id_autor_fk = @IdAutor, copias_disponibles = @CopiasDisponibles, estado = @Estado " +
                               "WHERE id_libro = @IdLibro";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Titulo", entity.Titulo);
                    cmd.Parameters.AddWithValue("@Caratula", entity.Caratula ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@FechaPublicacion", entity.FechaPublicacion ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@IdCategoria", entity.IdCategoriaFk ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@IdAutor", entity.IdAutorFk ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CopiasDisponibles", entity.CopiasDisponibles);
                    cmd.Parameters.AddWithValue("@Estado", entity.Estado);
                    cmd.Parameters.AddWithValue("@IdLibro", entity.IdLibro);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Eliminar(int id)
        {
            using (SqlConnection con = new SqlConnection(Conexion.Cn))
            {
                con.Open();
                string query = "DELETE FROM tbl_libro WHERE id_libro = @Id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
