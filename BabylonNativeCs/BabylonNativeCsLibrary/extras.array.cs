﻿namespace BABYLON
{
    using System;

    public class Array<T>
    {
        public Array()
        {
            throw new NotImplementedException();
        }

        public Array(T item)
        {
            throw new NotImplementedException();
        }

        public Array(T item1, T item2)
        {
            throw new NotImplementedException();
        }

        public int Length
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void push(T v)
        {
            throw new NotImplementedException();
        }

        public void push(T v1, T v2)
        {
            throw new NotImplementedException();
        }

        public int indexOf(T t)
        {
            throw new NotImplementedException();
        }

        public Array<T> slice(int index)
        {
            throw new NotImplementedException();
        }

        public string join(string joinSubstr)
        {
            throw new NotImplementedException();
        }

        public void splice(int index)
        {
            throw new NotImplementedException();
        }

        public void splice(int index, int size)
        {
            throw new NotImplementedException();
        }

        public void splice(int index, int size, Array<T> newKeys)
        {
            throw new NotImplementedException();
        }

        public void forEach(Func<T> func)
        {
            throw new NotImplementedException();
        }

        public T this[int subIndex]
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }

    public class SmartArray<T> : Array<T>
    {
        public SmartArray(int reserveSize)
        {
            throw new NotImplementedException();
        }

        public void reset()
        {
            throw new NotImplementedException();
        }
    }

    public class Float32Array : SmartArray<float>
    {
        public Float32Array(int reserveSize)
            : base(reserveSize)
        {
        }
    }

    public class Int32Array : SmartArray<int>
    {
        public Int32Array(int reserveSize)
            : base(reserveSize)
        {
        }
    }

    public class Uint8Array : SmartArray<byte>
    {
        public Uint8Array(int reserveSize)
            : base(reserveSize)
        {
        }
    }

    public class Int8Array : SmartArray<sbyte>
    {
        public Int8Array(int reserveSize)
            : base(reserveSize)
        {
        }
    }

    public class Int16Array : SmartArray<short>
    {
        public Int16Array(int reserveSize)
            : base(reserveSize)
        {
        }
    }

    public class Uint16Array : SmartArray<ushort>
    {
        public Uint16Array(int reserveSize)
            : base(reserveSize)
        {
        }
    }
}
