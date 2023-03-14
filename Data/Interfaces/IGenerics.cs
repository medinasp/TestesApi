using Data.Config;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IGenerics<T> where T : class
    {
        Task Add(T Objeto);
        Task Update(T Objeto);
        Task Delete(T Objeto);
        Task<bool> DeleteRange(List<int> Ids);
        Task<T> GetEntityById(int Id);
        Task<List<T>> List();
        void SetContextOptions(DbContextOptions<ContextBase> options);
        void AddRange(IEnumerable<T> entities);
    }
}