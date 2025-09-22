using Xunit;

namespace CodingKataArgs.Tests;

public class ArgsParserTests
{
    [Fact]
    public void ShouldParse_BooleanFlag()
    {
        var parser = new ArgsParser("l:bool", new[] { "-l" });
        
        Assert.True(parser.GetBool('l'));
    }

    [Fact]
    public void ShouldReturnFalse_ForMissingBooleanFlag()
    {
        var parser = new ArgsParser("l:bool", new string[] { });
        
        Assert.False(parser.GetBool('l'));
    }

    [Fact]
    public void ShouldParse_IntegerFlag()
    {
        var parser = new ArgsParser("p:int", new[] { "-p", "8080" });
        
        Assert.Equal(8080, parser.GetInt('p'));
    }

    [Fact]
    public void ShouldParse_NegativeInteger()
    {
        var parser = new ArgsParser("p:int", new[] { "-p", "-1234" });
        
        Assert.Equal(-1234, parser.GetInt('p'));
    }

    [Fact]
    public void ShouldParse_StringFlag()
    {
        var parser = new ArgsParser("d:string", new[] { "-d", "/usr/logs" });
        
        Assert.Equal("/usr/logs", parser.GetString('d'));
    }

    [Fact]
    public void ShouldParse_StringList()
    {
        var parser = new ArgsParser("g:stringlist", new[] { "-g", "this,is,a,list" });
        var expected = new List<string> { "this", "is", "a", "list" };
        
        Assert.Equal(expected, parser.GetStringList('g'));
    }

    [Fact]
    public void ShouldParse_IntList()
    {
        var parser = new ArgsParser("n:intlist", new[] { "-n", "1,2,-3,5" });
        var expected = new List<int> { 1, 2, -3, 5 };
        
        Assert.Equal(expected, parser.GetIntList('n'));
    }

    [Fact]
    public void ShouldThrowException_ForUnknownFlag()
    {
        var ex = Assert.Throws<ArgsException>(() => 
            new ArgsParser("l:bool", new[] { "-x" }));
        
        Assert.Contains("Unknown flag: '-x'", ex.Message);
    }

    [Fact]
    public void ShouldThrowException_ForMissingValue()
    {
        var ex = Assert.Throws<ArgsException>(() => 
            new ArgsParser("p:int", new[] { "-p" }));
        
        Assert.Contains("expects a value but none was provided", ex.Message);
    }
}
