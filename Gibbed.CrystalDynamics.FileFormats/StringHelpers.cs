﻿/* Copyright (c) 2013 Rick (rick 'at' gibbed 'dot' us)
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 * 
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 * 
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

namespace Gibbed.CrystalDynamics.FileFormats
{
    public static class StringHelpers
    {
        public static uint HashFileName(this string input)
        {
            uint hash = 0xFFFFFFFFu;
            foreach (char c in input)
            {
                hash ^= (uint)c << 24;

                for (int j = 0; j < 8; j++)
                {
                    if ((hash & 0x80000000) != 0)
                    {
                        hash = (hash << 1) ^ 0x04C11DB7u;
                    }
                    else
                    {
                        hash <<= 1;
                    }
                }
            }
            return ~hash;
        }
    }
}
