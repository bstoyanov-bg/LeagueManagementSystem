using System;
using System.Collections.Generic;

namespace LeagueApi.Models
{
    public class Team
    {
        public Team()
        {
            HomeMatches = new HashSet<Match>();
            AwayMatches = new HashSet<Match>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Match> HomeMatches { get; set; }
        public virtual ICollection<Match> AwayMatches { get; set; }
    }
}
