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

		public EggArray() : this(8)
		{
		}
		
		public EggArray(int capacity)
		{
			this.capacity = capacity;
			this.items = new T[capacity];
		}

		public int Count
		{
			get
			{
				return this.count;
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
		public T First
		{
			get
			{
				if (this.count > 0)
				{
					return this.items[0];
				}
				return (T)((object)null);
			}
		}
		public T Last
		{
			get
			{
				if (this.count > 0)
				{
					return this.items[this.count - 1];
				}
				return (T)((object)null);
			}
		}
		public int Capacity
		{
			get
			{
				return this.capacity;
			}
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

		public void Insert(int index, T item)
		{
			if (this.count >= this.capacity)
			{
				this.Resize(this.capacity * 2);
			}
			this.count++;
			for (int i = this.count - 1; i > index; i--)
			{
				this.items[i] = this.items[i - 1];
			}
			this.items[index] = item;
		}

		public void SetNull(int index)
		{
			this.items[index] = (T)((object)null);
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
						this.items[i] = (T)((object)null);
					}
				}
			}
			this.count -= num;
		}

		public void Clear()
		{
			if (this.count > 0)
			{
				for (int i = 0; i < this.count; i++)
				{
					this.items[i] = (T)((object)null);
				}
				this.count = 0;
			}
		}

		public void Add(T item)
		{
			if (this.count >= this.capacity)
			{
				this.Resize(this.capacity * 2);
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

		public void Set(EggArray<T> other)
		{
			this.Clear();
			for (int i = 0; i < other.count; i++)
			{
				this.Add(other.items[i]);
			}
		}

		public bool Contains(T arg0)
		{
			for (int i = 0; i < this.count; i++)
			{
				if (this.items[i].Equals(arg0))
				{
					return true;
				}
			}
			return false;
		}

		public bool ContainsStrict(T arg0)
		{
			for (int i = 0; i < this.count; i++)
			{
				if (this.items[i] == arg0)
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

		public bool Remove(T arg0)
		{
			for (int i = 0; i < this.count; i++)
			{
				if (this.items[i].Equals(arg0))
				{
					this.items[i] = (T)((object)null);
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
				this.items[index] = (T)((object)null);
				this.Compact();
			}
		}

		public T RemoveLast()
		{
			T result = (T)((object)null);
			if (this.count > 0)
			{
				result = this.items[this.count - 1];
				this.items[this.count - 1] = (T)((object)null);
				this.count--;
			}
			return result;
		}

		public int IndexOf(T arg0)
		{
			for (int i = 0; i < this.count; i++)
			{
				if (this.items[i].Equals(arg0))
				{
					return i;
				}
			}
			return -1;
		}

		public int IndexOfStrict(T arg0)
		{
			for (int i = 0; i < this.count; i++)
			{
				if (this.items[i] == arg0)
				{
					return i;
				}
			}
			return -1;
		}

		public int LastIndexOf(T arg0)
		{
			for (int i = this.count - 1; i >= 0; i--)
			{
				if (this.items[i].Equals(arg0))
				{
					return i;
				}
			}
			return -1;
		}

		public void Resize(int capacity)
		{
			if (this.capacity != capacity)
			{
				if (this.count > capacity)
				{
					this.count = capacity;
				}
				T[] destinationArray = new T[capacity];
				Array.Copy(this.items, destinationArray, this.count);
				this.items = destinationArray;
				this.capacity = capacity;
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
