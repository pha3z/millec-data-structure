using System.Runtime.CompilerServices;
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

        for (int i = 0; i < ADD_COUNT; i++)
        {
            millec.Add(i);
        }
        
        millec.ItemsCount.Should().Be(ADD_COUNT);
        
        millec.RemoveAt(0);
        
        millec.ItemsCount.Should().Be(ADD_COUNT - 1);
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

        return;
        
        [MethodImpl(MethodImplOptions.NoInlining)]
        void DoSomething(int item)
        {
            
        }
    }
}