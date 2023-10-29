using contracts;

namespace leaderboard;

public class MatchRecord : IMatch
{
    public string HomeTeam { get; set; }
    public string AwayTeam { get; set; }

    public string MatchId => GenerateMatchId(HomeTeam, AwayTeam);

    public uint HomeTeamScore { get; set; }
    public uint AwayTeamScore { get; set; }

    public uint Score => HomeTeamScore + AwayTeamScore;

    public static string GenerateMatchId(string left, string right) 
    {
        return $"{left}-{right}";
    }
}
