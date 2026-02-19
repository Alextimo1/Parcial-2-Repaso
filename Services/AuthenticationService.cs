namespace ParcialPeliculas.Services
{
    public interface IAuthenticationService
    {
        bool IsAuthenticated();
        bool HasAccessLevel(string rol);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpContextAccessor _ctx;
        public AuthenticationService(IHttpContextAccessor ctx) { _ctx = ctx; }
        
        public bool IsAuthenticated() => !string.IsNullOrEmpty(_ctx.HttpContext.Session.GetString("User"));
        public bool HasAccessLevel(string rol) => _ctx.HttpContext.Session.GetString("Rol") == rol;
    }
}