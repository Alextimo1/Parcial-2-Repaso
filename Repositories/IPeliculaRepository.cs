using System.Collections.Generic;
using ParcialPeliculas.Models;

namespace ParcialPeliculas.Repositories
{
    public interface IPeliculaRepository
    {
        List<Pelicula> GetAll();
        Pelicula GetById(int id);
        void Add(Pelicula pelicula);
        void Update(Pelicula pelicula);
        void Delete(int id);
    }
}