using System.Runtime.CompilerServices;
using FluentAssertions;
using MILLEC;

namespace Tests;

public class Tests
{
    static MILLEC<int> NewTestMillec(int itemCount)
    {
        var millec = new MILLEC<int>();
        for (int i = 0; i < itemCount; i++)
            millec.Add(i);

        return millec;
    }

    [Test]
    public void NewMillecHasZeroItemCount()
    {
        var millec = new MILLEC<int>(5);
        millec.ItemsCount.Should().Be(0);
    }

    [Test]
    public void ItemCountShouldBeValidAfterEveryAddSinceEmpty()
    {
        const int ITEM_COUNT = 5;
        var millec = new MILLEC<int>();

        for (int i = 0; i < ITEM_COUNT; i++)
        {
            millec.Add(i);
            millec.ItemsCount.Should().Be(i + 1);
        }
    }
    
    [Test]
    public void ItemCountShouldBeValidAfterEveryRemoveSinceFull()
    {
        const int ITEM_COUNT = 5;
        var millec = NewTestMillec(ITEM_COUNT);
        
        for (int i = 0; i < ITEM_COUNT; i++)
        {
            millec.RemoveAt(i);
            millec.ItemsCount.Should().Be(ITEM_COUNT - i - 1);
        }        
    }

    [Test]
    [TestCase(new int[] { 0, 1, 2, 3, 4 })]
    [TestCase(new int[] { 0, 1, 2, 3})]
    [TestCase(new int[] { 0, 1, 2})]
    [TestCase(new int[] { 0, 1})]
    [TestCase(new int[] { 0})]
    [TestCase(new int[] { 4, 3, 2, 1, 0})]
    [TestCase(new int[] { 4, 3, 2, 1})]
    [TestCase(new int[] { 4, 3, 2})]
    [TestCase(new int[] { 4, 3})]
    [TestCase(new int[] { 4})]
    [TestCase(new int[] { 3, 2, 1, 0 })]
    [TestCase(new int[] { 2, 1, 0 })]
    [TestCase(new int[] { 1, 0 })]
    [TestCase(new int[] { 4, 0 })]
    [TestCase(new int[] { 0, 4 })]
    public void HolesShouldBeInaccessibleWhenUsingIndexer(int[] removeTheseIndices)
    {
        const int ITEM_COUNT = 5;
        var millec = NewTestMillec(ITEM_COUNT);

        for (int i = 0; i < removeTheseIndices.Length; i++)
        {
            millec.RemoveAt(i);
            Assert.Throws<Exception>(() => { int x = millec[i]; });
        }
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
            try
            {
                DoSomething(millec[i]);
            }

            catch (Exception ex)
            {
                continue;
            }
                
            Assert.IsTrue(false);
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