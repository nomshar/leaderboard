namespace tests;

public static class Mockdata
{
    public static MatchMock[] InitialData = new[] {
       new MatchMock("Mexico", "Canada", 0, 5),
       new MatchMock("Spain", "Brazil", 10, 2),
       new MatchMock("Germany", "France", 2, 2),
       new MatchMock("Uruguay", "Italy", 6, 6),
       new MatchMock("Argentina", "Australia", 3, 1) 
    };

    public static MatchMock[] ExpectedOrder = new[] {
       new MatchMock("Uruguay", "Italy", 6, 6),
       new MatchMock("Spain", "Brazil", 10, 2),    
       new MatchMock("Mexico", "Canada", 0, 5),
       new MatchMock("Argentina", "Australia", 3, 1),
       new MatchMock("Germany", "France", 2, 2)        
    };
}
