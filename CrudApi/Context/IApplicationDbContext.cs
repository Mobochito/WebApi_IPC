using CrudApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrudApi.Context
{
    public interface IApplicationDbContext
    {
        DbSet<Empleado> Empleados { get; set; }
        DbSet<Perfil> Perfiles { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
