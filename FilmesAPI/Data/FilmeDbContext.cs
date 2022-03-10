using FilmesApi.Models;
using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data
{
    public class FilmeDbContext : DbContext
    {
        public FilmeDbContext(DbContextOptions<FilmeDbContext> opt) : base(opt)
        {

        }

        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Gerente> Gerentes { get; set; }
        public DbSet<Sessao> Sessoes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Endereco>()
                .HasOne(endereco => endereco.Cinema)
                .WithOne(cinema => cinema.Endereco)
                .HasForeignKey<Cinema>(cinema => cinema.EnderecoId);

            builder.Entity<Gerente>()
                .HasMany(gerente => gerente.Cinemas)
                .WithOne(cinema => cinema.Gerente)
                .HasForeignKey(cinema => cinema.GerenteId).IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Sessao>()
                .HasOne(sessao => sessao.Filme)
                .WithMany(filme => filme.Sessões)
                .HasForeignKey(sessao => sessao.FilmeId);

            builder.Entity<Sessao>()
                .HasOne(sessao => sessao.Cinema)
                .WithMany(cinema => cinema.Sessões)
                .HasForeignKey(sessao => sessao.CinemaId);
        }
    }
}
