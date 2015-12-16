using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using HexToBinLib;

namespace HexToBinTest
{
	[TestClass()]
	public class Test
	{
		[TestMethod()]
		public void TestCase01()
		{
            MemoryStream output = new MemoryStream(2);
            int count = HexToBin.Convert(new StringReader("0xDE 0xAD"), output);
            Assert.AreNotEqual(-1, count);
            CollectionAssert.AreEqual(new byte[] {0xDE, 0XAD}, output.GetBuffer());
		}

        [TestMethod()]
        public void TestCase02()
        {
            MemoryStream output = new MemoryStream(2);
            int count = HexToBin.Convert(new StringReader("0xDE0xAD"), output);
            Assert.AreNotEqual(-1, count);
            CollectionAssert.AreEqual(new byte[] { 0xDE, 0XAD }, output.GetBuffer());
        }

        [TestMethod()]
        public void TestCase03()
        {
            MemoryStream output = new MemoryStream(2);
            int count = HexToBin.Convert(new StringReader("0xD E0xAD"), output);
            Assert.AreEqual(2, count);
            CollectionAssert.AreEqual(new byte[] { 0xDE, 0xAD }, output.GetBuffer());
        }

        [TestMethod()]
        public void TestCase04()
        {
            MemoryStream output = new MemoryStream(3);
            int count = HexToBin.Convert(new StringReader("ab 0xCD EF"), output);
            Assert.AreEqual(3, count);
            CollectionAssert.AreEqual(new byte[] { 0xAB, 0XCD, 0xEF }, output.GetBuffer());
        }

        [TestMethod()]
        public void TestCase05()
        {
            MemoryStream output = new MemoryStream(2);
            int count = HexToBin.Convert(new StringReader("0xDE\r\n 0xAD"), output);
            Assert.AreNotEqual(-1, count);
            CollectionAssert.AreEqual(new byte[] { 0xDE, 0XAD }, output.GetBuffer());
        }

        [TestMethod()]
        public void TestCase06()
        {
            MemoryStream output = new MemoryStream(2);
            int count = HexToBin.Convert(new StringReader("0xDE\r\n 0xAD \r\n"), output);
            Assert.AreNotEqual(-1, count);
            CollectionAssert.AreEqual(new byte[] { 0xDE, 0XAD }, output.GetBuffer());
        }

        [TestMethod()]
        public void TestCase07()
        {
            MemoryStream output = new MemoryStream(2);
            int count = HexToBin.Convert(new StringReader("0xDE\n 0xAD"), output);
            Assert.AreNotEqual(-1, count);
            CollectionAssert.AreEqual(new byte[] { 0xDE, 0XAD }, output.GetBuffer());
        }

        [TestMethod()]
        public void TestCase08()
        {
            MemoryStream output = new MemoryStream(2);
            int count = HexToBin.Convert(new StringReader("0xDE\r 0xAD"), output);
            Assert.AreNotEqual(-1, count);
            CollectionAssert.AreEqual(new byte[] { 0xDE, 0XAD }, output.GetBuffer());
        }

        [TestMethod()]
        public void TestCase09()
        {
            MemoryStream output = new MemoryStream(2);
            int count = HexToBin.Convert(new StringReader("0xD\r\nE 0xAD"), output);
            Assert.AreEqual(2, count);
            CollectionAssert.AreEqual(new byte[] { 0xDE, 0XAD }, output.GetBuffer());
        }

        [TestMethod()]
        public void TestCase10()
        {
            MemoryStream output = new MemoryStream(2);
            int count = HexToBin.Convert(new StringReader("0x00DE 0xAD"), output);
            Assert.AreNotEqual(-1, count);
            CollectionAssert.AreEqual(new byte[] { 0xDE, 0XAD }, output.GetBuffer());
        }

        [TestMethod()]
        public void TestCase11()
        {
            MemoryStream output = new MemoryStream(2);
            int count = HexToBin.Convert(new StringReader("DE AD"), output);
            Assert.AreNotEqual(-1, count);
            CollectionAssert.AreEqual(new byte[] { 0xDE, 0XAD }, output.GetBuffer());
        }

        [TestMethod()]
        public void TestCase12()
        {
            MemoryStream output = new MemoryStream(2);
            int count = HexToBin.Convert(new StringReader("DEA D"), output);
            Assert.AreEqual(2, count);
            CollectionAssert.AreEqual(new byte[] { 0xDE, 0xAD }, output.GetBuffer());
        }

        [TestMethod()]
        public void TestCase13()
        {
            MemoryStream output = new MemoryStream(2);
            int count = HexToBin.Convert(new StringReader("DEA\r\nD"), output);
            Assert.AreEqual(2, count);
            CollectionAssert.AreEqual(new byte[] { 0xDE, 0xAD}, output.GetBuffer());
        }

        [TestMethod()]
        public void TestCase14()
        {
            MemoryStream output = new MemoryStream(2);
            int count = HexToBin.Convert(new StringReader("0x01DE 0xAD"), output);
            Assert.AreEqual(-1, count);
        }

        [TestMethod()]
        public void TestCase15()
        {
            MemoryStream output = new MemoryStream(2);
            int count = HexToBin.Convert(new StringReader("0xDExAD"), output);
            Assert.AreEqual(-1, count);
        }

        [TestMethod()]
        public void TestCase16()
        {
            MemoryStream output = new MemoryStream(2);
            int count = HexToBin.Convert(new StringReader("DEAD"), output);
            Assert.AreNotEqual(-1, count);
            CollectionAssert.AreEqual(new byte[] { 0xDE, 0XAD }, output.GetBuffer());
        }

        [TestMethod()]
        public void TestCase17()
        {
            MemoryStream output = new MemoryStream(2);
            HexToBin.IgnoredChars = "{},";
            int count = HexToBin.Convert(new StringReader("{0xDE,\r\n 0xAD}"), output);
            Assert.AreNotEqual(-1, count);
            CollectionAssert.AreEqual(new byte[] { 0xDE, 0XAD }, output.GetBuffer());
        }
    }
}
