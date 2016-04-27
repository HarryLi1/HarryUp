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
        Stack<int> _stack1 = new Stack<int>();
        Stack<int> _stack2 = new Stack<int>();

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
}
