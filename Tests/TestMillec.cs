using MILLEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    internal class TestMillec
    {
        public static MILLEC<int> New(int itemCount, int capacity)
        {
            var millec = new MILLEC<int>(capacity);
            for (int i = 0; i < itemCount; i++)
                millec.Add(i);

            return millec;
        }
    }
}
