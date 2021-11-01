using Lru.Logic;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Lru.Tests
{
    [TestFixture]
    public class LruTests
    {
        //CONSTRUCTOR TESTS
        [Test]
        public void ConstructorCreatesBlankList()
        {
            LRU storage = new LRU();
            Assert.That(storage.Recent, Is.Not.Null);
            Assert.That(storage.Recent, Is.Empty);
        }
        [Test]
        public void TestDefaultCtorParameter()
        {
            LRU storage = new LRU();
            Assert.That(storage.ListLimit, Is.EqualTo(LRU.DEFAULT_LIMIT));
        }
        [TestCase(2)]
        [TestCase(42)]
        [TestCase(20000)]
        public void TestCtorLimitParameter(int limit)
        {
            LRU storage = new LRU(limit);
            Assert.That(storage.ListLimit, Is.EqualTo(limit));
        }
        [TestCase(0)]
        [TestCase(-42)]
        public void TestCtorBadLimitRefused(int limit)
        {
            Assert.That(() =>
               {
                   LRU storage = new LRU(limit);
               },
                Throws.TypeOf<ArgumentOutOfRangeException>());
        }
        //CONSTRUCTOR TESTS

        //ADD TESTS
        const int LIMIT = 5;
        LRU lru;

        [SetUp]
        public void Setup()
        {
            lru = new LRU(LIMIT);
        }
        [Test]
        public void TestAddNullShouldThrowException()
        {
            Assert.That(() => lru.Add(null), Throws.ArgumentNullException);
        }
        [Test]
        public void TestLimitWorks()
        {
            object[] instances = new object[2 * LIMIT];
            for (int i = 0; i < instances.Length; i++)
            {
                instances[i] = new { Index = i };
                lru.Add(instances[i]);
            }
            Assert.That(lru.Recent.Count, Is.EqualTo(LIMIT));
            for (int i = 0; i < LIMIT; i++)
            {
                Assert.That(lru.Recent, Does.Contain(instances[i + LIMIT]));
            }
        }
        static IEnumerable<TestCaseData> TestCountsSource
        {
            get
            {
                var first = new { index = 1 };
                var second = new { index = 2 };
                var third = new { index = 3 };
                yield return new TestCaseData(new object[] { first, second, third }, 3);
                yield return new TestCaseData(new object[] { first, second, second }, 2);
                yield return new TestCaseData(new object[] { first, first, first }, 1);
            }
        }
        [TestCaseSource(nameof(TestCountsSource))]
        public void TestCounts(object[] instances, int expectedCount)
        {
            foreach (object item in instances)
            {
                lru.Add(item);
            }

            Assert.That(lru.Recent.Count, Is.EqualTo(expectedCount));
        }
        static IEnumerable<TestCaseData> TestOrderSource
        {
            get
            {
                var first = new { index = 1 };
                var second = new { index = 2 };
                var third = new { index = 3 };
                yield return new TestCaseData
                (
                    new object[] { first, second, third }, // items added
                    new object[] {third, second, first } //expected order                        
                );
                yield return new TestCaseData
                (
                    new object[] { first, second, second }, // items added
                    new object[] { second, first } //expected order                        
                );
                yield return new TestCaseData
                (
                    new object[] { first, second, first }, // items added
                    new object[] { first, second } //expected order                        
                );
            }
        }
        [TestCaseSource(nameof(TestOrderSource))]
        public void TestOrder(object[] instances, object[] expectedOrder)
        {
            foreach (object item in instances)
            {
                lru.Add(item);
            }

            for (int i = 0; i < expectedOrder.Length; i++)
            {
                Assert.That(lru.Recent[i], Is.SameAs(expectedOrder[i]));
            }
        }
        //ADD TESTS
    }
}
