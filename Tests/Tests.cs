using System.Runtime.CompilerServices;
using FluentAssertions;
using MILLEC;

namespace Tests;

public class Tests
{
    [Test]
    public void ItemCountShouldBeValidAfterAdds()
    {
        var millec = new MILLEC<int>();

        millec.ItemsCount.Should().Be(0);

        const int ADD_COUNT = 2;

        for (int i = 0; i < ADD_COUNT; i++)
        {
            millec.Add(i);
        }
        
        millec.ItemsCount.Should().Be(ADD_COUNT);
    }
    
    [Test]
    public void ItemCountShouldBeValidAfterRemoves()
    {
        var millec = new MILLEC<int>();

        millec.ItemsCount.Should().Be(0);

        const int ADD_COUNT = 5, REMOVE_COUNT = 4;

        for (int i = 0; i < ADD_COUNT; i++)
        {
            millec.Add(i);
        }
        
        millec.ItemsCount.Should().Be(ADD_COUNT);
        
        for (int i = 0; i < REMOVE_COUNT; i++)
        {
            millec.RemoveAt(i);
        }
        
        millec.ItemsCount.Should().Be(ADD_COUNT - REMOVE_COUNT);
    }
    
    [MethodImpl(MethodImplOptions.NoInlining)]
    private void DoSomething(int item)
    {
            
    }
    
    [Test]
    public void DeletedSlotsShouldNotBeAccessible()
    {
        var millec = new MILLEC<int>();

        millec.ItemsCount.Should().Be(0);

        const int ADD_COUNT = 5;

        for (int i = 0; i < ADD_COUNT; i++)
        {
            millec.Add(i);
        }
        
        // We are skipping first index
        for (int i = 1; i < ADD_COUNT; i++)
        {
            millec.RemoveAt(i);
        }
        
        Assert.DoesNotThrow(() =>
        {
            millec[0].Should().Be(0);
        });
        
        for (int i = 1; i < ADD_COUNT; i++)
        {
            Assert.Throws<Exception>(() =>
            {
                DoSomething(millec[i]);
            });
        }
    }
    
    [Test]
    public void FreeSlotsShouldBePopulated()
    {
        var millec = new MILLEC<int>();

        millec.ItemsCount.Should().Be(0);

        const int ADD_COUNT = 5;

        for (int i = 0; i < ADD_COUNT; i++)
        {
            millec.Add(i);
        }
        
        // Skip first one, as Count == 0 will reset HighestKnownIndex
        for (int i = 1; i < ADD_COUNT; i++)
        {
            millec.RemoveAt(i);
        }
        
        for (int i = 1; i < ADD_COUNT; i++)
        {
            millec.Add(i);
        }

        millec.GetHighestKnownIndex().Should().Be(ADD_COUNT - 1);
        
        for (int i = 0; i < ADD_COUNT; i++)
        {
            millec.RemoveAt(i);
        }
        
        millec.GetHighestKnownIndex().Should().Be(-1);
    }
}