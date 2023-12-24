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
        millec.Count.Should().Be(0);
    }

    [Test]
    public void ItemCountShouldBeValidAfterEveryAdd()
    {
        const int ITEM_COUNT = 5;
        var millec = new MILLEC<int>();

        for (int i = 0; i < ITEM_COUNT; i++)
        {
            millec.Add(777);
            millec.Count.Should().Be(i + 1);
        }
    }
    
    [Test]
    public void ItemCountShouldBeValidAfterEveryRemove()
    {
        const int ITEM_COUNT = 5;
        var millec = NewTestMillec(ITEM_COUNT);
        
        for (int i = 0; i < ITEM_COUNT; i++)
        {
            millec.RemoveAt(i);
            millec.Count.Should().Be(ITEM_COUNT - i - 1);
        }        
    }

    [Test]
    [TestCase(3, new int[] { 0 })]
    [TestCase(3, new int[] { 0, 1 })]
    [TestCase(3, new int[] { 0, 2 })]
    [TestCase(3, new int[] { 0, 1, 2 })]
    [TestCase(3, new int[] { 1 })]
    [TestCase(3, new int[] { 1, 0 })]
    [TestCase(3, new int[] { 1, 2 })]
    [TestCase(3, new int[] { 1, 0, 2 })]
    [TestCase(3, new int[] { 2 })]
    [TestCase(3, new int[] { 2, 1 })]
    [TestCase(3, new int[] { 2, 0 })]
    [TestCase(3, new int[] { 2, 1, 0 })]

    [TestCase(4, new int[] { 0 })]
    [TestCase(4, new int[] { 0, 1 })]
    [TestCase(4, new int[] { 0, 2 })]
    [TestCase(4, new int[] { 0, 1, 2 })]
    [TestCase(4, new int[] { 1 })]
    [TestCase(4, new int[] { 1, 0 })]
    [TestCase(4, new int[] { 1, 2 })]
    [TestCase(4, new int[] { 1, 0, 2 })]
    [TestCase(4, new int[] { 2 })]
    [TestCase(4, new int[] { 2, 1 })]
    [TestCase(4, new int[] { 2, 0 })]
    [TestCase(4, new int[] { 2, 1, 0 })]
    public void AfterRemovingItems_SlotAccessibilityShouldMatchFreeOrUnfreeStateOfSlot(int itemCount, int[] removeTheseIndices)
    {
        var millec = NewTestMillec(itemCount);
        List<int> removedPositions = new List<int>();
        for (int i = 0; i < removeTheseIndices.Length; i++)
        {
            millec.RemoveAt(i);
            removedPositions.Add(i);
            Assert.Throws<Exception>(() => { int x = millec[i]; });
            
            for(int j = 0; j < itemCount; j++)
            {
                if (removedPositions.Contains(j))
                    Assert.Throws<Exception>(() => { int x = millec[j]; });
                else
                    Assert.DoesNotThrow(() => { int x = millec[j]; });
            }
        }
    }

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(8)]
    public void AfterAddingItems_SlotAccessibilityShouldMatchFreeOrUnfreeStateOfSlot(int itemCount)
    {
        var millec = NewTestMillec(itemCount);
        List<int> addedPositions = new List<int>();
        for (int i = 0; i < itemCount; i++)
        {
            millec.Add(777);
            addedPositions.Add(i);

            for (int j = 0; j < millec.Capacity; j++)
            {
                if (addedPositions.Contains(j))
                    Assert.DoesNotThrow(() => { int x = millec[j]; });
                else
                    Assert.Throws<Exception>(() => { int x = millec[j]; });
            }
        }
    }
}