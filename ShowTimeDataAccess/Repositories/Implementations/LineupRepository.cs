using Microsoft.EntityFrameworkCore;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories.Abstractions;

namespace ShowTime.DataAccess.Repositories;

public class LineupRepository : ILineupRepository
{
    private readonly ShowtimeDbContext _context;
    private readonly DbSet<Lineup> _lineups;
    
    public LineupRepository(ShowtimeDbContext context)
    {
        _context = context;
        _lineups = context.Set<Lineup>();
    }

    public async Task<Lineup?> GetAsync(int festivalId, int artistId)
    {
        return await _lineups.SingleOrDefaultAsync(l => l.FestivalId == festivalId && l.ArtistId == artistId);   
    }
    
    public async Task<IEnumerable<Lineup>> GetByFestivalIdAsync(int festivalId)
    {
        return await _lineups
            .Include(l => l.Artist)
            .Where(l => l.FestivalId == festivalId)
            .ToListAsync();   
    }
    
    public async Task<IEnumerable<Lineup>> GetByArtistIdAsync(int artistId)
    {
        return await _lineups
            .Include(l => l.Festival)
            .Where(l => l.ArtistId == artistId)
            .ToListAsync();   
    }

    public async Task AddAsync(Lineup lineup)
    {
        try
        {
            await _lineups.AddAsync(lineup);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Unable to add lineup: {ex.Message}");       
        }
    }

    public async Task UpdateAsync(Lineup lineup)
    {
        try
        {
            var existingEntity = await _lineups.FindAsync(lineup.FestivalId, lineup.ArtistId);
            if (existingEntity == null)
            {
                throw new Exception($"Lineup with festivalId {lineup.FestivalId} and artistId {lineup.ArtistId} not found.");
            }
        
            existingEntity.Stage = lineup.Stage;
            existingEntity.StartTime = lineup.StartTime;
        
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Unable to update lineup: {ex.Message}");
        }
    }
    
    public async Task DeleteAsync(int festivalId, int artistId)
    {
        try
        {
            var entity = await _lineups.FindAsync(festivalId, artistId);
            if (entity == null)
            {
                throw new Exception($"Lineup with festivalId {festivalId} and artistId {artistId} not found.");
            }
            _lineups.Remove(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Unable to delete lineup with festivalId {festivalId} and artistId {artistId}: {ex.Message}");      
        }
    }
}