using DesafioNubank.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioNubank.Api.Infra;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options){
    
    public DbSet<Cliente> Clientes { get; set; }

    // Opcional: Configurações adicionais do modelo (fluent API)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    
        // Exemplo de configuração usando Fluent API (se necessário)
        modelBuilder.Entity<Cliente>()
                .HasKey(c => c.Id); // Definindo a chave primária
        
        modelBuilder.Entity<Cliente>()
            .Property(c => c.Email)
            .HasMaxLength(100)
            .IsRequired();
        
         modelBuilder.Entity<Cliente>()
             .Property(c => c.Nome)
             .HasMaxLength(150)
             .IsRequired();
         
        // Configurar nomes de tabelas e esquemas se você não quiser usar as convenções
         modelBuilder.Entity<Cliente>().ToTable("Clientes");
    }
}