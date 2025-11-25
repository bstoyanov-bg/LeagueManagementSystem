namespace LeagueApi.Scoring
{
    public interface IScoringStrategy
    {
        /// <summary>
        /// Given home and away scores, return (homePoints, awayPoints).
        /// </summary>
        (int homePoints, int awayPoints) GetPoints(int homeScore, int awayScore);
    }
}
