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

    internal readonly ref struct BitVectorsArrayInterfacer
    {
        public readonly ref byte FirstItem;

        public BitVectorsArrayInterfacer(byte[] bitVectorsArray)
            => FirstItem = ref MemoryMarshal.GetArrayDataReference(bitVectorsArray);

        public ref byte this[int index]
            => ref Unsafe.Add(ref FirstItem, index);
    }

}
