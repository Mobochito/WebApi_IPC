namespace CrudApi.Entities
{
    public class Empleado
    {
        public int IdEmpleado { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public int Sueldo { get; set; }
        public int IdPerfil { get; set; }
        public virtual Perfil PerfilReferencia { get; set; } = null!;

    }
}
