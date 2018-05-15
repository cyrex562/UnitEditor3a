using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas.Brushes;
using Windows.UI;

namespace GraphEditor3b3
{
    class Defines
    {
        public const UInt32 INIT_NUM_ADJ_MAT_ELE = 8;
        public const UInt32 INIT_NUM_INC_MAT_ELE = 8;
        public const UInt32 VERTEX_SIZE = 20;
        public const UInt32 MIN_NUM_VERTS = 1;
        public const UInt32 MAX_NUM_VERTS = 100;
        public const UInt32 MIN_VERT_SPACE = 10;
        public const UInt32 MAX_VERT_SPACE = 20;
        public const UInt32 MIN_X = 0;
        public const UInt32
            MAX_X = VERTEX_SIZE * (MAX_NUM_VERTS + MAX_VERT_SPACE);
        public const UInt32 MIN_Y = 0;
        public const UInt32 MAX_Y = VERTEX_SIZE * (MAX_NUM_VERTS + MAX_VERT_SPACE);
        public static readonly Color DEF_VERT_LINE_COLOR = Colors.White;
       
        public const Double EDGE_PROBABILITY = 0.5;

        public static readonly Color DEF_EDGE_COLOR = Colors.White;
        public static readonly Color SEL_EDGE_COLOR = Colors.Red;
        public static readonly Color SEL_VERT_LINE_COLOR = Colors.Red;
        public const UInt32 DEF_EDGE_LINE_WIDTH = 3;
        public const UInt32 DEF_VERT_LINE_WIDTH = 3;
        public const Boolean FIT_GRAPH_TO_VIEW = true;
    }
}
