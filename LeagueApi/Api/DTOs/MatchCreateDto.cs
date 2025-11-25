using System;

public class MatchCreateDto
{
    public int HomeTeamId { get; set; }
    public int AwayTeamId { get; set; }

    public int HomeTeamScore { get; set; }
    public int AwayTeamScore { get; set; }

    public DateTime PlayedAt { get; set; }
}