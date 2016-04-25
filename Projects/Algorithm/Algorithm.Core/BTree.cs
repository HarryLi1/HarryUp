using Algorithm.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Core
{
    public class BTree
    {
        private BTreeEntity _tree;

        public BTreeEntity Entity
        {
            get
            {
                if (_tree == null)
                {
                    InitBTree();
                }

                return _tree;
            }
        }

        public void InitBTree(string input = null)
        {
            string sample = "124##57###3#6##";
            if (input == null) input = sample;

            _tree = BTree.BuildBTree(input);
        }

        public static BTreeEntity BuildBTree(string input)
        {
            return BuildBTreeInternal(ref input);
        }

        /// <summary>
        /// 递归创建创建二叉树
        /// </summary>
        /// <param name="input">"3#45###"</param>
        private static BTreeEntity BuildBTreeInternal(ref string input)
        {
            //Console.WriteLine("BuildBTree:" + input);
            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }

            char c = input[0];
            if (c == '#')
            {
                return null;
            }
            else if (c >= 0x30 && c <= 0x39)
            {
                BTreeEntity newNode = new BTreeEntity();
                newNode.Value = int.Parse(c.ToString());

                input = input.Substring(1);
                newNode.Left = BuildBTreeInternal(ref input);

                input = input.Substring(1);
                newNode.Right = BuildBTreeInternal(ref input);

                return newNode;
            }
            else
            {
                // error
                return null;
            }
        }

        public void PrintBTreeByPreorder()
        {
            Console.Write("前序遍历二叉树：");
            PreorderTraversal(_tree);
            Console.WriteLine();
        }

        public void PrintBTreeByPostorder()
        {
            Console.Write("后序遍历二叉树：");
            PostorderTraversal(_tree);
            Console.WriteLine();
        }

        public void PrintBTreeByInorder()
        {
            Console.Write("中序遍历二叉树：");
            InorderTraversal(_tree);
            Console.WriteLine();
        }

        /// <summary>
        /// 打印二叉树的深度
        /// </summary>
        public void PrintDepth()
        {
            Console.WriteLine("二叉树的深度：{0}", GetDepth(_tree));
        }

        /// <summary>
        /// 打印是否为平衡二叉树
        /// </summary>
        public void PrintIsBalanced()
        {
            int depth;
            Console.WriteLine("二叉树是否为平衡树：{0}", IsBalanced(_tree, out depth));
        }

        /// <summary>
        /// 检查subTree是否为当前二叉树的子结构，即能局部吻合。如：12#3##4##中的子结构2#3##
        /// </summary>
        /// <param name="subTree"></param>
        /// <returns></returns>
        public bool CheckMatchSubTree(BTreeEntity subTree)
        {
            if (_tree == null && subTree != null)
                return false;
            else if (subTree == null)
                return true;
            else
            {
                return CheckMatchSubTreeRoot(_tree, subTree);
            }
        }

        #region private methods
        /// <summary>
        /// 递归实现前序遍历
        /// </summary>
        /// <param name="tree"></param>
        private void PreorderTraversal(BTreeEntity tree)
        {
            if (tree == null) return;

            Console.Write(tree.Value);
            PreorderTraversal(tree.Left);
            PreorderTraversal(tree.Right);
        }

        /// <summary>
        /// 递归实现后序遍历
        /// </summary>
        /// <param name="tree"></param>
        private void PostorderTraversal(BTreeEntity tree)
        {
            if (tree == null) return;

            PostorderTraversal(tree.Left);
            PostorderTraversal(tree.Right);
            Console.Write(tree.Value);
        }

        /// <summary>
        /// 递归实现中序遍历
        /// </summary>
        /// <param name="tree"></param>
        private void InorderTraversal(BTreeEntity tree)
        {
            if (tree == null) return;

            InorderTraversal(tree.Left);
            Console.Write(tree.Value);
            InorderTraversal(tree.Right);
        }

        /// <summary>
        /// 递归方式获取二叉树深度
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        private int GetDepth(BTreeEntity tree)
        {
            if (tree == null) return 0;

            int leftDepth = GetDepth(tree.Left);
            int rightDepth = GetDepth(tree.Right);

            return Math.Max(leftDepth, rightDepth) + 1;
        }

        /// <summary>
        /// 递归方式判断二叉树是否为平衡树
        /// 递归算法：1: 左右子树均为平衡树; 2: 左右子树的深度差小于1
        /// 
        /// 平衡树的条件是：二叉树中任意结点的左右子树的深度相差不超过1
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        private bool IsBalanced(BTreeEntity tree, out int depth)
        {
            depth = 0;

            if (tree == null)
            {
                return true;
            }

            int leftDepth, rightDepth;
            if (IsBalanced(tree.Left, out leftDepth) && IsBalanced(tree.Right, out rightDepth))
            {
                if (Math.Abs(leftDepth - rightDepth) <= 1)
                {
                    depth = 1 + Math.Max(leftDepth, rightDepth);
                    return true;
                }
            }

            return false;
        }

        private bool CheckMatchSubTreeRoot(BTreeEntity tree1, BTreeEntity tree2)
        {
            if (tree1 == null) return false;

            bool result = false;
            //result = tree1.Value == tree2.Value && CheckMatchSubTreeWhole(tree1, tree2);
            //result = result || !result && CheckMatchSubTreeRoot(tree1.Left, tree2);
            //result = result || !result && CheckMatchSubTreeRoot(tree1.Right, tree2);
            if (tree1.Value == tree2.Value) result = CheckMatchSubTreeWhole(tree1, tree2);
            if (!result) result = CheckMatchSubTreeRoot(tree1.Left, tree2);
            if (!result) result = CheckMatchSubTreeRoot(tree1.Right, tree2);

            return result;
        }

        private bool CheckMatchSubTreeWhole(BTreeEntity tree1, BTreeEntity tree2)
        {
            if (tree2 == null) return true;
            if (tree1 == null) return false;

            if (tree1.Value != tree2.Value) return false;

            return CheckMatchSubTreeWhole(tree1.Left, tree2.Left) && CheckMatchSubTreeWhole(tree1.Right, tree2.Right);
        }
        #endregion
    }
}
