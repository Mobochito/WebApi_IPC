using System.ComponentModel.DataAnnotations;

namespace CrudApi.DTOs
{
    public class PerfilDTO
    {
        public int IdPerfil { get; set; }

        [Required(ErrorMessage = "El nombre del perfil es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;
    }
}
