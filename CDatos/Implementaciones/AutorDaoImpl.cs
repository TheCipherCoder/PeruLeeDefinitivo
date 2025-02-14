using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using CEntidad;

namespace CDatos.Implementaciones
{
    public class AutorDaoImpl : IDao<Autor>
    {
        //private readonly Conexion conexion;

        //public AutorDaoImpl(Conexion conexion)
        //{
        //    conexion = conexion;
        //}

        public Autor Obtener(int? id)
        {
            Autor autor = null;
            using (SqlConnection con = new SqlConnection(Conexion.Cn))
            {
                con.Open();
                string query = "SELECT * FROM tbl_autor WHERE id_autor = @Id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            autor = new Autor
                            {
                                IdAutor = (int)reader["id_autor"],
                                Nombre = reader["nombre"].ToString(),
                                Apellido = reader["apellido"].ToString()
                            };
                        }
                    }
                }
            }
            return autor;
        }

        public List<Autor> Listar()
        {
            List<Autor> autores = new List<Autor>();
            using (SqlConnection con = new SqlConnection(Conexion.Cn))
            {
                con.Open();
                string query = "SELECT * FROM tbl_autor";
                using (SqlCommand cmd = new SqlCommand(query, con))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        autores.Add(new Autor
                        {
                            IdAutor = (int)reader["id_autor"],
                            Nombre = reader["nombre"].ToString(),
                            Apellido = reader["apellido"].ToString()
                        });
                    }
                }
            }
            return autores;
        }

        public void Crear(Autor entity)
        {
            using (SqlConnection con = new SqlConnection(Conexion.Cn))
            {
                con.Open();
                string query = "INSERT INTO tbl_autor (nombre, apellido) VALUES (@Nombre, @Apellido)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Nombre", entity.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", entity.Apellido);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Actualizar(Autor entity)
        {
            using (SqlConnection con = new SqlConnection(Conexion.Cn))
            {
                con.Open();
                string query = "UPDATE tbl_autor SET nombre = @Nombre, apellido = @Apellido WHERE id_autor = @Id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Nombre", entity.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", entity.Apellido);
                    cmd.Parameters.AddWithValue("@Id", entity.IdAutor);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Eliminar(int id)
        {
            using (SqlConnection con = new SqlConnection(Conexion.Cn))
            {
                con.Open();
                string query = "DELETE FROM tbl_autor WHERE id_autor = @Id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
