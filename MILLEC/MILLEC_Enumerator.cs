using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MILLEC;

public unsafe partial struct MILLEC<T>
{
    public ref struct Enumerator
    {
        private readonly ref T FirstItem, LastItem;
        private ref T CurrentItem;

        // private ref byte CurrentBitVector;

        private readonly BitVectorsArrayInterfacer BitVectorsArrayInterfacer;

        public ref T Current => ref CurrentItem;

        internal Enumerator(ItemsArrayInterfacer itemsArrayInterfacer, BitVectorsArrayInterfacer bitVectorsArrayInterfacer)
        {
            FirstItem = ref itemsArrayInterfacer.FirstItem;
            // MoveNext() is always called before the first iteration
            CurrentItem = ref Unsafe.Subtract(ref FirstItem, 1);
            // CurrentBitVector = ref bitArrayInterfacer.FirstItem;
            BitVectorsArrayInterfacer = bitVectorsArrayInterfacer;
        }

        public bool MoveNext()
        {
// TODO: Improve performance of this.
MoveNext:
            CurrentItem = ref Unsafe.Add(ref CurrentItem, 1);

            if (!Unsafe.IsAddressGreaterThan(ref CurrentItem, ref LastItem))
            {
                var currentIndex = IndexOfItemRef(ref FirstItem, ref CurrentItem);

                if (new BitInterfacer(BitVectorsArrayInterfacer, currentIndex).IsSet)
                {
                    return true;
                }

                goto MoveNext;
            }

            return false;
        }
    }

    public Enumerator GetEnumerator()
    {
        return new Enumerator(new ItemsArrayInterfacer(_itemsArr), new BitVectorsArrayInterfacer(_bitVecsArr));
    }
}
