namespace ShowTime.BusinessLogic.Dtos;

public class LineupCreateDto
{
    public int FestivalId { get; set; }
    public int ArtistId { get; set; }
    public string Stage { get; set; } = String.Empty;
    public DateTime StartTime { get; set; }
}