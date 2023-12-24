using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MILLEC;

public unsafe partial struct MILLEC<T>
{
    internal struct FreeSlot
    {
        public int Next;

        public FreeSlot() : this(NO_NEXT_SLOT_VALUE) { }

        public FreeSlot(int next) => Next = next;

        public ref FreeSlot GetNextFreeSlot(ItemsArrayInterfacer itemsArrayInterfacer)
        {
            var next = Next;

            if (next != NO_NEXT_SLOT_VALUE)
                return ref Unsafe.As<T, FreeSlot>(ref Unsafe.Add(ref itemsArrayInterfacer.FirstItem, next));

            return ref Unsafe.NullRef<FreeSlot>();
        }

        [UnscopedRef]
        public ref T ReinterpretAsItem()
            => ref Unsafe.As<FreeSlot, T>(ref this);
        
        public static ref FreeSlot ReinterpretItemAsFreeSlot(ref T item)
            => ref Unsafe.As<T, FreeSlot>(ref item);
        
    }
}
