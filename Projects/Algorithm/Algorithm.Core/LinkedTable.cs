using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Core
{
    /// <summary>
    /// 链表
    /// </summary>
    public class LinkedTable
    {
    }

    /// <summary>
    /// 链表（两个数组实现）
    /// </summary>
    public class LinkedTableBy2Array
    {
        int[] dataArray = new int[101];
        int[] nextArray = new int[101];
        int len = 0, end=0;

        public void Initial(int[] input)
        {
            len = input.Length;
            end = 0;

            for (int i = 0; i < input.Length; i++)
            {
                dataArray[++end] = input[i];
                nextArray[end] = i + 2;
            }

            if (input.Length > 0)
            {
                nextArray[end] = 0;
                nextArray[0] = 1;
            }
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="position">位置</param>
        /// <param name="data">数据</param>
        public void Insert(int position, int data)
        {
            if (position < 1) return;

            if (position == 1)//插在头部
            {
                dataArray[++end] = data;
                nextArray[end] = nextArray[0];
                nextArray[0] = end;

                return;
            }

            int next = nextArray[0];
            int index = 0, last = 0;
            while (next != 0)
            {
                if (++index == position - 1) break;

                last = next;
                next = nextArray[next];
            }

            if (next != 0)//插在中部
            {
                dataArray[++end] = data;
                nextArray[end] = nextArray[next];

                nextArray[next] = end;
            }
            else//插在尾部
            {
                dataArray[++end] = data;
                nextArray[end] = 0;

                nextArray[last] = end;
            }
           
        }

        /// <summary>
        /// 查找数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>若找到，返回下标；否则返回0</returns>
        public int Search(int data)
        {
            int next = nextArray[0];
            while (next != 0)
            {
                if (dataArray[next] == data) break;
                next = nextArray[next];
            }

            return next;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="data">数据</param>
        public void Delete(int data)
        {
            int next = nextArray[0], p = 0;
            while (next != 0)
            {
                if (dataArray[next] == data) break;
                p = next;
                next = nextArray[next];
            }

            //当不为空，且找到待删除数据
            if (next != 0)
            {
                nextArray[p] = nextArray[next];
                len--;
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="position">位置</param>
        /// <param name="newData">新数据</param>
        public void Update(int position, int newData)
        {
            if (position < 1 || position > len) return;

            int index = 0;
            int next = nextArray[0];
            while (next != 0)
            {
                if(++index == position)
                {
                    dataArray[next] = newData;
                    break;
                }

                next = nextArray[next];
            }
        }

        /// <summary>
        /// 数据长度
        /// </summary>
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
            int next = nextArray[0];

            while (next != 0)
            {
                sb.AppendFormat("{0}", dataArray[next]);
                next = nextArray[next];
            }

            return sb.ToString();
        }
    }
}
