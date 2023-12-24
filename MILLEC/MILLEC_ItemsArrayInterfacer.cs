using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MILLEC;

public unsafe partial struct MILLEC<T>
{
    internal readonly ref struct ItemsArrayInterfacer
    {
        public readonly ref T FirstItem;

        public ItemsArrayInterfacer(T[] itemsArr)
            => FirstItem = ref MemoryMarshal.GetArrayDataReference(itemsArr);

        public ref T this[int index] 
            => ref Unsafe.Add(ref FirstItem, index);
        
        public ref T GetLastSlotOffsetByOne(T[] itemsArr) 
            => ref this[itemsArr.Length];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T GetFirstFreeOrNewSlot(FreeSlot firstFreeSlotFieldValue, ref int newSlotWriteIndex, out bool isNewSlot)
        {
            var next = firstFreeSlotFieldValue.Next;
            isNewSlot = next == -1;
            newSlotWriteIndex = isNewSlot ? newSlotWriteIndex : next;
            return ref this[newSlotWriteIndex];
        }
    }
}
