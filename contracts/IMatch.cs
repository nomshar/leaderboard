namespace contracts;

/// <summary>
/// Interface to store match info: team names, match id and score
/// </summary>
public interface IMatch
{   
    /// <summary>
    /// Home team name
    /// </summary>
    public string HomeTeam { get; set; }

    /// <summary>
    /// Away team name
    /// </summary>
    public string AwayTeam { get; set; }

    /// <summary>
    /// Match Id, readonly property generated from Home and Away team names 
    /// </summary>
    public string MatchId { get; }

    /// <summary>
    /// Home team score
    /// </summary>
    public uint HomeTeamScore { get; set; }

    /// <summary>
    /// Away team score
    /// </summary>
    public uint AwayTeamScore { get; set; }

    /// <summary>
    /// Total score of the match, readonly property, sum of Home and Away team scores
    /// </summary>
    public uint Score { get; }
}
