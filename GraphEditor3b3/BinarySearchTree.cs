using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor3b3
{
    //public class TreeNodeList<T> : Collection<TreeNode<T>>
    //{
    //    public TreeNodeList(): base() { }

    //    public TreeNodeList(int initialSize)
    //    {
    //        for (int i = 0; i < initialSize; i++)
    //        {
    //            base.Items.Add(default<TreeNode<T>));
    //        }
    //    }

    //    public TreeNode<T> FindByValue(T value)
    //{

    //}
    //}
    //public class TreeNode<T>
    //{
    //    private T data;
    //    private NodeList<T> neighbors = null;

    //    public TreeNode() { }
    //    public TreeNode(T data) : this(data, null) { }
    //}

    public class BSTNode<T>
    {
        public T Key { get; set; }

        public BSTNode() {}
    }
    public class BinarySearchTree<T>
    {
    }
}
