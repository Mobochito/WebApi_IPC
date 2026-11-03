
using CrudApi.Context;
using CrudApi.DTOs;
using CrudApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudApi.Service
{
    public class PerfilService
    {
        //private readonly AppDbMysqlContext _context;
        private readonly IApplicationDbContext _context;

        public PerfilService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PerfilDTO>> Lista()
        {
            List<PerfilDTO> listaDTO = new List<PerfilDTO>();
            foreach (var item in await _context.Perfiles.ToListAsync())
            {
                listaDTO.Add(new PerfilDTO()
                {
                    IdPerfil = item.IdPerfil,
                    Nombre = item.Nombre
                });
            }
            return listaDTO;
        }

        public async Task<PerfilDTO> Buscar(int id)
        {
            var perfilDB = await _context.Perfiles.Where(p => p.IdPerfil == id).FirstOrDefaultAsync();
            if (perfilDB == null)
            {
                return null;
            }
            var perfilDTO = new PerfilDTO()
            {
                IdPerfil = perfilDB.IdPerfil,
                Nombre = perfilDB.Nombre
            };
            return perfilDTO;
        }
        public async Task<PerfilDTO> Guardar(PerfilDTO perfilDTO)
        {
            var perfilDB = new Perfil()
            {
                Nombre = perfilDTO.Nombre
            };
            await _context.Perfiles.AddAsync(perfilDB);
            await _context.SaveChangesAsync();
            perfilDTO.IdPerfil = perfilDB.IdPerfil;
            return perfilDTO;
        }

        public async Task<PerfilDTO> Editar(PerfilDTO perfilDTO)
        {
            var perfilDB = await _context.Perfiles.Where(p => p.IdPerfil == perfilDTO.IdPerfil).FirstOrDefaultAsync();
            if (perfilDB == null)
            {
                return null;
            }
            perfilDB.Nombre = perfilDTO.Nombre;
            _context.Perfiles.Update(perfilDB);
            await _context.SaveChangesAsync();
            return perfilDTO;
        }

        public async Task<string> Eliminar(int id)
        {
            var perfilDB = await _context.Perfiles.Where(p => p.IdPerfil == id).FirstOrDefaultAsync();
            if (perfilDB == null)
            {
                return null;
            }
            _context.Perfiles.Remove(perfilDB);
            await _context.SaveChangesAsync();
            return "Perfil eliminado.";
        }
    }
}
