using System.Runtime.CompilerServices;
using FluentAssertions;
using MILLEC;

namespace Tests.Tests;

public class UntouchedMillecTests
{
    [Test]
    public void NewMillecHasZeroItemCount()
    {
        var millec = new MILLEC<int>(5);
        millec.Count.Should().Be(0);
    }

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(8)]
    public void IndexedAccessToUntouchedSlotsShouldError(int capacity)
    {
        var millec = new MILLEC<int>(capacity);

        for (int i = 0; i < capacity; i++)
            Assert.Throws<Exception>(() => { int x = millec[i]; });
    }

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(8)]
    public void IndexedAccessOutOfBoundsShouldError(int capacity)
    {
        var millec = new MILLEC<int>(capacity);
        Assert.Throws<Exception>(() => { int x = millec[-1]; });
        Assert.Throws<Exception>(() => { int x = millec[capacity]; });
    }

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(8)]
    public void ByRefEnumerationReturnsZeroItems(int capacity)
    {
        var millec = new MILLEC<int>(capacity);
        foreach (ref var x in millec)
            throw new Exception("This exception should not occur because there are no items to enumerator.");
    }

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(8)]
    public void IndexEnumerationReturnsZeroItems(int capacity)
    {
        var millec = new MILLEC<int>(capacity);
        foreach (int idx in millec)
            throw new Exception("This exception should not occur because there are no items to enumerator.");
    }
}