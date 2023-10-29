namespace tests;

public class MatchMock
{
    public TeamMock Home { get; set; }

    public TeamMock Away { get; set; }

    public MatchMock(string home, string away, uint homeScore, uint awayScore)
    {
        Home = new TeamMock() {
            Team = home,
            Score = homeScore
        };
        Away = new TeamMock() {
            Team = away,
            Score = awayScore
        };
    }
}
