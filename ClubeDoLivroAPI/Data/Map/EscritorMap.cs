using ClubeDoLivroAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubeDoLivroAPI.Data.Map
{
    public class EscritorMap : IEntityTypeConfiguration<EscritorModel>
    {
        public void Configure(EntityTypeBuilder<EscritorModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Ano);
        }
    }
}
