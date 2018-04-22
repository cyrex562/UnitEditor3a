using System;

namespace UnitEditor3a
{
    public class UEdge
    {

        public Guid HeadVertexId { get; set; }
        public Guid TailVertexId { get; set; }
        public int Value;
        public EdgeDirection Direction { get; set; }
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