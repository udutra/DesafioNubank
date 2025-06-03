using DesafioNubank.Domain.Models;
using DesafioNubank.Domain.Repositories;
using DesafioNubank.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DesafioNubank.Infrastructure.Repositories;

public class ClienteRepository(AppDbContext context) : IClienteRepository{
    
    private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    
    public async Task AddAsync(Cliente cliente){
        ArgumentNullException.ThrowIfNull(cliente);
        await _context.Clientes.AddAsync(cliente);
    }
    
    public async Task<Cliente?> GetByIdAsync(Guid id){
        // FindAsync é otimizado para buscar por chave primária e primeiro verifica o contexto.
        return await _context.Clientes.FindAsync(id);
    }

    public async Task<Cliente?> GetByIdWithContatosAsync(Guid id)
    {
        return await _context.Clientes
            .Include(c => c.Contatos) // Eager loading dos Contatos
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Cliente>> GetAllWithContatosAsync()
    {
        return await _context.Clientes
            .Include(c => c.Contatos) // Eager loading dos Contatos
            .ToListAsync();
    }

    public void Update(Cliente cliente){
        ArgumentNullException.ThrowIfNull(cliente);
        // O EF Core rastreia as entidades. Se você buscou a entidade e a modificou,
        // o DbContext já sabe que ela foi alterada.
        // Chamar Update() explicitamente garante que a entidade seja marcada como Modified,
        // o que é útil se a entidade foi desanexada ou se você quer ser explícito.
        _context.Clientes.Update(cliente);
        // Alternativamente, se você tem certeza que a entidade está sendo rastreada e foi modificada:
        // _context.Entry(cliente).State = EntityState.Modified;
    }

    public void Delete(Cliente cliente)
    {
        ArgumentNullException.ThrowIfNull(cliente);
        _context.Clientes.Remove(cliente);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
