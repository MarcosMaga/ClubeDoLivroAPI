using ClubeDoLivroAPI.Data.Map;
using ClubeDoLivroAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ClubeDoLivroAPI.Data
{
    public class ClubeDoLivroDBContext : DbContext
    {
        public ClubeDoLivroDBContext(DbContextOptions<ClubeDoLivroDBContext> options) : base(options)
        {

        }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<LivroModel> Livros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new LivroMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
