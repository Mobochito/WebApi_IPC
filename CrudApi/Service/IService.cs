using CrudApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CrudApi.Service
{
    public interface IService<T>
    {
        public Task<List<T>> Lista();
        public Task<T> Buscar(int id);

        public Task<T> Guardar(T dTO);

        public Task<T> Editar(T dTO);

        public Task<string> Eliminar(int id);
    }
}
