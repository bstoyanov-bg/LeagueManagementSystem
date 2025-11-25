namespace LeagueApi.Scoring
{
    public class StandardScoringStrategy : IScoringStrategy
    {
        public (int homePoints, int awayPoints) GetPoints(int homeScore, int awayScore)
        {
            if (homeScore > awayScore) return (3, 0);
            if (homeScore < awayScore) return (0, 3);
            return (1, 1);
        }
    }
}
