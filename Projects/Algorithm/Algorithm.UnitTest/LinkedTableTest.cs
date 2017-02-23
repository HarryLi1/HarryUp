using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithm.Core;

namespace Algorithm.UnitTest
{
    [TestClass]
    public class LinkedTableTest
    {
        [TestMethod]
        public void Initial()
        {
            int[] input1 = new int[] { 1, 2, 3, 4, 5 };
            LinkedTableBy2Array lta1 = new LinkedTableBy2Array();
            lta1.Initial(input1);
            Assert.AreEqual("12345", lta1.ToString());

            int[] input2 = new int[] { };
            LinkedTableBy2Array lta2 = new LinkedTableBy2Array();
            lta2.Initial(input2);
            Assert.AreEqual("", lta2.ToString());

            int[] input3 = new int[] { 1 };
            LinkedTableBy2Array lta3 = new LinkedTableBy2Array();
            lta3.Initial(input3);
            Assert.AreEqual("1", lta3.ToString());
        }

        [TestMethod]
        public void Delete()
        {
            int[] input = new int[] { 1, 2, 3, 4, 5 };
            LinkedTableBy2Array lta = new LinkedTableBy2Array();
            lta.Initial(input);
            lta.Delete(3);
            Assert.AreEqual("1245", lta.ToString());

            lta.Delete(1);
            Assert.AreEqual("245", lta.ToString());

            lta.Delete(5);
            Assert.AreEqual("24", lta.ToString());

            lta.Delete(9);
            Assert.AreEqual("24", lta.ToString());

            lta.Delete(2);
            lta.Delete(4);
            Assert.AreEqual("", lta.ToString());
        }

        [TestMethod]
        public void Insert()
        {
            int[] input = new int[] { };
            LinkedTableBy2Array lta = new LinkedTableBy2Array();
            lta.Initial(input);
            lta.Insert(1, 3);
            Assert.AreEqual("3", lta.ToString());
            lta.Insert(1, 1);
            Assert.AreEqual("13", lta.ToString());
            lta.Insert(2, 2);
            Assert.AreEqual("123", lta.ToString());
            lta.Insert(4, 4);
            Assert.AreEqual("1234", lta.ToString());
        }

        [TestMethod]
        public void Update()
        {
            int[] input = new int[] { 1, 2, 3, 4, 5 };
            LinkedTableBy2Array lta = new LinkedTableBy2Array();
            lta.Initial(input);
            lta.Update(1, 7);
            Assert.AreEqual("72345", lta.ToString());
            lta.Update(4, 8);
            Assert.AreEqual("72385", lta.ToString());
            lta.Update(5, 9);
            Assert.AreEqual("72389", lta.ToString());
            lta.Update(10, 6);
            Assert.AreEqual("72389", lta.ToString());
        }

        [TestMethod]
        public void Search()
        {
            int[] input = new int[] { 1, 2, 3, 4, 5 };
            LinkedTableBy2Array lta = new LinkedTableBy2Array();
            lta.Initial(input);
            
            Assert.AreEqual(lta.Search(0), 0);
            Assert.AreEqual(lta.Search(1), 1);
            Assert.AreEqual(lta.Search(3), 3);
            Assert.AreEqual(lta.Search(5), 5);
        }
    }
}
