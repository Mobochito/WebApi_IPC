using CrudApi.Context;
using CrudApi.DTOs;
using CrudApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudApi.Service
{
    public class EmpleadoService
    {
        //private readonly AppDbMysqlContext _context;
        private readonly IApplicationDbContext _context;

        public EmpleadoService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmpleadoDTO>> Lista()
        {
            List<EmpleadoDTO> listaDTO = new List<EmpleadoDTO>();
            foreach (var item in await _context.Empleados.Include(p => p.PerfilReferencia).ToListAsync())
            {
                listaDTO.Add(new EmpleadoDTO()
                {
                    IdEmpleado = item.IdEmpleado,
                    NombreCompleto = item.NombreCompleto,
                    Sueldo = item.Sueldo,
                    IdPerfil = item.IdPerfil,
                    NombrePerfil = item.PerfilReferencia.Nombre
                });
            }
            return listaDTO;
        }

        public async Task<EmpleadoDTO> Buscar(int id)
        {
            var empleadoDTO = new EmpleadoDTO();
            var empleadoDB = await _context.Empleados.Include(p => p.PerfilReferencia)
                .Where(e => e.IdEmpleado == id).FirstOrDefaultAsync();

            if (empleadoDB is null)
            {
                return null;
            }

            empleadoDTO.IdEmpleado = empleadoDB.IdEmpleado;
            empleadoDB.NombreCompleto = empleadoDB.NombreCompleto;
            empleadoDTO.Sueldo = empleadoDB.Sueldo;
            empleadoDTO.IdPerfil = empleadoDB.IdPerfil;
            empleadoDTO.NombrePerfil = empleadoDB.PerfilReferencia.Nombre;
            return empleadoDTO;
        }


       
        public async Task<EmpleadoDTO> Guardar(EmpleadoDTO empleadoDTO)
        {
            var perfilExiste = await _context.Perfiles
             .AnyAsync(perfil => perfil.IdPerfil == empleadoDTO.IdPerfil);

            if (!perfilExiste)
            {
                return null;
            }

            var empleadoDB = new Empleado()
            {
                NombreCompleto = empleadoDTO.NombreCompleto,
                Sueldo = empleadoDTO.Sueldo,
                IdPerfil = empleadoDTO.IdPerfil
            };

            await _context.Empleados.AddAsync(empleadoDB);
            await _context.SaveChangesAsync();
            empleadoDTO.IdEmpleado = empleadoDB.IdEmpleado;
            return empleadoDTO;

        }

        public async Task<EmpleadoDTO> Editar(EmpleadoDTO empleadoDTO)
        {
            var empleadoDB = await _context.Empleados.Include(p => p.PerfilReferencia)
                .Where(e => e.IdEmpleado == empleadoDTO.IdEmpleado).FirstOrDefaultAsync();

            if (empleadoDB == null)
            {
                return null;
            }

            empleadoDB.NombreCompleto = empleadoDTO.NombreCompleto;
            empleadoDB.Sueldo = empleadoDTO.Sueldo;
            empleadoDB.IdPerfil = empleadoDTO.IdPerfil;

            _context.Empleados.Update(empleadoDB);
            await _context.SaveChangesAsync();

            return empleadoDTO;
        }

       
        public async Task<string> Eliminar(int id)
        {
            var empleadoDB = await _context.Empleados.FindAsync(id);
            if (empleadoDB == null)
            {
                return null;
            }
            _context.Empleados.Remove(empleadoDB);
            await _context.SaveChangesAsync();
            return "Empleado eliminado.";
        }

    }
}
