using System;
using System.Runtime.Serialization;

namespace GraphEditor3b3
{
    [DataContract]
    public class Edge
    {
        [DataMember]
        public Guid HeadVertexId { get; set; }
        [DataMember]
        public Guid TailVertexId { get; set; }
        [DataMember]
        public Int32 Value;
        [DataMember]
        public EdgeDirection Direction { get; set; }
        [DataMember]
        public Guid EdgeId { get; set; }

        public Edge()
        {
            this.HeadVertexId = Guid.Empty;
            this.TailVertexId = Guid.Empty;
            this.Value = -1;
            this.Direction = EdgeDirection.None;
            this.EdgeId = Guid.NewGuid();
        }
    }
}