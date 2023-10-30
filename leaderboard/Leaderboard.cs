namespace leaderboard;

using System.Collections.Generic;
using contracts;
using contracts.Avl;
using leaderboard.Avl;

public class Leaderboard : ILeaderboard<IMatch>
{
    private ITree<IMatch> tree;

    private IDictionary<string, IMatch> map;

    public Leaderboard()
    {
        tree = new AvlTree();
        map = new Dictionary<string, IMatch>();
    }
    
    public void AddScore(string home, string away, uint homeScore, uint awayScore)
    {
        var match = new MatchRecord() {
            HomeTeam = home,
            AwayTeam = away,
            HomeTeamScore = homeScore,
            AwayTeamScore = awayScore
        };
        
        if (map.TryGetValue(match.MatchId, out var oldRecord))
        {
            match.HomeTeamScore += oldRecord.HomeTeamScore;
            match.AwayTeamScore += oldRecord.AwayTeamScore;
            if (oldRecord.Score != match.Score)
            {
                tree.RemoveNodesFromScore(oldRecord.Score, match);
                tree.Add(match.Score, match);
            }
            map[match.MatchId] = match;
        }
        else
        {
            tree.Add(match.Score, match);
            map[match.MatchId] = match;
        }
    }

    public void FinishMatch(string home, string away)
    {
        var matchId = MatchRecord.GenerateMatchId(home, away);
        
        if (map.TryGetValue(matchId, out IMatch oldRecord))
        {
            tree.RemoveNodesFromScore(oldRecord.Score, oldRecord);
        }
        map.Remove(matchId);
    }

    public void StartMatch(string home, string away)
    {
        AddScore(home, away, 0, 0);
    }

    public IEnumerable<IMatch> TopMatches(int k)
    {
        return tree.Top(k);
    }

    public void Reset() {
        tree.Clean();
        map.Clear();
    }

    public void Dispose()
    {
        tree.Dispose();
        map.Clear();
    }    
}
