using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServicesApp
{
    public class MinHeap<T> where T : IComparable<T>
    {
        private List<T> heap;

        public MinHeap()
        {
            heap = new List<T>();
        }

        public void Insert(T item)
        {
            heap.Add(item);
            HeapifyUp(heap.Count - 1);
        }

        public T ExtractMin()
        {
            if (heap.Count == 0)
                throw new InvalidOperationException("Heap is empty");

            T min = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            HeapifyDown(0);
            return min;
        }

        public T PeekMin()
        {
            if (heap.Count == 0)
                throw new InvalidOperationException("Heap is empty");
            return heap[0];
        }

        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parent = (index - 1) / 2;
                if (heap[parent].CompareTo(heap[index]) <= 0)
                    break;

                Swap(parent, index);
                index = parent;
            }
        }

        private void HeapifyDown(int index)
        {
            int leftChild, rightChild, smallest;

            while (index < heap.Count)
            {
                leftChild = 2 * index + 1;
                rightChild = 2 * index + 2;
                smallest = index;

                if (leftChild < heap.Count && heap[leftChild].CompareTo(heap[smallest]) < 0)
                    smallest = leftChild;

                if (rightChild < heap.Count && heap[rightChild].CompareTo(heap[smallest]) < 0)
                    smallest = rightChild;

                if (smallest == index)
                    break;

                Swap(index, smallest);
                index = smallest;
            }
        }

        private void Swap(int i, int j)
        {
            T temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }

        public int Count => heap.Count;
        public bool IsEmpty => heap.Count == 0;
    }
}