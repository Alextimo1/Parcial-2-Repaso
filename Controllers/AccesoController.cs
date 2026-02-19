using Microsoft.AspNetCore.Mvc;
using ParcialPeliculas.Repositories;

namespace ParcialPeliculas.Controllers
{
    public class AccesoController : Controller
    {
        private readonly IUserRepository _repo;
        public AccesoController(IUserRepository repo) { _repo = repo; }

        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string user, string pass)
        {
            var u = _repo.GetByCredentials(user, pass);
            if (u != null) {
                HttpContext.Session.SetString("User", u.User);
                HttpContext.Session.SetString("Rol", u.Rol);
                return RedirectToAction("Index", "Peliculas");
            }
            ViewBag.Error = "Datos incorrectos";
            return View();
        }
    }
}