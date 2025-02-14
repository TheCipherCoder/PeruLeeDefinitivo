using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using CEntidad;

namespace CDatos.Implementaciones
{
    public class CategoriaDaoImpl : IDao<Categoria>
    {

        //private readonly Conexion conexion;

        //public CategoriaDaoImpl(Conexion conexion)
        //{
        //    conexion = conexion;
        //}
        public Categoria Obtener(int id)
        {
            Categoria categoria = null;
            using (SqlConnection con = new SqlConnection(Conexion.Cn))
            {
                con.Open();
                string query = "SELECT * FROM tbl_categoria WHERE id_categoria = @Id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            categoria = new Categoria
                            {
                                IdCategoria = (int)reader["id_categoria"],
                                Nombre = reader["nombre"].ToString()
                            };
                        }
                    }
                }
            }
            return categoria;
        }

        public List<Categoria> Listar()
        {
            List<Categoria> categorias = new List<Categoria>();
            using (SqlConnection con = new SqlConnection(Conexion.Cn))
            {
                con.Open();
                string query = "SELECT * FROM tbl_categoria";
                using (SqlCommand cmd = new SqlCommand(query, con))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categorias.Add(new Categoria
                        {
                            IdCategoria = (int)reader["id_categoria"],
                            Nombre = reader["nombre"].ToString()
                        });
                    }
                }
            }
            return categorias;
        }

        public void Crear(Categoria entity)
        {
            using (SqlConnection con = new SqlConnection(Conexion.Cn))
            {
                con.Open();
                string query = "INSERT INTO tbl_categoria (nombre) VALUES (@Nombre)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Nombre", entity.Nombre);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Actualizar(Categoria entity)
        {
            using (SqlConnection con = new SqlConnection(Conexion.Cn))
            {
                con.Open();
                string query = "UPDATE tbl_categoria SET nombre = @Nombre WHERE id_categoria = @Id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Nombre", entity.Nombre);
                    cmd.Parameters.AddWithValue("@Id", entity.IdCategoria);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Eliminar(int id)
        {
            using (SqlConnection con = new SqlConnection(Conexion.Cn))
            {
                con.Open();
                string query = "DELETE FROM tbl_categoria WHERE id_categoria = @Id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
