using Microsoft.AspNetCore.Mvc.Rendering;
using ParcialPeliculas.Models; // <-- ¡Cambiado! Antes decía Enums
using System.ComponentModel.DataAnnotations;

namespace ParcialPeliculas.ViewModels
{
    public class PeliculaIndexViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int Anio { get; set; }
        public string Categoria { get; set; }
    }

    public class PeliculaCreateViewModel
    {
        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Debe tener entre 2 y 100 caracteres")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "El año es obligatorio")]
        [Range(1895, 2025, ErrorMessage = "Año inválido (No futuro)")]
        public int Anio { get; set; }

        [Required(ErrorMessage = "Seleccione una categoría")]
        public Categoria Categoria { get; set; }

        public SelectList? ListaCategorias { get; set; }
    }

    public class PeliculaUpdateViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Titulo { get; set; }

        [Required]
        [Range(1895, 2025)]
        public int Anio { get; set; }

        [Required]
        public Categoria Categoria { get; set; }

        public SelectList? ListaCategorias { get; set; }
    }
}