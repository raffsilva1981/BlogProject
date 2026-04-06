using BlogProject.Domain.Entidades;
using BlogProject.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly BlogDbContext _context;

    public PostRepository(BlogDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Post>> ListarTodosAsync()
        => await _context.Posts
                         .Include(p => p.User)
                         .ToListAsync();

    public async Task<Post?> ObterPorIdAsync(int id)
        => await _context.Posts
                         .Include(p => p.User)
                         .FirstOrDefaultAsync(p => p.Id == id);

    public async Task AdicionarAsync(Post post)
    {
        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Post post)
    {
        _context.Posts.Update(post);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(Post post)
    {
        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
    }
}