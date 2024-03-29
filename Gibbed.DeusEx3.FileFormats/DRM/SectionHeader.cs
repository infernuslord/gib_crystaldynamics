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

using System;
using System.Collections.Generic;
using System.IO;
using Gibbed.IO;

namespace Gibbed.DeusEx3.FileFormats.DRM
{
    internal class SectionHeader
    {
        public uint DataSize;
        public SectionType Type;
        public byte Unknown05;
        public ushort Unknown06;
        public uint Flags;
        public uint Id;
        public uint Unknown10;

        private static Dictionary<byte, SectionType> ValidSectionTypes;

        static SectionHeader()
        {
            ValidSectionTypes = new Dictionary<byte, SectionType>();
            foreach (var value in Enum.GetValues(typeof(SectionType)))
            {
                ValidSectionTypes.Add((byte)value, (SectionType)value);
            }
        }

        public byte UnknownFlagValue
        {
            get { return (byte)((this.Flags & 0x000000FE) >> 1); }
        }

        public uint HeaderSize
        {
            get { return (this.Flags & 0xFFFFFF00) >> 8; }
            set
            {
                this.Flags &= ~0xFFFFFF00;
                this.Flags |= (value << 8) & 0xFFFFFF00;
            }
        }

        public void Deserialize(Stream input, Endian endian)
        {
            this.DataSize = input.ReadValueU32(endian);

            var type = input.ReadValueU8();
            if (ValidSectionTypes.ContainsKey(type) == false)
            {
                throw new FormatException("unknown section type");
            }
            this.Type = (SectionType)type;

            this.Unknown05 = input.ReadValueU8();
            this.Unknown06 = input.ReadValueU16(endian);
            this.Flags = input.ReadValueU32(endian);
            this.Id = input.ReadValueU32(endian);
            this.Unknown10 = input.ReadValueU32(endian);
        }

        public override string ToString()
        {
            return string.Format("{0:X2} {1:X4} {2:X8} {3:X8} {4:X8} : {5}",
                                 this.Unknown05,
                                 this.Unknown06,
                                 this.Flags,
                                 this.Id,
                                 this.Unknown10,
                                 this.Type);
            return this.Type.ToString();
        }
    }
}
