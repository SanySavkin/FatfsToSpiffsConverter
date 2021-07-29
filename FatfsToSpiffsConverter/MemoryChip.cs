using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FatfsToSpiffsConverter
{
    enum FlashTypes
    {
        W25Q80 = 0,
        W25Q16,
        W25Q32,
        W25Q64,
        W25Q128
    }
    class MemoryChip
    {
        public static uint GetSize(FlashTypes type)
        {
            switch (type)
            {
                case FlashTypes.W25Q80:
                    return 0x100000;
                case FlashTypes.W25Q16:
                    return 0x200000;
                case FlashTypes.W25Q32:
                    return 0x400000;
                case FlashTypes.W25Q64:
                    return 0x800000;
                case FlashTypes.W25Q128:
                    return 0x1000000;
            }
            return 0;
        }
    }
}
