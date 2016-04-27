using Algorithm.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Test.Stack
{
    public class Program
    {
        static void Main(string[] args)
        {
            #region StackWithQueues
            //StackWithQueues stack = new StackWithQueues();
            //int data;
            //stack.Push(1);
            //stack.Push(2);
            //stack.Push(3);
            //data = stack.Pop();
            //stack.Push(4);
            //stack.Push(5);
            //data = stack.Pop();
            //stack.Push(6);
            //data = stack.Pop();
            //data = stack.Pop();
            //stack.Push(7);
            //stack.Push(8);
            //stack.Push(9);
            //data = stack.Pop();
            #endregion StackWithQueues

            #region StackWithMin1
            //StackWithMin1 stackWithMin1 = new StackWithMin1();
            //int data2, min;
            //stackWithMin1.Push(4);
            //stackWithMin1.Push(3);
            //stackWithMin1.Push(7);
            //stackWithMin1.Push(2);
            //stackWithMin1.Push(7);
            //data2 = stackWithMin1.Pop();
            //min = stackWithMin1.Min();

            //stackWithMin1.Push(2);
            //stackWithMin1.Push(3);
            //data2 = stackWithMin1.Pop();
            //min = stackWithMin1.Min();
            //stackWithMin1.Push(5);
            //stackWithMin1.Push(1);
            //min = stackWithMin1.Min();
            
            #endregion StackWithMin1

            #region StackWithMin2
            //StackWithMin2 stackWithMin2 = new StackWithMin2();
            //int data3, min1;
            //bool b;
            //stackWithMin2.Push(4);
            //stackWithMin2.Push(3);
            //stackWithMin2.Push(7);
            //stackWithMin2.Push(2);
            //stackWithMin2.Push(7);
            //data3 = stackWithMin2.Pop();
            //b = data3 == 7;
            //min1 = stackWithMin2.Min();
            //b = min1 == 2;

            //stackWithMin2.Push(2);
            //stackWithMin2.Push(3);
            //data3 = stackWithMin2.Pop();
            //b = data3 == 3;
            //min1 = stackWithMin2.Min();
            //b = min1 == 2;

            //stackWithMin2.Push(5);
            //stackWithMin2.Push(1);
            //min1 = stackWithMin2.Min();
            //b = min1 == 1;

            #endregion StackWithMin2

            #region StackWithMin3
            StackWithMin3 stackWithMin3 = new StackWithMin3();
            int data4, min2;
            bool b1;
            stackWithMin3.Push(4);
            stackWithMin3.Push(3);
            stackWithMin3.Push(7);
            stackWithMin3.Push(2);
            stackWithMin3.Push(1);
            data4 = stackWithMin3.Pop();
            b1 = data4 == 1;
            min2 = stackWithMin3.Min();
            b1 = min2 == 2;

            stackWithMin3.Push(6);
            stackWithMin3.Push(3);
            data4 = stackWithMin3.Pop();
            b1 = data4 == 3;
            min2 = stackWithMin3.Min();
            b1 = min2 == 2;

            stackWithMin3.Push(5);
            stackWithMin3.Push(1);
            min2 = stackWithMin3.Min();
            b1 = min2 == 1;

            #endregion StackWithMin2

            Console.Read();
        }
    }
}
