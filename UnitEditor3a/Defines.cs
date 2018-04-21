using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace UnitEditor3a
{
    class Defines
    {
        public const int INIT_NUM_ADJ_MAT_ELE = 8;
        public const int INIT_NUM_INC_MAT_ELE = 8;
        public const int NODE_SIZE = 40;
        public const int MIN_NUM_NODES = 6;
        public const int MAX_NUM_NODES = 25;
        public const int MIN_NODE_SPACE = 10;
        public const int MAX_NODE_SPACE = 20;
        public const int MIN_X = 0;
        public const int
            MAX_X = NODE_SIZE * (MAX_NUM_NODES + MAX_NODE_SPACE);
        public const int MIN_Y = 0;
        public const int MAX_Y = NODE_SIZE * (MAX_NUM_NODES + MAX_NODE_SPACE);
        public static readonly Color DEFAULT_NODE_COLOR = Colors.Black;
        public const int NODE_LINE_WIDTH = 3;
    }
}
