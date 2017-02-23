using Algorithm.Core.Entity;
using System;
using System.Collections;
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

            Console.WriteLine(input);

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
            PreorderTraversalRecursively(_tree);
            Console.WriteLine();
        }

        public void PrintBTreeByPostorder()
        {
            Console.Write("后序遍历二叉树：");
            PostorderTraversalRecursively(_tree);
            Console.WriteLine();
        }

        public void PrintBTreeByInorder()
        {
            Console.Write("中序遍历二叉树：");
            InorderTraversalRecursively(_tree);
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

        /// <summary>
        /// 获得所有从根节点到叶子节点的路径中，节点数值总和为指定值的路径
        /// </summary>
        /// <param name="sum"></param>
        public void PrintAllPathsWithSum(int sum)
        {
            Console.WriteLine("从根节点到叶子节点的路径上和为{0}的结果：", sum);
            AllPathWithSumInternal(_tree, "", 0, sum);
        }

        /// <summary>
        /// 递归查找
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        public void PrintCommonMinParentNode(int value1, int value2)
        {
            Console.WriteLine("节点{0}和节点{1}的公共最小父节点为：", value1, value2);
            var node = FindCommonMinParentNodeInternal(_tree, value1, value2);
            if (node != null)
                Console.WriteLine(node.Value);
            else
                Console.WriteLine("没有找到");
        }
        
        /// <summary>
        /// 使用数组存储两个节点的路径，然后比较数组
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        public void PrintCommonMinParentNode2(int value1, int value2)
        {
            Console.WriteLine("节点{0}和节点{1}的公共最小父节点为：", value1, value2);
            var node = FindCommonMinParentNodeInternal2(_tree, value1, value2);
            if (node != null)
                Console.WriteLine(node.Value);
            else
                Console.WriteLine("没有找到");
        }

        #region private methods
        /// <summary>
        /// 递归实现前序遍历
        /// </summary>
        /// <param name="tree"></param>
        private void PreorderTraversalRecursively(BTreeEntity tree)
        {
            if (tree == null) return;

            Console.Write(tree.Value);
            PreorderTraversalRecursively(tree.Left);
            PreorderTraversalRecursively(tree.Right);
        }

        /// <summary>
        /// 递归实现后序遍历
        /// </summary>
        /// <param name="tree"></param>
        private void PostorderTraversalRecursively(BTreeEntity tree)
        {
            if (tree == null) return;

            PostorderTraversalRecursively(tree.Left);
            PostorderTraversalRecursively(tree.Right);
            Console.Write(tree.Value);
        }

        /// <summary>
        /// 递归实现中序遍历
        /// </summary>
        /// <param name="tree"></param>
        private void InorderTraversalRecursively(BTreeEntity tree)
        {
            if (tree == null) return;

            InorderTraversalRecursively(tree.Left);
            Console.Write(tree.Value);
            InorderTraversalRecursively(tree.Right);
        }

        /// <summary>
        /// 非递归实现前序遍历
        /// <see cref="http://blog.csdn.net/zhangxiangdavaid/article/details/37115355"/>
        /// </summary>
        public string PreorderTraversalUnrecursively1()
        {
            string output = string.Empty;

            if (_tree == null) return output;

            Stack<BTreeEntity> stack = new Stack<BTreeEntity>();
            BTreeEntity p = _tree;

            while (p != null || stack.Count > 0)
            {
                if (p != null)
                {
                    //打印
                    output += "," + p.Value;
                    
                    //循环获取最左下角的节点，并压栈途经的节点
                    stack.Push(p);
                    p = p.Left;
                }
                else
                {
                    p = stack.Pop();
                    p = p.Right;
                }
            }
            
            /* 或者
            while (p != null || stack.Count > 0)
            {
                while (p != null)
                {
                    stack.Push(p);
                    output += "," + p.Value;
                    p = p.Left;
                }

                p = stack.Pop();
                p = p.Right;
            }
            */

            return output.Substring(1);
        }

        /// <summary>
        /// 非递归实现前序遍历
        /// 根据
        /// <see cref="http://blog.csdn.net/zhangxiangdavaid/article/details/37115355"/>
        /// </summary>
        public string PreorderTraversalUnrecursively2()
        {
            string output = string.Empty;

            if (_tree == null) return output;

            Stack<BTreeEntity> stack = new Stack<BTreeEntity>();
            BTreeEntity p = _tree;

            //先使树根入栈，这是循环退出的关键点
            stack.Push(_tree);

            //遇见右孩子，先入栈。 接着处理左孩子，如果不为空，移动p节点，否则，处理栈顶节点（一般为右孩子的节点，如果弹出栈的是根，则结束循环）
            while (stack.Count > 0)
            {
                //先打印
                output += "," + p.Value;

                if (p.Right != null) stack.Push(p.Right);
                if (p.Left != null) p = p.Left;
                else p = stack.Pop();
            }

            return output.Substring(1);
        }

        /// <summary>
        /// 非递归实现中序遍历
        /// <see cref="http://blog.csdn.net/zhangxiangdavaid/article/details/37115355"/>
        /// </summary>
        public string InorderTraversalIteratively1()
        {
            string output = string.Empty;

            if (_tree == null) return output;

            Stack<BTreeEntity> stack = new Stack<BTreeEntity>();
            BTreeEntity p = _tree;

            while (p != null || stack.Count > 0)
            {
                if (p != null)
                {
                    //循环获取最左下角的节点，并压栈途经的节点
                    stack.Push(p);
                    p = p.Left;
                }
                else
                {
                    p = stack.Pop();
                    
                    //打印
                    output += "," + p.Value; 
                    
                    p = p.Right;
                }
            }

            /* 或者
            while (p != null || stack.Count > 0)
            {
                while (p != null)
                {
                    stack.Push(p);
                    p = p.Left;
                }

                p = stack.Pop();
                output += "," + p.Value;
                p = p.Right;
            }
            */

            return output.Substring(1);
        }

        /// <summary>
        /// 非递归实现后序遍历
        /// 采用一个标志位
        /// <see cref="http://blog.csdn.net/zhangxiangdavaid/article/details/37115355"/>
        /// </summary>
        public string PostorderTraversalIteratively1()
        {
            string output = string.Empty;

            if (_tree == null) return output;

            Stack<BTreeEntity> stack = new Stack<BTreeEntity>();
            BTreeEntity p = _tree;
            BTreeEntity last = null; //指示上次打印的节点。此节点用于确认右孩子是否处理过

            //处理堆栈中的节点，因为均是左子节点，所以只考虑处理其右子节点
            while (p != null || stack.Count > 0)
            {
                //先循环找到最左下角的节点，并压栈途径的左节点
                if (p != null)
                {
                    stack.Push(p);
                    p = p.Left;
                }
                else
                {
                    p = stack.Pop();

                    //当右子节点存在且没有右孩子没有被访问过，则从处理右孩子；否则打印
                    if (p.Right != null && p.Right != last)
                    {
                        stack.Push(p);
                        p = p.Right;
                    }
                    else
                    {
                        output += "," + p.Value;
                        last = p;
                        p = null;
                    }
                }
            }

            return output.Substring(1);
        }

        /// <summary>
        /// 非递归实现后序遍历
        /// 逻辑和PostorderTraversalIteratively1，基本类似，区别是通过添加标识字段来确定是否有必要处理右孩子
        /// <see cref="http://blog.csdn.net/zhangxiangdavaid/article/details/37115355"/>
        /// </summary>
        public string PostorderTraversalIteratively2()
        {
            //借助IsRightProcessed标识。 若IsRightProcessed=false，则右孩子没有被访问过；否则则访问过
            string output = string.Empty;

            if (_tree == null) return output;

            Stack<TagBTreeEntity> stack = new Stack<TagBTreeEntity>();
            TagBTreeEntity tagBTree;
            BTreeEntity p = _tree;

            while (p != null || stack.Count > 0)
            {
                if (p != null)
                {
                    //当p不为null时,循环获取左子节点

                    tagBTree = new TagBTreeEntity();
                    tagBTree.Tree = p;
                    tagBTree.IsRightProcessed = false;
                    stack.Push(tagBTree);

                    p = p.Left;
                }
                else
                {
                    //当p为pull,说明需要读栈处理啦。

                    //出栈
                    tagBTree = stack.Pop();

                    if(tagBTree.Tree.Right != null && tagBTree.IsRightProcessed == false)
                    {
                        //如果该节点的右子节点不为空，且未被处理过（后序遍历的逻辑：肯定是先处理了右子树的根，然后接着处理当前节点）

                        //更新标记
                        tagBTree.IsRightProcessed = true;

                        //再次入栈
                        stack.Push(tagBTree);

                        //获取右孩子
                        p = tagBTree.Tree.Right;
                    }
                    else
                    {
                        //此时，左右孩子都已处理过了，或者是因为孩子为null，跳过了

                        output += "," + tagBTree.Tree.Value;
                        //置空，需要从栈中pop一个新的节点
                        p = null;
                    }
                }
            }

            return output.Substring(1);
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

        private void AllPathWithSumInternal(BTreeEntity tree, string path, int tempSum, int targetSum)
        {
            if (tree == null) return;

            tempSum += tree.Value;
            path = path + "," + tree.Value;

            if (tempSum > targetSum)
            {
                return;
            }
            else if (tempSum == targetSum)
            {
                Console.WriteLine(path.Substring(1));
                return;
            }
            else
            {
                AllPathWithSumInternal(tree.Left, path, tempSum, targetSum);
                AllPathWithSumInternal(tree.Right, path, tempSum, targetSum);
            }
        }

        /// <summary>
        /// 递归查找两个节点的位置。如果在均在单侧，则在单侧查找；否则，当前节点即为最小公共父节点
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        private BTreeEntity FindCommonMinParentNodeInternal(BTreeEntity tree, int value1, int value2)
        {
            if (tree == null)
                return null;

            if (_tree.Value == value1 || _tree.Value == value2)
                return tree;

            bool isInLeft1 = IsFoundChildNode(tree.Left, value1);
            bool isInLeft2 = IsFoundChildNode(tree.Left, value2);

            if (isInLeft1 && isInLeft2)
            {
                return FindCommonMinParentNodeInternal(tree.Left, value1, value2);
            }

            bool isInRight1 = IsFoundChildNode(tree.Right, value1);
            bool isInRight2 = IsFoundChildNode(tree.Right, value2);

            if (isInRight1 && isInRight2)
            {
                return FindCommonMinParentNodeInternal(tree.Right, value1, value2);
            }

            if (isInLeft1 && isInRight2 || isInLeft2 || isInRight1)
                return tree;

            return null;
        }

        /// <summary>
        /// 查找指定节点是否存在二叉树中
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsFoundChildNode(BTreeEntity tree, int value)
        {
            if (tree == null) return false;

            if (tree.Value == value)
                return true;

            return IsFoundChildNode(tree.Left, value) || IsFoundChildNode(tree.Right, value);
        }

        /// <summary>
        /// 使用数组存储两条节点路径，再顺序比较，找到最后一个相同的节点，即为最小公共父节点
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        private BTreeEntity FindCommonMinParentNodeInternal2(BTreeEntity tree, int value1, int value2)
        {
            List<BTreeEntity> list1 = new List<BTreeEntity>(), list2=new List<BTreeEntity>();
            GetNodePathInternal(tree, value1, list1);
            GetNodePathInternal(tree, value2, list2);

            var e1 = list1.GetEnumerator();
            var e2 = list2.GetEnumerator();

            BTreeEntity t1,t2;
            BTreeEntity result = null;
            while (e1.MoveNext() && e2.MoveNext())
            {
                t1 = e1.Current;
                t2 = e2.Current;
                if (t1 != t2) break;

                result = t1;
            }

            return result;
        }

        /// <summary>
        /// 获取二叉树中指定节点的节点路径（存放数组中）
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="value"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private bool GetNodePathInternal(BTreeEntity tree, int value, List<BTreeEntity> list)
        {
            if (tree == null) return false;

            if (tree.Value == value) return true;

            list.Add(tree);

            bool found = GetNodePathInternal(tree.Left, value, list) || GetNodePathInternal(tree.Right, value, list);
            if (!found) list.Remove(tree);

            return found;
        }
        #endregion
    }
}