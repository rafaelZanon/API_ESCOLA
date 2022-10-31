using Microsoft.EntityFrameworkCore;
using ProjetoLoja_API.Models;
using System.Diagnostics.CodeAnalysis;
namespace ProjetoLoja_API.Data
{
    public class LojaContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public LojaContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("StringConexaoSQLServer"));
        }
        public DbSet<Cliente>? Cliente { get; set; }
         public DbSet<Produto>? Produto { get; set; }
    }
}