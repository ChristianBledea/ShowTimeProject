using ShowTime.BusinessLogic.Dtos;

namespace ShowTime.BusinessLogic.Abstractions;

public interface ILineupService
{
    Task AddToLineupAsync(LineupCreateDto lineupCreateDto);
    Task UpdateLineupAsync(LineupCreateDto lineupCreateDto);
    Task<IEnumerable<LineupGetDto>> GetLineupAsync(int festivalId);
    Task RemoveFromLineupAsync(int festivalId, int artistId);
}