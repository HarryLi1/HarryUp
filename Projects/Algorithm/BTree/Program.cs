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
            tree.InitBTree("2247##8##5##3#6##");
            tree.PrintBTreeByPreorder();
            tree.PrintBTreeByInorder();
            tree.PrintBTreeByPostorder();
            tree.PrintDepth();
            tree.PrintIsBalanced();

            string test="247###5##";
            BTreeEntity tree2 = BTree.BuildBTree(test);
            bool result = tree.CheckMatchSubTree(tree2);

            Console.WriteLine("BTree1:{0}", tree.Entity.ToString());
            Console.WriteLine("BTree2:{0}", tree2.ToString());
            Console.WriteLine("Match result:{0}", result);

            Console.Read();
        }
    }
}
