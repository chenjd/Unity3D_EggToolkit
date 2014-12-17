/// <summary>
/// EggArray
/// Created by chenjd
/// http://www.cnblogs.com/murongxiaopifu/
/// </summary>
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
namespace EggToolkit
{
	public class EggArray<T> where T : class
	{
		public delegate void IterationHandler(T item);
		public delegate bool IterationBoolHandler(T item);
		public delegate T IterationVauleHandler(T item);
		
		private int capacity;
		private int count;
		private T[] items;
		private int growthFactor = 2;

		public EggArray() : this(8)
		{
		}
		
		public EggArray(int capacity)
		{
			this.capacity = capacity;
			this.items = new T[capacity];
		}
		/// <summary>
		/// Attr
		/// </summary>
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		public int Capacity
		{
			get
			{
				return this.capacity;
			}
		}

		public bool IsEmpty
		{
			get
			{
				return this.count == 0;
			}
		}
		public bool IsFull
		{
			get
			{
				return this.count == this.capacity;
			}
		}
		/// <summary>
		/// Methods
		/// </summary>
		private void Resize()
		{
			int capacity = this.capacity * growthFactor;
			if (this.count > capacity)
			{
				this.count = capacity;
			}
			T[] destinationArray = new T[capacity];
			Array.Copy(this.items, destinationArray, this.count);
			this.items = destinationArray;
			this.capacity = capacity;
		}

		public void Add(T item)
		{
			if (this.count >= this.capacity)
			{
				this.Resize();
			}
			this.items[this.count++] = item;
		}
		
		public void AddRange(IEnumerable<T> collection)
		{
			if (collection != null)
			{
				foreach (T current in collection)
				{
					this.Add(current);
				}
			}
		}

		public void Insert(int index, T item)
		{
			if (this.count >= this.capacity)
			{
				this.Resize();
			}
			this.count++;
			for (int i = this.count - 1; i > index; i--)
			{
				this.items[i] = this.items[i - 1];
			}
			this.items[index] = item;
		}

		public bool Contains(T arg)
		{
			for (int i = 0; i < this.count; i++)
			{
				if (this.items[i].Equals(arg))
				{
					return true;
				}
			}
			return false;
		}
		
		public void Clear()
		{
			if (this.count > 0)
			{
				for (int i = 0; i < this.count; i++)
				{
					this.items[i] = null;
				}
				this.count = 0;
			}
		}

		public void ToArray(T[] array)
		{
			if (array != null)
			{
				for (int i = 0; i < this.count; i++)
				{
					array[i] = this.items[i];
				}
			}
		}

		public void Sort(IComparer<T> comparer)
		{
			Array.Sort<T>(this.items, 0, this.count, comparer);
		}
		
		public void Foreach(EggArray<T>.IterationHandler handler)
		{
			for (int i = 0; i < this.count; i++)
			{
				handler(this.items[i]);
			}
		}

		public bool Remove(T arg)
		{
			for (int i = 0; i < this.count; i++)
			{
				if (this.items[i].Equals(arg))
				{
					this.items[i] = null;
					this.Compact();
					return true;
				}
			}
			return false;
		}
		
		public void RemoveAt(int index)
		{
			if (index < this.count)
			{
				this.items[index] = null;
				this.Compact();
			}
		}
		
		public T RemoveLast()
		{
			T result = null;
			if (this.count > 0)
			{
				result = this.items[this.count - 1];
				this.items[this.count - 1] = null;
				this.count--;
			}
			return result;
		}
		
		public int IndexOf(T arg)
		{
			for (int i = 0; i < this.count; i++)
			{
				if (this.items[i].Equals(arg))
				{
					return i;
				}
			}
			return -1;
		}



		public T First()
		{
			if (this.count > 0)
			{
				return this.items[0];
			}
			return null;
		}

		public T[] First(int n)
		{
			if(n < 0)
			{
				return null;
			}
			if (this.count > n)
			{
				T[] targetArray = this.Slice(0, n);
				return targetArray;
			}
			else
			{
				return this.items;
			}
		}

		public T Last()
		{
			if (this.count > 0)
			{
				return this.items[this.count - 1];
			}
			return null;
		}

		public T[] Last(int n)
		{
			if(n < 0)
			{
				return null;
			}
			if(this.count > n)
			{
				int startIndex = this.count - 1 - n;
				T[] target = this.Slice(startIndex, this.count - 1);
				return target;
			}
			else
			{
				return this.items;
			}
		}
		
		public T[] Slice(int start, int end)
		{
			if (end < 0)
			{
				end = this.items.Length + end;
			}
			int len = end - start;
			
			T[] res = new T[len];
			for (int i = 0; i < len; i++)
			{
				res[i] = this.items[i + start];
			}
			return res;
		}

		public T Get(int i)
		{
			return this.items[i];
		}

		public void Set(int index, T item)
		{
			this.items[index] = item;
		}

		public void AddFirst(T item)
		{
			this.Insert(0, item);
		}

		public void Compact()
		{
			int num = 0;
			for (int i = 0; i < this.count; i++)
			{
				if (this.items[i] == null)
				{
					num++;
				}
				else
				{
					if (num > 0)
					{
						this.items[i - num] = this.items[i];
						this.items[i] = null;
					}
				}
			}
			this.count -= num;
		}

		public void Set(EggArray<T> other)
		{
			this.Clear();
			for (int i = 0; i < other.count; i++)
			{
				this.Add(other.items[i]);
			}
		}

		public bool ContainsStrict(T arg)
		{
			for (int i = 0; i < this.count; i++)
			{
				if (this.items[i] == arg)
				{
					return true;
				}
			}
			return false;
		}

		public F TryGet<F>() where F : class, T
		{
			for (int i = 0; i < this.count; i++)
			{
				if (this.items[i] is F)
				{
					return (F)((object)this.items[i]);
				}
			}
			return (F)((object)null);
		}

		public int IndexOfStrict(T arg)
		{
			for (int i = 0; i < this.count; i++)
			{
				if (this.items[i] == arg)
				{
					return i;
				}
			}
			return -1;
		}

		public int LastIndexOf(T arg)
		{
			for (int i = this.count - 1; i >= 0; i--)
			{
				if (this.items[i].Equals(arg))
				{
					return i;
				}
			}
			return -1;
		}



		public void ForeachBreak(EggArray<T>.IterationBoolHandler handler)
		{
			for (int i = 0; i < this.count; i++)
			{
				if (handler(this.items[i]))
				{
					return;
				}
			}
		}

		public void ForeachReverse(EggArray<T>.IterationHandler handler)
		{
			for (int i = this.count - 1; i >= 0; i--)
			{
				handler(this.items[i]);
			}
		}

		public void ForeachBreakReverse(EggArray<T>.IterationBoolHandler handler)
		{
			for (int i = this.count - 1; i >= 0; i--)
			{
				if (handler(this.items[i]))
				{
					return;
				}
			}
		}

		public EggArray<T> Map(EggArray<T>.IterationVauleHandler handler)
		{
			EggArray<T> targetArray = new EggArray<T>(this.capacity);
			for(int i = 0; i < this.count; i++)
			{
				T t = handler(this.items[i]);
				targetArray.Add(t);
			}
			return targetArray;
		}
		
		public T Find(EggArray<T>.IterationBoolHandler handler)
		{
			for(int i = 0; i < this.count; i++)
			{
				if(handler(this.items[i]))
				{
					return this.items[i];
				}
			}
			return null;
		}
		
		public EggArray<T> Filter(EggArray<T>.IterationBoolHandler handler)
		{
			EggArray<T> targetArray = new EggArray<T>();
			for(int i = 0; i < this.count; i++)
			{
				if(handler(this.items[i]))
				{
					T t = this.items[i];
					targetArray.Add(t);
				}
			}
			return targetArray;
		}
	}
}
