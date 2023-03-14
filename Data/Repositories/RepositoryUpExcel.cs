using Data.Config;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class RepositoryUpExcel : RepositoryGenerics<UpExcel>, IUpExcel
    {
        public RepositoryUpExcel(DbContextOptions<ContextBase> optionsBuilder) : base(optionsBuilder)
        {

        }
    }
}
