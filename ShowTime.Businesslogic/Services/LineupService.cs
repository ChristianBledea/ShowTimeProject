using ShowTime.BusinessLogic.Abstractions;
using ShowTime.BusinessLogic.Dtos;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories.Abstractions;


namespace ShowTime.BusinessLogic.Services;

public class LineupService : ILineupService
{
    private readonly ILineupRepository _lineupRepository;
    
    public LineupService(ILineupRepository lineupRepository)
    {
        _lineupRepository = lineupRepository;
    }
    
    public async Task<IEnumerable<LineupGetDto>> GetLineupAsync(int festivalId)
    {
        try
        {
            var lineup = await _lineupRepository.GetByFestivalIdAsync(festivalId);
            return lineup.Select(l => new LineupGetDto
            {
                FestivalId = l.FestivalId,
                ArtistName = l.Artist.Name,
                Stage = l.Stage,
                StartTime = l.StartTime
            }).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception($"Unable to retrieve the lineup: {ex.Message}");     
        }
    }

    public async Task AddToLineupAsync(LineupCreateDto lineupCreateDto)
    {
        var entity = new Lineup
        {
            FestivalId = lineupCreateDto.FestivalId,
            ArtistId = lineupCreateDto.ArtistId,
            Stage = lineupCreateDto.Stage,
            StartTime = lineupCreateDto.StartTime
        };
        try
        {
            await _lineupRepository.AddAsync(entity);
        }
        catch (Exception ex)
        {
            throw new Exception($"Unable to add artist to lineup: {ex.Message}");
        }
    }
    
    public async Task UpdateLineupAsync(LineupCreateDto lineupCreateDto)
    {
        try
        {
            var entity = new Lineup
            {
                FestivalId = lineupCreateDto.FestivalId,
                ArtistId = lineupCreateDto.ArtistId,
                Stage = lineupCreateDto.Stage,
                StartTime = lineupCreateDto.StartTime
            };
        
            await _lineupRepository.UpdateAsync(entity);
        }
        catch (Exception ex)
        {
            throw new Exception($"Unable to update lineup: {ex.Message}");
        }
    }
    
    public async Task RemoveFromLineupAsync(int festivalId, int artistId)
    {
        try
        {
            await _lineupRepository.DeleteAsync(festivalId, artistId);
        }
        catch (Exception ex)
        {
            throw new Exception($"Unable to remove artist from lineup: {ex.Message}");
        }
    }
}