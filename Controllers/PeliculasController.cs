using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParcialPeliculas.Models; // <-- ¡Actualizado! Trae a Pelicula, Usuario y Categoria
using ParcialPeliculas.Repositories;
using ParcialPeliculas.Services;
using ParcialPeliculas.ViewModels;

namespace ParcialCine.Controllers
{
    public class PeliculasController : Controller
    {
        private readonly IPeliculaRepository _repo;
        private readonly IAuthenticationService _auth;

        public PeliculasController(IPeliculaRepository repo, IAuthenticationService auth)
        {
            _repo = repo;
            _auth = auth;
        }

        // Helper privado para cargar el SelectList
        private SelectList ObtenerCategorias()
        {
            // El typeof(Categoria) ahora referencia a ParcialCine.Models.Categoria
            var items = Enum.GetValues(typeof(Categoria)).Cast<Categoria>()
                .Select(c => new SelectListItem { Value = c.ToString(), Text = c.ToString() }).ToList();
            return new SelectList(items, "Value", "Text");
        }
        
        // ... (El resto de los métodos Index, Create, Edit, Delete quedan exactamente iguales)