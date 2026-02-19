using System.Data.SQLite;
using ParcialPeliculas.Models; // <-- Con esto alcanza para Models y el Enum Categoria

namespace ParcialCine.Repositories
{
    public class PeliculaRepository : IPeliculaRepository
    {
        private readonly string _connectionString;

        public PeliculaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Pelicula> GetAll()
        {
            var list = new List<Pelicula>();
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT Id, Titulo, Anio, Categoria FROM Peliculas ORDER BY Titulo ASC;", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Pelicula
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Titulo = reader["Titulo"].ToString(),
                            Anio = Convert.ToInt32(reader["Anio"]),
                            // Enum.Parse funciona perfecto porque la clase y el Enum comparten namespace
                            Categoria = Enum.Parse<Categoria>(reader["Categoria"].ToString())
                        });
                    }
                }
            }
            return list;
        }

        // ... (Los métodos Add, Update, Delete y GetById quedan exactamente iguales al código anterior)

        public void Add(Pelicula pelicula)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                [cite_start]// SQL del PDF [cite: 78]
                var command = new SQLiteCommand("INSERT INTO Peliculas (Titulo, Anio, Categoria) VALUES (@Titulo, @Anio, @Categoria);", connection);
                command.Parameters.AddWithValue("@Titulo", pelicula.Titulo);
                command.Parameters.AddWithValue("@Anio", pelicula.Anio);
                command.Parameters.AddWithValue("@Categoria", pelicula.Categoria.ToString()); // Guardamos como texto
                command.ExecuteNonQuery();
            }
        }

        public void Update(Pelicula pelicula)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                [cite_start]// SQL del PDF [cite: 80]
                var command = new SQLiteCommand("UPDATE Peliculas SET Titulo = @Titulo, Anio = @Anio, Categoria = @Categoria WHERE Id = @Id;", connection);
                command.Parameters.AddWithValue("@Titulo", pelicula.Titulo);
                command.Parameters.AddWithValue("@Anio", pelicula.Anio);
                command.Parameters.AddWithValue("@Categoria", pelicula.Categoria.ToString());
                command.Parameters.AddWithValue("@Id", pelicula.Id);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                [cite_start]// SQL del PDF [cite: 82]
                var command = new SQLiteCommand("DELETE FROM Peliculas WHERE Id = @Id;", connection);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }

        public Pelicula GetById(int id)
        {
            Pelicula pelicula = null;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                [cite_start]// SQL del PDF [cite: 75]
                var command = new SQLiteCommand("SELECT Id, Titulo, Anio, Categoria FROM Peliculas WHERE Id = @Id;", connection);
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        pelicula = new Pelicula
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Titulo = reader["Titulo"].ToString(),
                            Anio = Convert.ToInt32(reader["Anio"]),
                            Categoria = Enum.Parse<Categoria>(reader["Categoria"].ToString())
                        };
                    }
                }
            }
            return pelicula;
        }
    }
}