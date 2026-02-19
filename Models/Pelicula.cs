namespace ParcialCine.Models
{
    public class Pelicula
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int Anio { get; set; }
        public Categoria Categoria { get; set; } // Lo reconoce automáticamente porque están en la misma carpeta
    }
}