using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Searcher.DAL.Entities;

namespace Searcher.DAL.EntityConfigurations
{
    public class SearchResultConfiguration : IEntityTypeConfiguration<SearchResult>
    {
        public void Configure(EntityTypeBuilder<SearchResult> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
