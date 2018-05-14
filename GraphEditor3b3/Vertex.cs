using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace GraphEditor3b3
{


    //[DataContract]
    //public class Vertex
    //{
    //    [DataMember]
    //    public List<Guid> Neighbors { get; set; }
    //    [DataMember]
    //    public List<Guid> Edges { get; set; }
    //    [DataMember]
    //    public Guid VertexId { get; set; }
    //    [DataMember]
    //    public Int32 Value { get; set; }

    //    public Vertex()
    //    {
    //        this.Neighbors = new List<Guid>();
    //        this.Edges = new List<Guid>();
    //        this.VertexId = Guid.NewGuid();
    //        this.Value = -1;
    //    }

        
    //    public Boolean NodeInNeighbors(Guid nodeId)
    //    {
    //        Debug.WriteLine("checking for neighbor");
    //        foreach(Guid neighId in this.Neighbors) {
    //            if (neighId == nodeId)
    //            {
    //                return true;
    //            }
    //        }

    //        return false;
    //    }

    //    public void AddNeighbor(Guid newNeighbor)
    //    {
    //        Debug.WriteLine("adding neighbor");
    //        if (NodeInNeighbors(newNeighbor) == false)
    //        {
    //            this.Neighbors.Add(newNeighbor);
    //        }
    //    }

    //    public void AddEdge(Guid newEdge)
    //    {
    //        Debug.WriteLine("adding edge");
    //        if (EdgeInEdges(newEdge) == false)
    //        {
    //            this.Edges.Add(newEdge);
    //        }
    //    }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="edgeIdTerm"></param>
    //    /// <returns></returns>
    //    public Boolean EdgeInEdges(Guid edgeIdTerm)
    //    {
    //        Debug.WriteLine("checking for incident edge");
    //        foreach(Guid edgeId in this.Edges)
    //        {
    //            if (edgeIdTerm == edgeId)
    //            {
    //                return true;
    //            }
    //        }

    //        return false;
    //    }

    //    public String ListItemText
    //    {
    //        get
    //        {
    //            return String.Format("{0}: {1}", this.VertexId, this.Value);
    //        }
    //    }

    //    public String ShortListItemText
    //    {
    //        get
    //        {
    //            return String.Format("{0}", this.VertexId.ToString().Substring(0, 6));
    //        }
    //    }
    //} // end of class
} // end of namepsace