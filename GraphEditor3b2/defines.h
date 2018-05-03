#pragma once

using namespace Windows::UI;

#define DEF_EDGE_COLOR Colors.Black
#define DEF_EDGE_LINE_WIDTH 3

#define VERTEX_SIZE 20
#define MIN_VERT_SPACE 10
#define MAX_VERT_SPACE 20
#define MIN_NUM_VERT 6
#define MAX_NUM_VERT 25
#define MIN_X 0
#define MAX_X (VERTEX_SIZE + MAX_VERT_SPACE) * MAX_NUM_VERT
#define MIN_Y 0
#define MAX_Y (VERTES_SIZE + MAX_VERT_SPACE) * MAX_NUM_VERT
#define DEF_VERT_LINE_COLOR Colors.Black
#define DEF_VERT_LINE_WIDTH 3
#define EDGE_PROBABILITY 0.5
#define DEF_FIT_GRAPH_TO_VIEW true
