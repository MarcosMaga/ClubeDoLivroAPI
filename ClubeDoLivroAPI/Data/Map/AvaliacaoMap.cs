using ClubeDoLivroAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubeDoLivroAPI.Data.Map
{
    public class AvaliacaoMap : IEntityTypeConfiguration<AvaliacaoModel>
    {
        public void Configure(EntityTypeBuilder<AvaliacaoModel> builder) {
            builder.HasKey(x => new {x.UsuarioId, x.LivroId});
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.LivroId).IsRequired();
            builder.Property(x => x.UsuarioId).IsRequired();
            builder.Property(x => x.Opiniao).HasMaxLength(500);
            builder.Property(x => x.Nota).IsRequired();
            builder.HasOne(x => x.Usuario);
            builder.HasOne(x => x.Livro);
        }
    }
}
