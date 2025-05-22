using Animation2Tilemap.Common;

namespace Animation2Tilemap.Test.Common;

public class NaturalStringComparerTests
{
    private readonly NaturalStringComparer _comparer = new();

    [Theory]
    [InlineData("", "", 0)]
    [InlineData("abc", "abc", 0)]
    [InlineData("abc", "def", -1)]
    [InlineData("def", "abc", 1)]
    public void Compare_ShouldReturnExpectedResult_WhenComparingSimpleStrings(string x, string y, int expected)
    {
        // Act
        var result = _comparer.Compare(x, y);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("abc1", "abc2", -1)]
    [InlineData("abc2", "abc1", 1)]
    [InlineData("abc10", "abc2", 1)]
    [InlineData("abc2", "abc10", -1)]
    [InlineData("abc10def", "abc2def", 1)]
    [InlineData("abc2def", "abc10def", -1)]
    [InlineData("abc123def", "abc22def", 1)]
    [InlineData("abc22def", "abc123def", -1)]
    public void Compare_ShouldReturnExpectedResult_WhenComparingStringsContainingNumbers(string x, string y,
        int expected)
    {
        // Act
        var result = _comparer.Compare(x, y);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("١٢٣٤٥", "12345", 0)] // Arabic-Indic digits
    [InlineData("１２３４５", "12345", 0)] // Fullwidth digits
    [InlineData("๒๓๔", "234", 0)] // Thai digits
    [InlineData("६७८", "678", 0)] // Devanagari digits
    [InlineData("৫৬৭", "567", 0)] // Bengali digits
    [InlineData("୫୬୭", "567", 0)] // Odia digits
    [InlineData("౫౬౭", "567", 0)] // Telugu digits
    [InlineData("೫೬೭", "567", 0)] // Kannada digits
    [InlineData("൫൬൭", "567", 0)] // Malayalam digits
    [InlineData("๔๕๖", "456", 0)] // Thai digits
    [InlineData("၄၅၆", "456", 0)] // Myanmar digits
    public void Compare_ShouldHaveEqualValues_WhenComparingNonASCIIDigits(string x, string y, int expected)
    {
        // Act
        var result = _comparer.Compare(x, y);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Compare_ShouldReturnNaturalOrderedList()
    {
        // Arrange
        var expected = new List<string>
        {
            "frame_1.png",
            "frame_2.png",
            "frame_3.png",
            "frame_4.png",
            "frame_5.png",
            "frame_6.png",
            "frame_7.png",
            "frame_8.png",
            "frame_9.png",
            "frame_10.png",
            "frame_11.png",
            "frame_12.png",
            "frame_13.png",
            "frame_14.png",
            "frame_15.png",
            "frame_16.png",
            "frame_17.png",
            "frame_18.png",
            "frame_19.png",
            "frame_20.png",
            "frame_21.png",
            "frame_22.png",
            "frame_23.png",
            "frame_24.png",
            "frame_25.png",
            "frame_95.png",
            "frame_96.png",
            "frame_97.png",
            "frame_98.png",
            "frame_99.png",
            "frame_100.png",
            "frame_102.png",
            "frame_103.png",
            "frame_104.png",
            "frame_105.png"
        };

        var input = new List<string>
        {
            "frame_16.png",
            "frame_104.png",
            "frame_105.png",
            "frame_2.png",
            "frame_3.png",
            "frame_23.png",
            "frame_24.png",
            "frame_25.png",
            "frame_95.png",
            "frame_8.png",
            "frame_9.png",
            "frame_10.png",
            "frame_17.png",
            "frame_18.png",
            "frame_19.png",
            "frame_100.png",
            "frame_11.png",
            "frame_20.png",
            "frame_21.png",
            "frame_22.png",
            "frame_96.png",
            "frame_4.png",
            "frame_5.png",
            "frame_6.png",
            "frame_102.png",
            "frame_103.png",
            "frame_7.png",
            "frame_12.png",
            "frame_13.png",
            "frame_14.png",
            "frame_15.png",
            "frame_1.png",
            "frame_97.png",
            "frame_98.png",
            "frame_99.png"
        };

        // Act
        var actual = input.Order(_comparer).ToList();

        // Assert
        Assert.Equal(expected, actual);
    }
}