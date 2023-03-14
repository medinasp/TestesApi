using Data.Config;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class RepositoryProduto : RepositoryGenerics<Produto>, IProduto
    {
        public RepositoryProduto(DbContextOptions<ContextBase> optionsBuilder) : base(optionsBuilder)
        {
        }

        public async Task AddRangeAsync(IEnumerable<Produto> produtos)
        {
            await AddRangeAsync(produtos);
        }
    }
}
