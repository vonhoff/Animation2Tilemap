#pragma warning disable S4144

using TilemapGenerator.Common;

namespace TilemapGenerator.Test.Common;

public class NaturalStringComparerTests
{
    private readonly NaturalStringComparer _comparer = new();

    [Theory]
    [InlineData(null, null, 0)]
    [InlineData(null, "abc", -1)]
    [InlineData("abc", null, 1)]
    [InlineData("", "", 0)]
    [InlineData("abc", "abc", 0)]
    [InlineData("abc", "def", -1)]
    [InlineData("def", "abc", 1)]
    [InlineData("abc1", "abc2", -1)]
    [InlineData("abc2", "abc1", 1)]
    [InlineData("abc10", "abc2", 1)]
    [InlineData("abc2", "abc10", -1)]
    [InlineData("abc10def", "abc2def", 1)]
    [InlineData("abc2def", "abc10def", -1)]
    [InlineData("abc123def", "abc22def", 1)]
    [InlineData("abc22def", "abc123def", -1)]
    public void Compare_ShouldReturnExpectedResult(string x, string y, int expected)
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
    [InlineData("١٢٣٤٥", "٤٥٦٧٨", -1)] // Arabic-Indic digits
    public void Compare_ShouldReturnExpectedResult_WithNonASCIIDigits(string x, string y, int expected)
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
            "frame_105.png",
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
            "frame_99.png",
        };

        // Act
        var actual = input.Order(_comparer).ToList();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Compare_ShouldReturnNaturalOrderedList_WithMixedCharacters()
    {
        // Arrange
        var expected = new List<string> {
            "item1.png",
            "item2.png",
            "item3.png",
            "item4.png",
            "item5.png",
            "item6.png",
            "item7.png",
            "item8.png",
            "item9.png",
            "item10.png",
            "item11.png",
            "item12.png",
            "item13.png",
            "item14.png",
            "item15.png",
            "item16.png",
            "item17.png",
            "item18.png",
            "item19.png",
            "item20.png",
            "картинка1.png",
            "картинка2.png",
            "картинка3.png",
            "картинка4.png",
            "картинка5.png",
            "картинка6.png",
            "картинка7.png",
            "картинка8.png",
            "картинка9.png",
            "картинка10.png",
            "картинка11.png",
            "картинка12.png",
            "картинка13.png",
            "картинка14.png",
            "картинка15.png",
            "картинка16.png",
            "картинка17.png",
            "картинка18.png",
            "картинка19.png",
            "картинка20.png",
        };

        var input = new List<string> {
            "item10.png",
            "картинка10.png",
            "item5.png",
            "картинка5.png",
            "item20.png",
            "картинка20.png",
            "item3.png",
            "картинка16.png",
            "картинка15.png",
            "item7.png",
            "картинка7.png",
            "item16.png",
            "картинка14.png",
            "item13.png",
            "картинка13.png",
            "item9.png",
            "item14.png",
            "картинка3.png",
            "item1.png",
            "картинка1.png",
            "item15.png",
            "картинка12.png",
            "item17.png",
            "картинка17.png",
            "item6.png",
            "картинка6.png",
            "item8.png",
            "картинка8.png",
            "картинка9.png",
            "item12.png",
            "item2.png",
            "картинка2.png",
            "item19.png",
            "картинка19.png",
            "item4.png",
            "картинка4.png",
            "item18.png",
            "картинка18.png",
            "item11.png",
            "картинка11.png",
        };

        // Act
        var actual = input.Order(_comparer).ToList();

        // Assert
        Assert.Equal(expected, actual);
    }
}