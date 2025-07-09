using ShowTime.DataAccess.Models;

namespace ShowTime.DataAccess.Repositories.Abstractions;

public interface IArtistRepository : IRepository<Artist>
{
    public Task<Artist?> GetByName(string name);
    public Task<IEnumerable<Artist>> FilterByGenre(string genre);
    public Task<IEnumerable<Artist>> SearchByName(string name);
}