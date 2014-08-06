﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="extras.array.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BABYLON
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// </summary>
    public class ArrayConvert
    {
        /// <summary>
        /// </summary>
        /// <param name="array">
        /// </param>
        /// <param name="start">
        /// </param>
        /// <param name="length">
        /// </param>
        /// <returns>
        /// </returns>
        public static byte[] AsByte(byte[] array, int start, int length)
        {
            var newArray = new byte[length];
            for (var index = 0; index < length; index++)
            {
                newArray[index] = array[start + index];
            }

            return newArray;
        }

        /// <summary>
        /// </summary>
        /// <param name="array">
        /// </param>
        /// <returns>
        /// </returns>
        public static float[] AsFloat(double[] array)
        {
            var newArray = new float[array.Length];
            for (var index = 0; index < array.Length; index++)
            {
                newArray[index] = (float)array[index];
            }

            return newArray;
        }

        /// <summary>
        /// </summary>
        /// <param name="array">
        /// </param>
        /// <returns>
        /// </returns>
        public static float[] AsFloat(Array<double> array)
        {
            var newArray = new float[array.Length];
            for (var index = 0; index < array.Length; index++)
            {
                newArray[index] = (float)array[index];
            }

            return newArray;
        }

        /// <summary>
        /// </summary>
        /// <param name="array">
        /// </param>
        /// <param name="start">
        /// </param>
        /// <param name="length">
        /// </param>
        /// <returns>
        /// </returns>
        public static int[] AsInt(byte[] array, int start, int length)
        {
            var newArray = new int[length];
            for (var index = 0; index < length; index++)
            {
                newArray[index] = array[start + index];
            }

            return newArray;
        }

        /// <summary>
        /// </summary>
        /// <param name="array">
        /// </param>
        /// <returns>
        /// </returns>
        public static ushort[] AsUshort(int[] array)
        {
            var newArray = new ushort[array.Length];
            for (var index = 0; index < array.Length; index++)
            {
                newArray[index] = (ushort)array[index];
            }

            return newArray;
        }

        /// <summary>
        /// </summary>
        /// <param name="array">
        /// </param>
        /// <returns>
        /// </returns>
        public static ushort[] AsUshort(Array<int> array)
        {
            var newArray = new ushort[array.Length];
            for (var index = 0; index < array.Length; index++)
            {
                newArray[index] = (ushort)array[index];
            }

            return newArray;
        }
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="K">
    /// </typeparam>
    /// <typeparam name="V">
    /// </typeparam>
    public class Map<K, V> : Dictionary<K, V>
    {
        /// <summary>
        /// </summary>
        /// <param name="k">
        /// </param>
        /// <returns>
        /// </returns>
        public new V this[K k]
        {
            get
            {
                if (!this.ContainsKey(k))
                {
                    return default(V);
                }

                return base[k];
            }

            set
            {
                base[k] = value;
            }
        }
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class Array<T> : IEnumerable<T>
    {
        private const int _defaultCapacity = 4;

        private T[] _items;
        private int _size;
        private int _version;

        static T[] _emptyArray = new T[0];        

        /// <summary>
        /// </summary>
        public Array()
        {
            _items = _emptyArray;
        }

        /// <summary>
        /// </summary>
        /// <param name="item">
        /// </param>
        public Array(T item)
        {
            this.Add(item);
        }

        /// <summary>
        /// </summary>
        /// <param name="item1">
        /// </param>
        /// <param name="item2">
        /// </param>
        public Array(T item1, T item2)
        {
            this.Add(item1);
            this.Add(item2);
        }

        /// <summary>
        /// </summary>
        /// <param name="item1">
        /// </param>
        /// <param name="item2">
        /// </param>
        /// <param name="item3">
        /// </param>
        public Array(T item1, T item2, T item3)
        {
            this.Add(item1);
            this.Add(item2);
            this.Add(item3);
        }

        /// <summary>
        /// </summary>
        /// <param name="items">
        /// </param>
        public Array(params T[] items)
        {
            this.AddRange(items);
        }

        /// <summary>
        /// </summary>
        /// <param name="i">
        /// </param>
        /// <returns>
        /// </returns>
        public T this[int i]
        {
            get
            {
                if (i >= this.Count)
                {
                    return default(T);
                }

                return _items[i];
            }

            set
            {
                while (i >= this.Count)
                {
                    this.Add(default(T));
                }

                _items[i] = value;
            }
        }

        /// <summary>
        /// </summary>
        public int Length
        {
            get
            {
                return this.Count;
            }

            set
            {
                this.Capacity = value;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="v1">
        /// </param>
        /// <param name="v2">
        /// </param>
        public void Add(T v1, T v2)
        {
            this.Add(v1);
            this.Add(v2);
        }

        /// <summary>
        /// </summary>
        /// <param name="v1">
        /// </param>
        /// <param name="v2">
        /// </param>
        /// <param name="v3">
        /// </param>
        public void Add(T v1, T v2, T v3)
        {
            this.Add(v1);
            this.Add(v2);
            this.Add(v3);
        }

        /// <summary>
        /// </summary>
        /// <param name="items">
        /// </param>
        public void Add(params T[] items)
        {
            this.AddRange(items);
        }

        /// <summary>
        /// </summary>
        /// <param name="other">
        /// </param>
        /// <returns>
        /// </returns>
        public Array<T> Append(Array<T> other)
        {
            foreach (var item in other)
            {
                this.Add(item);
            }

            return this;
        }

        /// <summary>
        /// </summary>
        /// <param name="joinSubstring">
        /// </param>
        /// <returns>
        /// </returns>
        public string Concat(string joinSubstring)
        {
            var sb = new StringBuilder();
            var first = true;
            foreach (var item in this)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    sb.Append(joinSubstring);
                }

                sb.Append(item);
            }

            return sb.ToString();
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public T Pop()
        {
            var t = this[this.Length - 1];
            this.RemoveAt(this.Length - 1);
            return t;
        }

        /// <summary>
        /// </summary>
        /// <param name="compareFn">
        /// </param>
        public void Sort(Func<T, T, int> compareFn)
        {
            this.Sort(new ComparerAdapter(compareFn));
        }

        /// <summary>
        /// </summary>
        /// <param name="index">
        /// </param>
        /// <param name="length">
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public Array<T> slice(int index, int length = 0)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <param name="index">
        /// </param>
        /// <param name="size">
        /// </param>
        /// <param name="newKeys">
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public void splice(int index, int size, Array<T> newKeys)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        public class ComparerAdapter : IComparer<T>
        {
            /// <summary>
            /// </summary>
            private readonly Func<T, T, int> compareFn;

            /// <summary>
            /// </summary>
            /// <param name="compareFn">
            /// </param>
            public ComparerAdapter(Func<T, T, int> compareFn)
            {
                this.compareFn = compareFn;
            }

            /// <summary>
            /// </summary>
            /// <param name="x">
            /// </param>
            /// <param name="y">
            /// </param>
            /// <returns>
            /// </returns>
            public int Compare(T x, T y)
            {
                return this.compareFn(x, y);
            }
        }

        #region implementation

        /// <summary>
        /// </summary>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public int Capacity
        {
            get { return _items.Length; }
            set
            {
                if (value != _items.Length)
                {
                    if (value > 0)
                    {
                        T[] newItems = new T[value];
                        if (_size > 0)
                        {
                            Array.Copy(_items, 0, newItems, 0, _size);
                        }

                        _items = newItems;
                    }
                    else
                    {
                        _items = _emptyArray;
                    }
                }
            }

        }

        /// <summary>
        /// </summary>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public int Count
        {
            get { return _size; }
        }

        /// <summary>
        /// </summary>
        /// <param name="item">
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public void Add(T item)
        {
            if (_size == _items.Length) EnsureCapacity(_size + 1);
            _items[_size++] = item;
            _version++;
        }

        /// <summary>
        /// </summary>
        /// <param name="items">
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public void AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                this.Add(item);
            }
        }

        /// <summary>
        /// </summary>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public void Clear()
        {
            Array.Clear(_items, 0, _size);
            _size = 0;
            _version++;
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <param name="t">
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public int IndexOf(T item)
        {
            return Array.IndexOf(_items, item, 0, _size);
        }

        /// <summary>
        /// </summary>
        /// <param name="i">
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public void RemoveAt(int index)
        {
            if ((uint)index >= (uint)_size)
            {
                throw IndexOutOfRangeException();
            }

            _size--;
            if (index < _size)
            {
                Array.Copy(_items, index + 1, _items, index, _size - index);
            }
            _items[_size] = default(T);
            _version++;
        }

        /// <summary>
        /// </summary>
        /// <param name="comparerAdapter">
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public void Sort(ComparerAdapter comparerAdapter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void EnsureCapacity(int min)
        {
            if (_items.Length < min)
            {
                int newCapacity = _items.Length == 0 ? _defaultCapacity : _items.Length * 2;
                if (newCapacity < min) newCapacity = min;
                Capacity = newCapacity;
            }
        }

        #endregion
    }
}