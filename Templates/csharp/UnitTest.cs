namespace Solution;

public class UnitTest
{
    private readonly Solution _solution = new Solution();
    public UnitTest(){}

    [Fact]
    public void Test()
    {
        var result = _solution.Run();
        // Add assertions here
    }
}
