using System;
using System.Collections.Generic;
using System.Linq;

namespace MunicipalServicesApp
{
    public class PriorityQueue<T> where T : IComparable<T>
    {
        private List<T> heap;
        private readonly IComparer<T> comparer;

        #region Constructors
        public PriorityQueue() : this(Comparer<T>.Default) { }

        public PriorityQueue(IComparer<T> comparer)
        {
            this.heap = new List<T>();
            this.comparer = comparer ?? Comparer<T>.Default;
        }

        public PriorityQueue(int capacity) : this(capacity, Comparer<T>.Default) { }

        public PriorityQueue(int capacity, IComparer<T> comparer)
        {
            this.heap = new List<T>(capacity);
            this.comparer = comparer ?? Comparer<T>.Default;
        }

        public PriorityQueue(IEnumerable<T> collection) : this(collection, Comparer<T>.Default) { }

        public PriorityQueue(IEnumerable<T> collection, IComparer<T> comparer)
        {
            this.comparer = comparer ?? Comparer<T>.Default;
            this.heap = new List<T>(collection);

            // Heapify the collection
            for (int i = (heap.Count - 2) / 2; i >= 0; i--)
            {
                HeapifyDown(i);
            }
        }
        #endregion

        #region Core Queue Operations
        public void Enqueue(T item)
        {
            heap.Add(item);
            HeapifyUp(heap.Count - 1);
        }

        public T Dequeue()
        {
            if (heap.Count == 0)
                throw new InvalidOperationException("Queue is empty");

            T frontItem = heap[0];
            int lastIndex = heap.Count - 1;
            heap[0] = heap[lastIndex];
            heap.RemoveAt(lastIndex);

            if (heap.Count > 0)
                HeapifyDown(0);

            return frontItem;
        }

        public T Peek()
        {
            if (heap.Count == 0)
                throw new InvalidOperationException("Queue is empty");
            return heap[0];
        }

        public bool TryDequeue(out T result)
        {
            if (heap.Count == 0)
            {
                result = default(T);
                return false;
            }

            result = Dequeue();
            return true;
        }

        public bool TryPeek(out T result)
        {
            if (heap.Count == 0)
            {
                result = default(T);
                return false;
            }

            result = Peek();
            return true;
        }
        #endregion

        #region Heap Maintenance Methods
        private void HeapifyUp(int index)
        {
            int current = index;
            while (current > 0)
            {
                int parent = (current - 1) / 2;
                if (comparer.Compare(heap[parent], heap[current]) <= 0)
                    break;

                Swap(parent, current);
                current = parent;
            }
        }

        private void HeapifyDown(int index)
        {
            int current = index;
            int last = heap.Count - 1;

            while (true)
            {
                int leftChild = 2 * current + 1;
                int rightChild = 2 * current + 2;
                int smallest = current;

                if (leftChild <= last && comparer.Compare(heap[leftChild], heap[smallest]) < 0)
                    smallest = leftChild;

                if (rightChild <= last && comparer.Compare(heap[rightChild], heap[smallest]) < 0)
                    smallest = rightChild;

                if (smallest == current)
                    break;

                Swap(current, smallest);
                current = smallest;
            }
        }

        private void Swap(int i, int j)
        {
            T temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }
        #endregion

        #region Utility Methods
        public int Count => heap.Count;

        public bool IsEmpty => heap.Count == 0;

        public void Clear()
        {
            heap.Clear();
        }

        public bool Contains(T item)
        {
            return heap.Contains(item);
        }

        public T[] ToArray()
        {
            return heap.ToArray();
        }

        public List<T> ToList()
        {
            return new List<T>(heap);
        }

        public void TrimExcess()
        {
            heap.TrimExcess();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return heap.GetEnumerator();
        }
        #endregion

        #region Advanced Operations
        public bool Remove(T item)
        {
            int index = heap.IndexOf(item);
            if (index == -1)
                return false;

            int lastIndex = heap.Count - 1;
            if (index == lastIndex)
            {
                heap.RemoveAt(lastIndex);
            }
            else
            {
                heap[index] = heap[lastIndex];
                heap.RemoveAt(lastIndex);
                HeapifyDown(index);
                HeapifyUp(index); // May need to bubble up if the replacement was smaller
            }

            return true;
        }

        public void UpdatePriority(T item)
        {
            int index = heap.IndexOf(item);
            if (index != -1)
            {
                // Remove and re-add to maintain heap property
                Remove(item);
                Enqueue(item);
            }
        }

        public void Merge(PriorityQueue<T> otherQueue)
        {
            foreach (T item in otherQueue.heap)
            {
                Enqueue(item);
            }
        }

        public PriorityQueue<T> Copy()
        {
            return new PriorityQueue<T>(this.heap, this.comparer);
        }
        #endregion

        #region Service Request Specific Extensions
        public List<T> GetItemsByPriority(int count = -1)
        {
            if (count <= 0 || count > heap.Count)
                count = heap.Count;

            List<T> result = new List<T>();
            PriorityQueue<T> tempQueue = this.Copy();

            for (int i = 0; i < count && tempQueue.Count > 0; i++)
            {
                result.Add(tempQueue.Dequeue());
            }

            return result;
        }

        public T Find(Predicate<T> match)
        {
            return heap.Find(match);
        }

        public List<T> FindAll(Predicate<T> match)
        {
            return heap.FindAll(match);
        }

        public bool Exists(Predicate<T> match)
        {
            return heap.Exists(match);
        }
        #endregion

        #region Performance Monitoring
        public int Capacity => heap.Capacity;

        public double LoadFactor => (double)heap.Count / heap.Capacity;

        public void EnsureCapacity(int capacity)
        {
            heap.Capacity = capacity;
        }
        #endregion

        public override string ToString()
        {
            return $"PriorityQueue<{typeof(T).Name}> [Count = {Count}]";
        }
    }

    // Specialized Priority Queue for Service Requests
    public class ServiceRequestPriorityQueue : PriorityQueue<ServiceRequest>
    {
        public ServiceRequestPriorityQueue() : base(new ServiceRequestComparer()) { }

        public ServiceRequestPriorityQueue(IEnumerable<ServiceRequest> requests)
            : base(requests, new ServiceRequestComparer()) { }

        // Service Request specific methods
        public List<ServiceRequest> GetHighPriorityRequests()
        {
            return FindAll(req => req.Priority == 1);
        }

        public List<ServiceRequest> GetRequestsByStatus(string status)
        {
            return FindAll(req => req.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
        }

        public ServiceRequest FindByRequestId(string requestId)
        {
            return Find(req => req.RequestId == requestId);
        }

        private class ServiceRequestComparer : IComparer<ServiceRequest>
        {
            public int Compare(ServiceRequest x, ServiceRequest y)
            {
                if (x == null && y == null) return 0;
                if (x == null) return 1;
                if (y == null) return -1;

                // First compare by priority (lower number = higher priority)
                int priorityComparison = x.Priority.CompareTo(y.Priority);
                if (priorityComparison != 0)
                    return priorityComparison;

                // Then by date (older requests have higher priority)
                return x.DateSubmitted.CompareTo(y.DateSubmitted);
            }
        }
    }

    // Extension methods for LINQ-like operations
    public static class PriorityQueueExtensions
    {
        public static PriorityQueue<T> ToPriorityQueue<T>(this IEnumerable<T> collection) where T : IComparable<T>
        {
            return new PriorityQueue<T>(collection);
        }

        public static PriorityQueue<T> ToPriorityQueue<T>(this IEnumerable<T> collection, IComparer<T> comparer) where T : IComparable<T>
        {
            return new PriorityQueue<T>(collection, comparer);
        }

        
        public static IEnumerable<T> DequeueAll<T>(this PriorityQueue<T> queue) where T : IComparable<T>
        {
            while (!queue.IsEmpty)
            {
                yield return queue.Dequeue();
            }
        }
    }
}