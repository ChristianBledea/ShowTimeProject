using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowTime.Businesslogic.Dtos;

namespace ShowTime.Businesslogic.Abstractions
{
    public interface IFestivalService
    {
        Task<FestivalGetDto?> GetFestivalAsync(int id);
        Task<IList<FestivalGetDto>> GetAllFestivalsAsync();

        Task AddFestivalAsync(FestivalCreateDto festival);
        Task UpdateFestivalAsync(int id, UpdateFestivalDto festival);
        Task DeleteFestivalAsync(int id);
    }
}
