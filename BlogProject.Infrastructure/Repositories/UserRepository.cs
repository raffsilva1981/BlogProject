using BlogProject.Domain.Entidades;
using BlogProject.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly BlogDbContext _context;

    public UserRepository(BlogDbContext context)
    {
        _context = context;
    }

    public async Task<User?> ObterPorEmailAsync(string email)
        => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<User?> ObterPorIdAsync(int id)
        => await _context.Users.FindAsync(id);

    public async Task AdicionarAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> EmailJaExisteAsync(string email)
        => await _context.Users.AnyAsync(u => u.Email == email);
}