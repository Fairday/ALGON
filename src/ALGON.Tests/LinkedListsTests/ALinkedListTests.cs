using ALGON.DataStructures.LinkedLists;
using NUnit.Framework;

namespace ALGON.Tests.LinkedListsTests
{
    [TestFixture]
    public class ALinkedListTests
    {
        [Test]
        public void Add()
        {
            var list = new ALinkedList<int>();
            list.Add(5);
            var count = list.Count;
            if (count != 1)
                Assert.Fail();
            foreach (var item in list)
            {
                if (item != 5)
                    Assert.Fail();
            }
            Assert.Pass();
        }

        [Test]
        public void Count()
        {
            var list = new ALinkedList<int>();
            list.Add(5);
            list.Add(10);
            var count = 0;
            foreach (var item in list)
            {
                count++;
            }
            if (count == 2 && list.Count == 2)
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void Remove()
        {
            var list = new ALinkedList<int>();
            list.Add(5);
            list.Remove(5);
            var count = list.Count;
            if (count != 0)
                Assert.Fail();
            list.Add(15);
            list.Add(13);
            list.Remove(15);
            if (list.Head.Value != 13)
                Assert.Fail();
            Assert.Pass();
        }

        [Test]
        public void Clear()
        {
            var list = new ALinkedList<int>();
            list.Add(5);
            list.Clear();
            var count = list.Count;
            if (count != 0)
                Assert.Fail();
            Assert.Pass();
        }

        [Test]
        public void CopyTo()
        {
            var list = new ALinkedList<int>();
            list.Add(5);
            list.Add(10);
            list.Add(15);
            var array = new int[3];
            list.CopyTo(array, 0);
            if (array.Length != 3)
                Assert.Fail();
            if (array[0] == 5 && array[1] == 10 && array[2] == 15)
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void Conatins()
        {
            var list = new ALinkedList<int>();
            list.Add(5);
            if (!list.Contains(5))
                Assert.Fail();
            Assert.Pass();
        }

        [Test]
        public void AddFirst()
        {
            var list = new ALinkedList<int>();
            list.AddFirst(15);
            list.AddFirst(33);
            if (list.Head.Value != 33)
                Assert.Fail();
            else
                Assert.Pass();
        }

        [Test]
        public void AddLast()
        {
            var list = new ALinkedList<int>();
            list.AddFirst(15);
            list.AddLast(33);
            if (list.Tail.Value != 33)
                Assert.Fail();
            else
                Assert.Pass();
        }

        [Test]
        public void RemoveFirst()
        {
            var list = new ALinkedList<int>();
            list.Add(15);
            list.Add(88);
            list.RemoveFirst();
            if (list.Head.Value != 88)
                Assert.Fail();
            else
                Assert.Pass();
        }

        [Test]
        public void RemoveLast()
        {
            var list = new ALinkedList<int>();
            list.Add(15);
            list.Add(88);
            list.RemoveLast();
            if (list.Tail.Value != 15)
                Assert.Fail();
            else
                Assert.Pass();
        }
    }
}
