using System;

namespace Pentago
{
    class Move
    {
        public int i;
        public int j;
        public int blockI;
        public int blockJ;
        public int direction;
        public int score;

        public Move(int i, int j, int blockI, int blockJ, int direction)
        {
            this.i = i;
            this.j = j;
            this.blockI = blockI;
            this.blockJ = blockJ;
            this.direction = direction;
            this.score = 0;
        }

        public Move(int score)
        {
            this.i = 0;
            this.j = 0;
            this.blockI = 0;
            this.blockJ = 0;
            this.direction = 0;
            this.score = -1000000;
        }

        public Move(Move move)
        {
            this.i = move.i;
            this.j = move.j;
            this.blockI = move.blockI;
            this.blockJ = move.blockJ;
            this.direction = move.direction;
            this.score = move.score;
        }
    }
}