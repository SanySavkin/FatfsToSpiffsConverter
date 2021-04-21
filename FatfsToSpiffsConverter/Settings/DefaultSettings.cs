using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FatfsToSpiffsConverter
{
    public static class DefaultSettings
    {
        public static readonly MainSettings indoorTager3_0 = new MainSettings
        {
            flashSize = 0x300000,
            spiffsAddress = 0x100000,
            eraseSize = 0x10000,
            logPageSize = 0x200,
            blockSize = 0x10000,
            allowFormating = true,
            useSpiffs = true,
            pathFatfs = "indoor/tager/snd",
            pathSpiffs = "snd"
        };

        public static readonly MainSettings indoorVest3_0 = new MainSettings
        {
            flashSize = 0xF00000,
            spiffsAddress = 0x100000,
            eraseSize = 0x10000,
            logPageSize = 0x400,
            blockSize = 0x10000,
            allowFormating = true,
            useSpiffs = true,
            pathFatfs = "indoor/vest/snd",
            pathSpiffs = "snd"
        };

        public static readonly MainSettings outdoorTager = new MainSettings
        {
            flashSize = 0x300000,
            spiffsAddress = 0x100000,
            eraseSize = 0x10000,
            logPageSize = 0x100,
            blockSize = 0x10000,
            allowFormating = true,
            useSpiffs = true,
            pathFatfs = "outdoor/tager/snd",
            pathSpiffs = "snd"
        };
    }
}
