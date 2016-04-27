using Algorithm.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Test.Queue
{
    class Program
    {
        static void Main(string[] args)
        {
            QueueWithStacks stack = new QueueWithStacks();
            int data;
            stack.Enqueue(1);
            stack.Enqueue(2);
            stack.Enqueue(3);
            data = stack.Dequeue();
            stack.Enqueue(4);
            stack.Enqueue(5);
            data = stack.Dequeue();
            stack.Enqueue(6);
            data = stack.Dequeue();
            data = stack.Dequeue();
            stack.Enqueue(7);
            stack.Enqueue(8);
            stack.Enqueue(9);
            data = stack.Dequeue();

            Console.Read();
        }
    }
}
