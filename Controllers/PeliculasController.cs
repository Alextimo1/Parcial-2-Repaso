using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParcialPeliculas.Models;
using ParcialPeliculas.Repositories;
using ParcialPeliculas.Services;
using ParcialPeliculas.ViewModels;

namespace ParcialPeliculas.Controllers
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

        private SelectList ObtenerCategorias()
        {
            var items = Enum.GetValues(typeof(Categoria)).Cast<Categoria>()
                .Select(c => new SelectListItem { Value = c.ToString(), Text = c.ToString() }).ToList();
            return new SelectList(items, "Value", "Text");
        }

        public IActionResult Index()
        {
            if (!_auth.IsAuthenticated()) return RedirectToAction("Login", "Acceso");

            var peliculas = _repo.GetAll();
            var viewModels = peliculas.Select(p => new PeliculaIndexViewModel
            {
                Id = p.Id,
                Titulo = p.Titulo,
                Anio = p.Anio,
                Categoria = p.Categoria.ToString()
            }).ToList();

            return View(viewModels);
        }

        public IActionResult Create()
        {
            if (!_auth.HasAccessLevel("Admin")) return RedirectToAction("Index");

            var vm = new PeliculaCreateViewModel { ListaCategorias = ObtenerCategorias() };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(PeliculaCreateViewModel vm)
        {
            if (!_auth.HasAccessLevel("Admin")) return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                _repo.Add(new Pelicula { Titulo = vm.Titulo, Anio = vm.Anio, Categoria = vm.Categoria });
                return RedirectToAction("Index");
            }
            vm.ListaCategorias = ObtenerCategorias();
            return View(vm);
        }

        // Agregar Edit y Delete (Siguen la misma lógica usando _repo.GetById, _repo.Update y _repo.Delete)
        public IActionResult Delete(int id)
        {
            if (!_auth.HasAccessLevel("Admin")) return RedirectToAction("Index");
            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}