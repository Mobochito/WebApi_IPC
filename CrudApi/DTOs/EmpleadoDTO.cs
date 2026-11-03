using System.ComponentModel.DataAnnotations;

namespace CrudApi.DTOs
{
    public class EmpleadoDTO
    {
        public int IdEmpleado { get; set; }

        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        public string NombreCompleto { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "El sueldo debe ser mayor que cero.")]
        public int Sueldo { get; set; }= 0;

        [Range(1, int.MaxValue, ErrorMessage = "El perfil es obligatorio.")]
        public int IdPerfil { get; set; }= 0;

        public string NombrePerfil { get; set; } = string.Empty;
    }
}
