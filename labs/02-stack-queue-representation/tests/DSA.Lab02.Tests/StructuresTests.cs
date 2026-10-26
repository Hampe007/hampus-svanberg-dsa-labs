using System;
using NUnit.Framework;

namespace DSA.Lab02.Tests
{
    public sealed class StructuresTests
    {
        [Test]
        public void ArrayStack_IsLifo()
        {
            var stack = new ArrayStack<string>(4);
            stack.Push("A"); stack.Push("B"); stack.Push("C");
            Assert.That(stack.Pop(), Is.EqualTo("C"));
            Assert.That(stack.Pop(), Is.EqualTo("B"));
            Assert.That(stack.Pop(), Is.EqualTo("A"));
        }

        [Test]
        public void ArrayStack_PeekDoesNotRemove()
        {
            var stack = new ArrayStack<int>(2);
            stack.Push(10);
            Assert.That(stack.Peek(), Is.EqualTo(10));
            Assert.That(stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void ArrayStack_EmptyPopThrows()
        {
            var stack = new ArrayStack<int>(2);
            Assert.Throws<InvalidOperationException>(() => stack.Pop());
        }

        [Test]
        public void ArrayStack_FullPushThrows()
        {
            var stack = new ArrayStack<int>(1);
            stack.Push(1);
            Assert.Throws<InvalidOperationException>(() => stack.Push(2));
        }

        [Test]
        public void ListQueue_IsFifo()
        {
            var queue = new ListQueue<string>();
            queue.Enqueue("A"); queue.Enqueue("B"); queue.Enqueue("C");
            Assert.That(queue.Dequeue(), Is.EqualTo("A"));
            Assert.That(queue.Dequeue(), Is.EqualTo("B"));
            Assert.That(queue.Dequeue(), Is.EqualTo("C"));
        }

        [Test]
        public void RingQueue_IsFifo()
        {
            var queue = new RingQueue<string>(4);
            queue.Enqueue("A"); queue.Enqueue("B"); queue.Enqueue("C");
            Assert.That(queue.Dequeue(), Is.EqualTo("A"));
            Assert.That(queue.Dequeue(), Is.EqualTo("B"));
            Assert.That(queue.Dequeue(), Is.EqualTo("C"));
        }

        [Test]
        public void RingQueue_WrapsAround()
        {
            var queue = new RingQueue<int>(3);
            queue.Enqueue(1); queue.Enqueue(2); queue.Enqueue(3);
            Assert.That(queue.Dequeue(), Is.EqualTo(1));
            Assert.That(queue.Dequeue(), Is.EqualTo(2));
            queue.Enqueue(4); queue.Enqueue(5);
            Assert.That(queue.Dequeue(), Is.EqualTo(3));
            Assert.That(queue.Dequeue(), Is.EqualTo(4));
            Assert.That(queue.Dequeue(), Is.EqualTo(5));
        }

        [Test]
        public void RingQueue_PeekDoesNotRemove()
        {
            var queue = new RingQueue<int>(2);
            queue.Enqueue(7);
            Assert.That(queue.Peek(), Is.EqualTo(7));
            Assert.That(queue.Count, Is.EqualTo(1));
        }

        [Test]
        public void RingQueue_EmptyDequeueThrows()
        {
            var queue = new RingQueue<int>(2);
            Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
        }

        [Test]
        public void RingQueue_FullEnqueueThrows()
        {
            var queue = new RingQueue<int>(1);
            queue.Enqueue(1);
            Assert.Throws<InvalidOperationException>(() => queue.Enqueue(2));
        }
    }
}
