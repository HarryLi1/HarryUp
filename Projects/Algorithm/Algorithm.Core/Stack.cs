using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Core
{
    /// <summary>
    /// 利用两个队列实现栈的Push和Pop操作
    /// </summary>
    public class StackWithQueues
    {
        private Queue<int> _queue1 = new Queue<int>();
        private Queue<int> _queue2 = new Queue<int>();

        public void Push(int value)
        {
            if (_queue1.Count > 0)
                _queue1.Enqueue(value);
            else
                _queue2.Enqueue(value);
        }

        public int Pop()
        {
            if (_queue1.Count > 0)
            {
                while (_queue1.Count > 1)
                {
                    _queue2.Enqueue(_queue1.Dequeue());
                }

                if(_queue1.Count==1)
                    return _queue1.Dequeue();
            }
            else
            {
                while (_queue2.Count > 1)
                {
                    _queue1.Enqueue(_queue2.Dequeue());
                }

                if (_queue2.Count == 1)
                    return _queue2.Dequeue();

            }

            throw new InvalidOperationException("Empty collection");
        }
    }

    /// <summary>
    /// 题目：定义栈的数据结构，要求添加一个min函数，能够得到栈的最小元素。要求函数min、push以及pop的时间复杂度都是O(1)。
    /// 解法一，使用额外的栈保存最小值
    /// <see cref="http://zhedahht.blog.163.com/blog/static/25411174200712895228171/"/>
    /// </summary>
    public class StackWithMin1
    {
        Stack<int> data_stack = new Stack<int>();
        Stack<int> min_stack = new Stack<int>();

        public void Push(int data)
        {
            //if (min_stack.Count == 0)
            //{
            //    min_stack.Push(data);
            //}
            //else
            //{
            //    if(data<min_stack.First())
            //    {
            //        min_stack.Push(data);
            //    }
            //    else
            //    {
            //        min_stack.Push(min_stack.First());
            //    }
            //}
            min_stack.Push((min_stack.Count == 0 || data < min_stack.First()) ? data : min_stack.First());

            data_stack.Push(data);
        }

        public int Pop()
        {
            if (data_stack.Count > 0)
            {
                min_stack.Pop();
                return data_stack.Pop();
            }

            throw new InvalidOperationException("Empty collection");
        }

        public int Min()
        {
            if (min_stack.Count > 0)
            {
                return min_stack.First();
            }

            throw new InvalidOperationException("Empty collection");
        }
    }

    /// <summary>
    /// 题目：定义栈的数据结构，要求添加一个min函数，能够得到栈的最小元素。要求函数min、push以及pop的时间复杂度都是O(1)。
    /// 解法二，只额外使用一个空间，保存min的值
    /// <see cref="http://blog.csdn.net/anchor89/article/details/6055412"/>
    /// </summary>
    public class StackWithMin2
    {
        Stack<int> data_stack = new Stack<int>();
        int min;

        public void Push(int data)
        {
            if(data_stack.Count == 0)
            {
                data_stack.Push(data);
                min = data;
            }
            else
            {
                data_stack.Push(data - min);
                min = data < min ? data : min;
            }
        }

        public int Pop()
        {
            if (data_stack.Count > 0)
            {
                int top = data_stack.Pop();
                int ret;
                if (top >= 0)
                {
                    ret = min + top;
                }
                else
                {
                    ret = min;
                    min = min - top;
                }

                return ret;
            }

            throw new InvalidOperationException("Empty collection");
        }

        public int Min()
        {
            if(data_stack.Count > 0)
            {
                return min;
            }

            throw new InvalidOperationException("Empty collection");
        }
    }

    /// <summary>
    /// 题目：定义栈的数据结构，要求添加一个min函数，能够得到栈的最小元素。要求函数min、push以及pop的时间复杂度都是O(1)。
    /// 解法三，和解法二唯一的区别在于保存栈内数值的算法
    /// <see cref="http://blog.csdn.net/anchor89/article/details/6055412"/>
    /// </summary>
    public class StackWithMin3
    {
        Stack<int> data_stack = new Stack<int>();
        int min;

        public void Push(int data)
        {
            if (data_stack.Count == 0)
            {
                data_stack.Push(data);
                min = data;
            }
            else
            {
                data_stack.Push(data < min ? 2 * data - min : data);
                min = data < min ? data : min;
            }
        }

        public int Pop()
        {
            if (data_stack.Count > 0)
            {
                int top = data_stack.Pop();
                int ret;
                if (top < min)
                {
                    ret = min;
                    min = 2 * min - top;
                }
                else
                {
                    ret = top;
                }

                return ret;
            }

            throw new InvalidOperationException("Empty collection");
        }

        public int Min()
        {
            if (data_stack.Count > 0)
            {
                return min;
            }

            throw new InvalidOperationException("Empty collection");
        }
    }

}
