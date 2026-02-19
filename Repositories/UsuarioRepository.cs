using System.Data.SQLite;
using ParcialPeliculas.Models;

namespace ParcialPeliculas.Repositories
{
    public interface IUserRepository { Usuario GetByCredentials(string user, string pass); }

    public class UsuarioRepository : IUserRepository
    {
        private string _conn;
        public UsuarioRepository(IConfiguration conf) { _conn = conf.GetConnectionString("DefaultConnection"); }

        public Usuario GetByCredentials(string user, string pass)
        {
            Usuario u = null;
            using (var conn = new SQLiteConnection(_conn)) {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT Id, User, Rol FROM Usuarios WHERE User=@u AND Pass=@p;", conn);
                cmd.Parameters.AddWithValue("@u", user);
                cmd.Parameters.AddWithValue("@p", pass);
                using (var reader = cmd.ExecuteReader()) {
                    if (reader.Read()) {
                        u = new Usuario { Id = Convert.ToInt32(reader["Id"]), User = reader["User"].ToString(), Rol = reader["Rol"].ToString() };
                    }
                }
            }
            return u;
        }
    }
}