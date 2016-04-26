using Algorithm.Core;
using Algorithm.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTreeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            BTree tree = new BTree();
            tree.InitBTree("2347##8##5##3#6##");
            tree.PrintBTreeByPreorder();
            tree.PrintBTreeByInorder();
            tree.PrintBTreeByPostorder();
            //tree.PrintDepth();
            //tree.PrintIsBalanced();
            //tree.PrintAllPathsWithSum(110);
            //tree.PrintCommonMinParentNode(8, 5);
            //tree.PrintCommonMinParentNode2(8, 5);
            string pre1 = tree.PreorderTraversalUnrecursively1();
            string pre2 = tree.PreorderTraversalUnrecursively2();

            string in1 = tree.InorderTraversalIteratively1();

            string post1 = tree.PostorderTraversalIteratively1();
            string post2 = tree.PostorderTraversalIteratively2();

            //string test="247###5##";
            //BTreeEntity tree2 = BTree.BuildBTree(test);
            //bool result = tree.CheckMatchSubTree(tree2);

            //Console.WriteLine("BTree1:{0}", tree.Entity.ToString());
            //Console.WriteLine("BTree2:{0}", tree2.ToString());
            //Console.WriteLine("Match result:{0}", result);

            //int[] data = new int[]{ 10,6,14,4,8,12,16 };
            //BSTree bst = new BSTree(data);
            //bst.PrintBinarySearchTree();
            ////bst.ConvertToDoublyLinkedList(bst.Root);
            ////bst.PrintDoublyLinkedList();
            //bst.MirrorRecursively();
            //bst.PrintBinarySearchTree();
            //bst.MirrorIteratively();
            //bst.PrintBinarySearchTree();


            Console.Read();
        }
    }
}
