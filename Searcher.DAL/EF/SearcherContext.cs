using Microsoft.EntityFrameworkCore;
using Searcher.DAL.Entities;
using Searcher.DAL.EntityConfigurations;

namespace Searcher.DAL.EF
{
    public class SearcherContext : DbContext
    {
        public SearcherContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new SearchResultConfiguration());
        }

        public DbSet<SearchResult> SearchResults { get; set; }
    }
}
