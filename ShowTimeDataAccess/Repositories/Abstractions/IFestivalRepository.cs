using ShowTime.DataAccess.Models;

namespace ShowTime.DataAccess.Repositories.Abstractions;

public interface IFestivalRepository : IRepository<Festival>
{
    public Task<Festival?> GetByName(string name);
    public Task<IEnumerable<Festival>> FilterByDate(DateTime date);
    
    public Task<IEnumerable<Festival>> SearchByName(string name);   
}