using ShowTime.DataAccess.Models;

namespace ShowTime.DataAccess.Repositories.Abstractions;

public interface ILineupRepository
{
    Task AddAsync(Lineup lineup);
    Task UpdateAsync(Lineup lineup);
    Task DeleteAsync(int festivalId, int artistId);
    Task<Lineup?> GetAsync(int festivalId, int artistId);
    Task<IEnumerable<Lineup>> GetByFestivalIdAsync(int festivalId);
    Task<IEnumerable<Lineup>> GetByArtistIdAsync(int artistId);
}