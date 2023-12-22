using FluentAssertions;
using MILLEC;

namespace Tests;

public class Tests
{
    [Test]
    public void ItemCountShouldBeValid()
    {
        var millec = new MILLEC<int>();

        millec.ItemsCount.Should().Be(0);

        const int ADD_COUNT = 2;

        for (int i = 1; i <= ADD_COUNT; i++)
        {
            millec.Add(i);
        }
        
        millec.ItemsCount.Should().Be(ADD_COUNT);
        
        millec.RemoveAt(0);
        
        millec.ItemsCount.Should().Be(ADD_COUNT - 1);
    }
}