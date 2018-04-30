using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas.Brushes;
using Windows.UI;

namespace UnitEditor3a
{
    class Defines
    {
        public const Int32 INIT_NUM_ADJ_MAT_ELE = 8;
        public const Int32 INIT_NUM_INC_MAT_ELE = 8;
        public const Int32 VERTEX_SIZE = 20;
        public const Int32 MIN_NUM_NODES = 6;
        public const Int32 MAX_NUM_NODES = 25;
        public const Int32 MIN_NODE_SPACE = 10;
        public const Int32 MAX_VERTEX_SPACE = 20;
        public const Int32 MIN_X = 0;
        public const Int32
            MAX_X = VERTEX_SIZE * (MAX_NUM_NODES + MAX_VERTEX_SPACE);
        public const Int32 MIN_Y = 0;
        public const Int32 MAX_Y = VERTEX_SIZE * (MAX_NUM_NODES + MAX_VERTEX_SPACE);
        public static readonly Color DEFAULT_NODE_COLOR = Colors.Black;
        public const Int32 NODE_LINE_WIDTH = 3;
        public const Double EDGE_PROBABILITY = 0.5;

        public static readonly Color DEF_EDGE_COLOR = Colors.Black;
        public const Int32 DEF_EDGE_LINE_WIDTH = 3;
        public const Boolean FIT_GRAPH_TO_VIEW = true;
    }
}
