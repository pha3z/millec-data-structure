using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MILLEC;

public unsafe partial struct MILLEC<T>
{
    internal readonly ref struct BitInterfacer
    {
        private readonly ref byte Slot;

        private readonly int VectorIndex;

        public BitInterfacer(BitVectorsArrayInterfacer bitVectorsArrayInterfacer, int slotIndex)
        {
            // E.x. index 7 -> 7 / 8 -> Q:0 R:7, 8 -> 8 / 8 -> Q:1 R:0, 9 -> 9 / 8 ->  Q:1 R:1
            var index = Math.DivRem(slotIndex, BYTE_BIT_COUNT, out VectorIndex);

            Slot = ref Unsafe.Add(ref bitVectorsArrayInterfacer.FirstItem, index);
        }

        public bool IsWholeByteClear()
            => Slot == 0;        

        public bool IsSet 
            => (Slot & (1 << VectorIndex)) != 0;

        public void Set()
            => Slot |= unchecked((byte)(1 << VectorIndex));

        public void Clear()
            => Slot &= unchecked((byte)~(1 << VectorIndex));
    }

}
