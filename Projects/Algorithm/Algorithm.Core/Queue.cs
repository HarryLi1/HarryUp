using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Core
{
    /// <summary>
    /// 利用两个栈实现队列的Enqueue和Dequeue操作
    /// </summary>
    public class QueueWithStacks
    {
        MyStack<int> _stack1 = new MyStack<int>();
        MyStack<int> _stack2 = new MyStack<int>();

        public void Enqueue(int value)
        {
            _stack1.Push(value);
        }

        public int Dequeue()
        {
            if (_stack2.Count < 1)
            {
                while (_stack1.Count > 0)
                {
                    _stack2.Push(_stack1.Pop());
                }
            }

            if (_stack2.Count > 0)
                return _stack2.Pop();

            throw new InvalidOperationException("Empty collection");
        }
    }

    public class MyQueue<T>
    {
        T[] q = new T[100];
        int len = 0;
        int start = 0;

        public void Enqueue(T data)
        {
            if (len >= 100) throw new InvalidOperationException("Full collection");

            q[(start + len) % 100] = data;
            len++;
        }

        public T Dequeue()
        {
            if(len<=0) 
                throw new InvalidOperationException("Empty collection");

            len --;
            T ret = q[start];
            start = (start + 1) % 100;

            return ret;
        }

        public int Count 
        { 
            get 
            { 
                return len; 
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = start; i < start+len; i++)
            {
                sb.Append(q[i]);
            }

            return sb.ToString();
        }
    }
}
