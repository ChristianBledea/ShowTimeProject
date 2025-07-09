namespace ShowTime.BusinessLogic.Dtos;

public class LineupGetDto
{
    public int FestivalId { get; set; }
    public string ArtistName { get; set; } = String.Empty;
    public string Stage { get; set; } = String.Empty;
    public DateTime StartTime { get; set; }
}