using System.Runtime.CompilerServices;
using FluentAssertions;
using MILLEC;

namespace Tests;

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
}