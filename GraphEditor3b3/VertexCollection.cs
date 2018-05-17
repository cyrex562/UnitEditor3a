using System;
using System.Collections.Generic;
using System.ComponentModel;
using Windows.UI.Xaml.Data;

namespace GraphEditor3b3
{
    public class VertexCollection : INotifyPropertyChanged, IList<GraphVertex>, ISupportIncrementalLoading
    {
        private Int32 count;
        public Int32 Count { get { return this.count; } }
        public Boolean IsFixedSize { get { return false; } }
        public Boolean IsReadOnly { get { return false; } }
        // TODO: implement sync root, per: https://msdn.microsoft.com/en-us/library/windows/apps/xaml/system.collections.icollection.issynchronized.aspx
        public Boolean IsSynchronized { get { return false; } }

        public VertexCollection()
        {
            this.count = 0;
            this.graphVertices = new List<GraphVertex>();
        }

        public Int32 Add(GraphVertex value)
        {
            this.graphVertices.Add(value);
            this.count += 1;
            return this.count - 1;
        }

        public void Clear()
        {
            this.count = 0;
            this.graphVertices.Clear();
        }

        public Boolean Contains(GraphVertex value)
        {
            return this.graphVertices.Contains(value);
        }

        public Int32 IndexOf(GraphVertex value)
        {
            return this.graphVertices.IndexOf(value);
        }

        public void Insert(Int32 index, GraphVertex value)
        {
            this.graphVertices.Insert(index, value);
            this.count += 1;
        }

        public GraphVertex this[Int32 index]
        {
            get
            {
                return graphVertices[index];
            }
            set
            {
                graphVertices[index] = value;
            }
        }

        public void Remove(GraphVertex value)
        {
            RemoveAt(IndexOf(value));
        }

        public void RemoveAt(Int32 index)
        {
            if ((index >= 0) && (index < this.count))
            {
                this.graphVertices.RemoveAt(index);
                this.count--;
            }
        }

        public void CopyTo(Array array, Int32 index)
        {
            Int32 j = index;
            for (Int32 i = 0; i < this.count; i++)
            {
                array.SetValue(this.graphVertices[i], j);
                j++;
            }
        }

        public Object SyncRoot
        {
            get
            {
                return this;
            }
        }

        //public IEnumerator GetEnumerator()
        //{
        //    return this.graphVertices.GetEnumerator();
        //}

        public IEnumerator GetEnumerator()
        {
            return this.graphVertices.GetEnumerator();
        }



        private List<GraphVertex> graphVertices;

    }
}