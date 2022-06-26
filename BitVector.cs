/*
* Based on ActionScript bitvector class by Michael Baczynski
* 
* Copyright (c) 2006 Michael Baczynski http://lab.polygonal.de
*
* Permission to use, copy, modify, distribute and sell this software
* and its documentation for any purpose is hereby granted without fee,
* provided that the above copyright notice appear in all copies.
* Michael Baczynski makes no representations about the suitability 
* of this software for any purpose.  
* It is provided "as is" without express or implied warranty.
*
* Description: 
* A bitvector is meant to condense bit values (or booleans) into
* an array as close as possible so that no space is wasted.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCLCP.Util
{
    public class BitVector
    {
        private uint[] _bits;
        private uint _size = 0;
        private uint _elementCount = 0;

        private const uint VECTOR_SIZE = 32;

        public BitVector(uint size)
        {
            _size = size;
            _elementCount = (uint)Math.Ceiling((decimal)(size / VECTOR_SIZE));
            _bits = new uint[_elementCount];
            for(int i = 0; i < _elementCount; i++)
            {
                _bits[i] = 0;
            }
        }

        public uint Size
        {
            get { return _size; }
            set
            {
                uint newSize;

                if (value % 31 == 0)
                {
                    newSize = value / 31;
                }
                else
                {
                    newSize = value / 31 + 1;
                }

                uint[] a = new uint[newSize];
                for (int i = 0; i < Math.Min(value, newSize); i++)
                {
                    a[i] = _bits[i];
                }
                _size = newSize;
                _bits = a;
                a = null;
            }
        }

        public void ClearAll()
        {
            for (int i = 0; i < _bits.Length; i++)
            {
                _bits[i] = 0;
            }
        }

        public void SetAll()
        {
            for (int i = 0; i < _bits.Length; i++)
            {
                _bits[i] = 0xffffffff;
            }
        }

        public void Set(uint i)
        {
            i--;
            uint dwordIndex = (uint)Math.Floor((decimal)(i / VECTOR_SIZE));
            uint bitIndex = i % VECTOR_SIZE;
            _bits[dwordIndex] = (uint)(_bits[dwordIndex] | (uint)(1 << (int)bitIndex));
        }

        public void Clear(uint i)
        {
            i--;
            uint dwordIndex = (uint)Math.Floor((decimal)(i / VECTOR_SIZE));
            uint bitIndex = i % VECTOR_SIZE;
            _bits[dwordIndex] = (uint)(_bits[dwordIndex] & ~(1 << (int)bitIndex));
        }

        public void Toggle(uint i)
        {
            i--;
            uint dwordIndex = (uint)Math.Floor((decimal)(i / VECTOR_SIZE));
            uint bitIndex = i % VECTOR_SIZE;
            _bits[dwordIndex] = (uint)(_bits[dwordIndex] ^ (1 << (int)bitIndex));
        }

        public uint Get(uint i)
        {
            i--;
            uint dwordIndex = (uint)Math.Floor((decimal)(i / VECTOR_SIZE));
            uint bitIndex = i % VECTOR_SIZE;
            return ((uint)(_bits[dwordIndex] & (1 << (int)bitIndex)));
        }

        public bool GetBool(uint i)
        {
            i--;
            uint dwordIndex = (uint)Math.Floor((decimal)(i / VECTOR_SIZE));
            uint bitIndex = i % VECTOR_SIZE;
            return ((uint)(_bits[dwordIndex] & (1 << (int)bitIndex))) == 1 ? true : false;
        }
    }
}
