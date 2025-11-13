// BinarySearchTree.cs
using System;
using System.Collections.Generic;

namespace MunicipalServicesApp
{
    public class BinarySearchTree<T> where T : IComparable<T>
    {
        private class TreeNode
        {
            public T Data { get; set; }
            public TreeNode Left { get; set; }
            public TreeNode Right { get; set; }

            public TreeNode(T data)
            {
                Data = data;
                Left = Right = null;
            }
        }

        private TreeNode root;

        public void Insert(T data)
        {
            root = InsertRec(root, data);
        }

        private TreeNode InsertRec(TreeNode node, T data)
        {
            if (node == null)
            {
                node = new TreeNode(data);
                return node;
            }

            if (data.CompareTo(node.Data) < 0)
                node.Left = InsertRec(node.Left, data);
            else if (data.CompareTo(node.Data) > 0)
                node.Right = InsertRec(node.Right, data);

            return node;
        }

        public T Search(T data)
        {
            return SearchRec(root, data);
        }

        private T SearchRec(TreeNode node, T data)
        {
            if (node == null)
                return default(T);

            if (data.CompareTo(node.Data) == 0)
                return node.Data;

            if (data.CompareTo(node.Data) < 0)
                return SearchRec(node.Left, data);

            return SearchRec(node.Right, data);
        }

        public List<T> InOrderTraversal()
        {
            List<T> result = new List<T>();
            InOrderRec(root, result);
            return result;
        }

        private void InOrderRec(TreeNode node, List<T> result)
        {
            if (node != null)
            {
                InOrderRec(node.Left, result);
                result.Add(node.Data);
                InOrderRec(node.Right, result);
            }
        }
    }
}