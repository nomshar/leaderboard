using contracts.Avl;

namespace contracts;

/// <summary>
/// Interface to define Leaderboard behaviour
/// T type identifies a node value
/// </summary>
public interface ILeaderboard<T>: IDisposable where T:class
{
    /// <summary>
    /// Starts a new match
    /// Scores of both teams are set to zero
    /// </summary>
    /// <param name="home">Home team name</param>
    /// <param name="away">Away team name</param>
    void StartMatch(string home, string away);

    /// <summary>
    /// Adds score for both teams
    /// New score for each team is sum of existing score and a new score
    /// </summary>
    /// <param name="home">Home team name</param>
    /// <param name="away">Away team name</param>
    /// <param name="homeScore">Home team score</param>
    /// <param name="awayScore">Away team score</param>
    void AddScore(string home, string away, uint homeScore, uint awayScore);

    /// <summary>
    /// Removes a match from the board
    /// </summary>
    /// <param name="home">Home team name</param>
    /// <param name="away">Away team name</param>
    void FinishMatch(string home, string away);

    /// <summary>
    /// Returns a list of matches from the board
    /// The list ordered by a match's total score.
    /// If two matches have equals score, they are ordered by the most recently started match
    /// </summary>
    /// <param name="k">Number of records to return</param>
    /// <returns></returns>
    IEnumerable<T> TopMatches(int k);

    /// <summary>
    /// Cleans up the board
    /// </summary>
    void Reset();
}
