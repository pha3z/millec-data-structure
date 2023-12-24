using System.Runtime.CompilerServices;
using FluentAssertions;
using MILLEC;

namespace Tests;

public class RemovalTests
{
    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(8)]
    public void ItemCountIsValidDuringIterativeRemoval(int capacity)
    {
        var millec = TestMillec.New(capacity, capacity: 8);

        for (int i = 0; i < capacity; i++)
        {
            millec.RemoveAt(i);
            millec.Count.Should().Be(capacity - i - 1);
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
    public void AfterRemovingItems_IndexedAccessibilityMatchesAvailabilityOfSlot(int itemCount, int[] removeTheseIndices)
    {
        var millec = TestMillec.New(itemCount, capacity: 8);
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
    public void ItemCountIsValidDuringRandomRemovals(int itemCount, int[] removeTheseIndices)
    {
        var millec = TestMillec.New(itemCount, capacity: 8);
        for (int i = 0; i < removeTheseIndices.Length; i++)
        {
            millec.RemoveAt(i);
            millec.Count.Should().Be(itemCount - i - 1);
        }
    }

}