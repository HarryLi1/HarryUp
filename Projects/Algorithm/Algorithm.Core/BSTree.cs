using Algorithm.Core.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Core
{
    /// <summary>
    /// Binary Search Tree 二叉查找树
    /// </summary>
    public class BSTree
    {
        private BTreeEntity _tree;

        private BTreeEntity _head;
        private BTreeEntity _index;

        public BTreeEntity Root
        {
            get
            {
                return _tree;
            }
        }

        public BTreeEntity Head
        {
            get
            {
                return _head;
            }
        }

        public BSTree()
        {
            _tree = null;
        }

        public BSTree(int[] values)
        {
            foreach (var value in values)
            {
                AddBSTree(value);
            }
        }

        public void AddBSTree(int value)
        {
            AddBSTree(ref _tree, value);
        }

        private void AddBSTree(ref BTreeEntity node, int value)
        {
            if (node == null)
            {
                BTreeEntity newNode = new BTreeEntity();
                newNode.Value = value;
                newNode.Left = null;
                newNode.Right = null;

                node = newNode;
            }
            else if(node.Value>value)
            {
                AddBSTree(ref node.Left, value);
            }
            else if(node.Value<value)
            {
                AddBSTree(ref node.Right, value);
            }
            else
            {
                //Node repeated
                return;
            }
        }

        public void ConvertToDoublyLinkedList(BTreeEntity tree)
        {
            if (tree == null) return;

            ConvertToDoublyLinkedList(tree.Left);
            ChangeReference(tree);
            ConvertToDoublyLinkedList(tree.Right);
        }

        public void ChangeReference(BTreeEntity node)
        {
            if(_head==null)
            {
                _head = node;
            }
            else
            {
                _index.Right = node;
            }

            node.Left = _index;
            _index = node;
        }

        public void MirrorRecursively()
        {
            MirrorRecursively(_tree);
        }

        private void MirrorRecursively(BTreeEntity node)
        {
            if (node == null) return;
            BTreeEntity temp = node.Left;
            node.Left = node.Right;
            node.Right = temp;

            MirrorRecursively(node.Left);
            MirrorRecursively(node.Right);
        }

        public void MirrorIteratively()
        {
            Stack<BTreeEntity> stack = new Stack<BTreeEntity>();
            stack.Push(Root);

            while (stack.Count > 0)
            {
                BTreeEntity node = stack.Pop();
                
                BTreeEntity temp = node.Left;
                node.Left = node.Right;
                node.Right = temp;

                if (node.Left != null) stack.Push(node.Left);
                if (node.Right != null) stack.Push(node.Right);
            }
        }

        public void PrintDoublyLinkedList()
        {
            Console.Write("打印双向链表的结果：");

            string ouput = string.Empty;
            BTreeEntity node = _head;
            while(node!=null)
            {
                ouput += "," + node.Value;
                node = node.Right;
            }

            Console.WriteLine(ouput.Substring(1));
        }

        public void PrintBinarySearchTree()
        {
            Console.Write("打印二叉查找树的结果：");

            string ouput = string.Empty;
            Stack<BTreeEntity> stack = new Stack<BTreeEntity>();
            Stack<bool> stack2 = new Stack<bool>();

            stack.Push(Root);
            stack2.Push(true);

            while (stack.Count > 0)
            {
                BTreeEntity node = stack.Pop();
                bool isRight = stack2.Pop();

                if (isRight)
                {
                    //更新父节点的配置为false
                    stack.Push(node);
                    stack2.Push(false);

                    if (node.Left != null)
                    {
                        stack.Push(node.Left);
                        stack2.Push(true);
                    }
                  
                    continue;
                }

                ouput += "," + node.Value;

                if (node.Right != null)
                {
                    stack.Push(node.Right);
                    stack2.Push(true);
                }
            }

            Console.WriteLine(ouput.Substring(1));
        }
    }
}
