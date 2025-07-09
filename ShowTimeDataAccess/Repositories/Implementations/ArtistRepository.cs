using Microsoft.EntityFrameworkCore;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories.Abstractions;
using ShowTime.DataAccess.Repositories.Implementations;


namespace ShowTime.DataAccess.Repositories;

public class ArtistRepository : GenericRepository<Artist>, IArtistRepository
{
    private readonly DbSet<Artist> _artists;
    
    public ArtistRepository(ShowtimeDbContext context) : base(context)
    {
        _artists = context.Set<Artist>();
    }
    
    public async Task<Artist?> GetByName(string name)
    {
        try
        {
            return await _artists.SingleOrDefaultAsync(a => a.Name == name);
        }
        catch (Exception ex)
        {
            throw new Exception($"Unable to retrieve artist with name {name}: {ex.Message}");
        }
    }

    public async Task<IEnumerable<Artist>> FilterByGenre(string genre)
    {
        return await _artists
            .Where(a => a.Genre.Contains(genre))
            .ToListAsync();       
    }

    public async Task<IEnumerable<Artist>> SearchByName(string name)
    {
        return await _artists
            .Where(a => a.Name.Contains(name))
            .ToListAsync();       
    }
}