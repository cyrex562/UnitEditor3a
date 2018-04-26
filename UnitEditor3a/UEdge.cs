using System;
using System.Runtime.Serialization;

namespace UnitEditor3a
{
    [DataContract]
    public class UEdge
    {
        [DataMember]
        public Guid HeadVertexId { get; set; }
        [DataMember]
        public Guid TailVertexId { get; set; }
        [DataMember]
        public int Value;
        [DataMember]
        public EdgeDirection Direction { get; set; }
        [DataMember]
        public Guid EdgeId { get; set; }

        public UEdge()
        {
            this.HeadVertexId = Guid.Empty;
            this.TailVertexId = Guid.Empty;
            this.Value = -1;
            this.Direction = EdgeDirection.None;
            this.EdgeId = Guid.NewGuid();
        }
    }
}