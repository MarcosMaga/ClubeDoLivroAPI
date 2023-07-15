using ClubeDoLivroAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubeDoLivroAPI.Data.Map
{
    public class EmprestimoMap : IEntityTypeConfiguration<EmprestimoModel>
    {
        public void Configure(EntityTypeBuilder<EmprestimoModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.LivroId).IsRequired();
            builder.Property(x => x.UsuarioId).IsRequired();
            builder.Property(x => x.DataEmprestimo).IsRequired();
            builder.Property(x => x.DataEstimada).IsRequired();
            builder.Property(x => x.DataDevolucao);
        }
    }
}
