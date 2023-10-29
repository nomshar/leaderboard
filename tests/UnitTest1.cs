using leaderboard;

namespace tests;

[TestFixture]
public class Tests
{
    private Leaderboard board;
    
    [SetUp]
    public void Setup()
    {
        board = new Leaderboard();
    }

    [Test]
    public void TestMatchIsStartedAndScoreIsZero()
    {
        var mock = Mockdata.InitialData[0];
        
        board.StartMatch(mock.Home.Team, mock.Away.Team);
        var result = board.TopMatches(1).ToArray();

        Assert.That(result.Length == 1, "Expected result count is 1");
        Assert.That(result[0].Score == 0, "Expected Score is 0");

        board.Reset();        
    }

    [Test]
    public void TestScoreIsUpdated()
    {
        var mock = Mockdata.InitialData[0];
        
        board.StartMatch(mock.Home.Team, mock.Away.Team);
        board.AddScore(mock.Home.Team, mock.Away.Team, mock.Home.Score, mock.Away.Score);

        var result = board.TopMatches(1).ToArray();

        Assert.That(result.Length == 1, "Expected result count is 1");
        Assert.That(result[0].Score == mock.Home.Score + mock.Away.Score, "Updated score value is not equal to expected one");

        board.Reset();
    }

    [Test]
    public void TestMatchIsFinishedAndRemovedFromScoreboard() 
    {
        var matchA = Mockdata.InitialData[0];
        var matchB = Mockdata.InitialData[1];

        board.StartMatch(matchA.Home.Team, matchA.Away.Team);
        board.StartMatch(matchB.Home.Team, matchB.Away.Team);

        board.FinishMatch(matchA.Home.Team, matchA.Away.Team);

        var result = board.TopMatches(2).ToArray();
        var expectedValue = $"{matchB.Home.Team}-{matchB.Away.Team}";
        
        Assert.That(result.Length == 1, "Expected result count is 1");
        Assert.That(result[0].MatchId.Equals(expectedValue), $"Result contains incorrect record.");
    
        board.Reset();
    }

    [Test]
    public void TestSummaryOfMatchesShouldFollowTheOrder() 
    {
        foreach(var match in Mockdata.InitialData) {
            board.StartMatch(match.Home.Team, match.Away.Team);
            board.AddScore(match.Home.Team, match.Away.Team, match.Home.Score, match.Away.Score);
        }

        var result = board.TopMatches(Mockdata.InitialData.Length);

        var expectedResult = Mockdata.ExpectedOrder
            .Select(x => $"{x.Home.Team} {x.Home.Score} - {x.Away.Team} {x.Away.Score}")
            .ToArray();

        var actualResult = result
            .Select(x => $"{x.HomeTeam} {x.HomeTeamScore} - {x.AwayTeam} {x.AwayTeamScore}")
            .ToArray();

        Assert.That(actualResult, Is.EqualTo(expectedResult), "Actual result is not equal to the expected");

        board.Reset();
    }

    [TearDown]
    public void Cleanup() {
        board.Dispose();
    }
}