using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LoggerClientLibrary.Collections.Generics
{
    public class LockFreeQueue<T>
    {
        class Node
        {
            public T Value;
            public Pointer Next;
        }

        struct Pointer
        {
            /// <summary>
            /// Copy constructor
            /// </summary>
            /// <param name="p"></param>
            public Pointer(Pointer p)
            {
                PointerNode = p.PointerNode;
                Count = p.Count;
            }

            /// <summary>
            /// Constructor that allows caller to specify pointer node and count
            /// </summary>
            /// <param name="node"></param>
            /// <param name="counter"></param>
            public Pointer(Node node, long counter)
            {
                PointerNode = node;
                Count = counter;
            }

            public long Count;
            public Node PointerNode; 
        }


        private Pointer m_Head;
        private Pointer m_Tail;

        public LockFreeQueue()
        {
            Node node = new Node();
            m_Head.PointerNode = m_Tail.PointerNode = node;
        }

        /// <summary>
        /// Interlocked Compare and Exchange operation
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="compared"></param>
        /// <param name="exchange"></param>
        /// <returns></returns>
        private bool CompareAndSwap(ref Pointer destination, Pointer compared, Pointer exchange)
        {
            if (compared.PointerNode == Interlocked.CompareExchange(ref destination.PointerNode, exchange.PointerNode, compared.PointerNode))
            {
                Interlocked.Exchange(ref destination.Count, exchange.Count);
                return true;
            }

            return false;
        }


        public long Count { get { return Math.Abs(m_Tail.Count - m_Head.Count); } }

        public bool Deque(ref T t)
        {
            Pointer head;

            // Keep trying until deque is done
            bool bDequeNotDone = true;
            while (bDequeNotDone)
            {
                // read head
                head = m_Head;

                // read tail
                Pointer tail = m_Tail;

                // read next
                Pointer next = head.PointerNode.Next;

                // Are head, tail, and next consistent?
                if (head.Count == m_Head.Count && head.PointerNode == m_Head.PointerNode)
                {
                    // is tail falling behind
                    if (head.PointerNode == tail.PointerNode)
                    {
                        // is the queue empty?
                        if (null == next.PointerNode)
                        {
                            // queue is empty cannnot dequeue
                            return false;
                        }

                        // Tail is falling behind. try to advance it
                        CompareAndSwap(ref m_Tail, tail, new Pointer(next.PointerNode, tail.Count + 1));

                    }
                    else // No need to deal with tail
                    {
                        // read value before CAS otherwise another deque might try to free the next node
                        t = next.PointerNode.Value;

                        // try to swing the head to the next node
                        if (CompareAndSwap(ref m_Head, head, new Pointer(next.PointerNode, head.Count + 1)))
                        {
                            bDequeNotDone = false;
                        }
                    }

                } // endif

            } // endloop

            // dispose of head.ptr
            return true;
        }

        public void Enqueue(T t)
        {
            // Allocate a new node from the free list
            Node node = new Node();

            // copy enqueued value into node
            node.Value = t;

            // keep trying until Enqueue is done
            bool bEnqueueNotDone = true;

            while (bEnqueueNotDone)
            {
                // read Tail.ptr and Tail.count together
                Pointer tail = m_Tail;

                // read next ptr and next count together
                Pointer next = tail.PointerNode.Next;

                // are tail and next consistent
                if (tail.Count == m_Tail.Count && tail.PointerNode == m_Tail.PointerNode)
                {
                    // was tail pointing to the last node?
                    if (null == next.PointerNode)
                    {
                        if (CompareAndSwap(ref tail.PointerNode.Next, next, new Pointer(node, next.Count + 1)))
                        {
                            bEnqueueNotDone = false;
                        } // endif

                    }
                    else // tail was not pointing to last node
                    {
                        // try to swing Tail to the next node
                        CompareAndSwap(ref m_Tail, tail, new Pointer(next.PointerNode, tail.Count + 1));
                    }

                } // endif

            } // endloop
        }

    }
}
