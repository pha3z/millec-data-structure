using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MILLEC;

public static class MILLECDebuggingExtensions
{
    public static T[] GetItemsArray<T>(ref this MILLEC<T> instance)
    {
        return instance._itemsArr;
    }

    public static byte[] GetBitVectorsArr<T>(ref this MILLEC<T> instance)
    {
        return instance._bitVecsArr;
    }

    public static int GetHighestTouchedIndex<T>(ref this MILLEC<T> instance)
    {
        return instance._highestTouchedIndex;
    }
}
