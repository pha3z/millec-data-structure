using System.Runtime.CompilerServices;
using FluentAssertions;
using MILLEC;

namespace Tests;

public class AdditionTests
{

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(8)]
    public void ItemCountIsValidDuringIterativeAdditions(int capacity)
    {
        var millec = new MILLEC<int>(capacity);
        for (int i = 0; i < capacity; i++)
        {
            millec.Add(777);
            millec.Count.Should().Be(i + 1);
        }
    }
    
    [Test]
    [TestCase(0, 1)]
    [TestCase(1, 1)]
    [TestCase(0, 8)]
    [TestCase(1, 8)]
    [TestCase(8, 8)]
    public void AfterAddingItems_IndexedAccessibilityMatchesAvailabilityOfSlot(int itemCount, int capacity)
    {
        var millec = new MILLEC<int>(8);
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

    [Test]
    [TestCase(0, 1)]
    [TestCase(1, 1)]
    [TestCase(0, 8)]
    [TestCase(1, 8)]
    [TestCase(8, 8)]
    public void AfterAddingItems_IterationByRefReturnsCorrectValue(int itemCount, int capacity)
    {
        var millec = new MILLEC<int>(capacity);
        for (int i = 0; i < itemCount; i++)
        {
            millec.Add(777 + i);

            int j = 0;
            foreach(ref var x in millec)
            {
                x.Should().Be(777 + j);
                j++;
            }
        }
    }

    [Test]
    [TestCase(0, 1)]
    [TestCase(1, 1)]
    [TestCase(0, 8)]
    [TestCase(1, 8)]
    [TestCase(8, 8)]
    public void AfterAddingItems_IterationByIndexReturnsCorrectValue(int itemCount, int capacity)
    {
        var millec = new MILLEC<int>(capacity);
        for (int i = 0; i < itemCount; i++)
        {
            millec.Add(777 + i);

            int j = 0;
            foreach (int idx in millec)
            {
                millec[idx].Should().Be(777 + j);
                j++;
            }
        }
    }
}