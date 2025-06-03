using DesafioNubank.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioNubank.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Contato> Contatos { get; set; } // Adicionado DbSet para Contato

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração da Entidade Cliente
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Nome)
                  .HasMaxLength(150)
                  .IsRequired();
            
            entity.Property(c => c.Email)
                  .HasMaxLength(150)
                  .IsRequired();
            
            entity.Property(c => c.Password)
                  .HasMaxLength(50)
                  .IsRequired();
            
            entity.Property(c => c.DataCadastro)
                  .IsRequired();
            
            entity.Property(c => c.DataUltimaAlteracao)
                  .IsRequired(false);

            // Configuração do relacionamento One-to-Many (Cliente tem muitos Contatos)
            // Esta é a forma correta de configurar do lado do Cliente,
            // especificando a propriedade de navegação em Contato ('Cliente')
            // e a chave estrangeira em Contato ('IdCliente').
            entity.HasMany(c => c.Contatos)          // Cliente tem muitos Contatos
                  .WithOne(co => co.Cliente)         // Cada Contato está associado a um Cliente (usando a propriedade de navegação Contato.Cliente)
                  .HasForeignKey(co => co.IdCliente) // A chave estrangeira em Contato é IdCliente
                  .IsRequired();                     // Um contato sempre deve ter um cliente
                  // .OnDelete(DeleteBehavior.Cascade); // Opcional: Define o comportamento ao deletar um Cliente.
                                                     // Cascade deletaria os contatos associados.
                                                     // Restrict (padrão para FKs não nulas) impediria.
        });

        // Configuração da Entidade Contato
        modelBuilder.Entity<Contato>(entity =>
        {
            entity.HasKey(co => co.Id);

            entity.Property(co => co.Nome)
                  .HasMaxLength(100)
                  .IsRequired();

            entity.Property(co => co.Email)
                  .HasMaxLength(150)
                  .IsRequired();

            entity.HasIndex(co => co.Email).IsUnique();
            // Considere adicionar: .HasIndex(co => co.Email).IsUnique(); se o email deve ser único

            entity.Property(co => co.Ddd)
                  .IsRequired();

            entity.Property(co => co.Telefone)
                  .HasMaxLength(9) // Se o telefone puder ter mais ou menos, ajuste
                  .IsRequired();

            // A configuração do relacionamento já foi feita do lado do Cliente.
            // Se você quisesse configurar a partir daqui (Contato tem um Cliente), seria:
            /*
            entity.HasOne(co => co.Cliente)             // Contato tem um Cliente
                  .WithMany(c => c.Contatos)          // Cliente tem muitos Contatos
                  .HasForeignKey(co => co.IdCliente)  // A chave estrangeira em Contato é IdCliente
                  .IsRequired();
            */
            // Não é necessário configurar dos dois lados se a configuração já estiver completa e correta em um deles.
            // A configuração feita em Cliente.HasMany já estabelece tudo.
        });

        // Configurar nomes de tabelas (opcional se os nomes DbSet já são os desejados)
        modelBuilder.Entity<Cliente>().ToTable("Clientes");
        modelBuilder.Entity<Contato>().ToTable("Contatos");
    }
}