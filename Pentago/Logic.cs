using System;

namespace Pentago
{
    class Logic
    {
        int[,] board;
        int depth;

        public Logic(int[,]board, int depth)
        {
            this.board = new int[6, 6];
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                    this.board[i, j] = board[i, j];
            this.depth = depth;
        }

        public void setBoard(int[,]board)
        {
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                    this.board[i, j] = board[i, j];
        }

        public int[,] getBoard()
        {
            return this.board;
        }

        public void setDepth(int depth)
        {
            this.depth = depth;
        }

        public void rotate(int i, int j, int d)
        {
            //get the values of the circles
            int[] values = new int[8];
            int x = i, y = j;
            for (int k = 0; k < values.Length; k++)
            {
                values[k] = this.board[x, y];
                int xy = getRotationValues(x, y, i, j);
                x = xy / 10;
                y = xy % 10;
            }
            //rotate the block
            x = i;
            y = j;
            if (d == 0)
                y += 2;
            else
                x += 2;
            for (int k = 0; k < values.Length; k++)
            {
                this.board[x, y] = values[k];
                int xy = getRotationValues(x, y, i, j);
                x = xy / 10;
                y = xy % 10;
            }
        }

        private int getRotationValues(int x, int y, int i, int j)
        {
            //changes x and y in a rotating-square order
            if (x == i)
                if (y == j + 2)
                    x++;
                else
                    y++;
            else
            if (x == i + 1)
                if (y == j + 2)
                    x++;
                else
                    x--;
            else
            if (y == j)
                x--;
            else
                y--;
            return x * 10 + y;
        }

        public int winnerStatus(Move move, int t)
        {
            //check winner in the selected slot
            int winner = checkWinner(move.i, move.j, this.board[move.i, move.j], t);
            if (winner == 1 || winner == 2)
                return winner;
            //check winner in all rotated slots
            for (int i = move.blockI; i < move.blockI + 3; i++)
                for (int j = move.blockJ; j < move.blockJ + 3; j++)
                    if ((this.board[i, j] != 0) && (checkWinner(i, j, this.board[i, j], t) == this.board[i, j]))
                        return this.board[i, j];
            return winner;
        }

        private int checkWinner(int row, int col, int val, int turn)
        {
            //returns 1/2 if player 1 or 2 won, (2 is AI), 3 if there's a tie, 0 if the game hasn't ended yet
            if (countRowSequence(row, col, val) % 10 >= 5)
                return val;
            if (countColumnSequence(row, col, val) % 10 >= 5)
                return val;
            if (countSlopeSequence(row, col, val) % 10 >= 5)
                return val;
            if (countClimbSequence(row, col, val) % 10 >= 5)
                return val;
            if (turn == 35)
                return 3; //tie
            return 0; //no winner yet
        }

        private int countRowSequence(int row, int col, int val)
        {
            int otherVal = (val == 1) ? 2 : 1;
            //is the sequence blocked on 1 side, both sides, or not at all
            int blockers = 0;
            //count the picked slot
            int c = 1;
            //count left of slot
            int j = col - 1;
            if (j == -1)
                blockers++; //a wall is also a blocker
            while (j >= 0)
            {
                if (this.board[row, j] == val)
                    c++;
                else
                {
                    if (this.board[row, j] == otherVal) //the other marble blocked this sequence
                        blockers++;
                    j = -1;
                }
                j--;
                if (j == -1)
                    blockers++; //a wall blocked this sequence
            }
            //count right of slot
            j = col + 1;
            if (j == 6)
                blockers++;
            while (j <= 5)
            {
                if (this.board[row, j] == val)
                    c++;
                else
                {
                    if (this.board[row, j] == otherVal)
                        blockers++;
                    j = 6;
                }
                j++;
                if (j == 6)
                    blockers++; //a wall blocked this sequence
            }
            return c + blockers * 10;
        }

        private int countColumnSequence(int row, int col, int val)
        {
            int otherVal = (val == 1) ? 2 : 1;
            //is the sequence blocked on 1 side, both sides, or not at all
            int blockers = 0;
            //count the picked slot
            int c = 1;
            //count above the slot
            int i = row - 1;
            if (i == -1)
                blockers++; //a wall is also a blocker
            while (i >= 0)
            {
                if (this.board[i, col] == val)
                    c++;
                else
                {
                    if (this.board[i, col] == otherVal) //the other marble blocked this sequence
                        blockers++;
                    i = -1;
                }
                i--;
                if (i == -1)
                    blockers++; //a wall blocked this sequence
            }
            //count below the slot
            i = row + 1;
            if (i == 6)
                blockers++;
            while (i <= 5)
            {
                if (this.board[i, col] == val)
                    c++;
                else
                {
                    if (this.board[i, col] == otherVal)
                        blockers++;
                    i = 6;
                }
                i++;
                if (i == 6)
                    blockers++; //a wall blocked this sequence
            }
            return c + blockers * 10;
        }

        private int countSlopeSequence(int row, int col, int val)
        {
            int otherVal = (val == 1) ? 2 : 1;
            //is the sequence blocked on 1 side, both sides, or not at all
            int blockers = 0;
            //count the picked slot
            int c = 1;
            //count left and above of the slot
            int i = row - 1;
            int j = col - 1;
            if (i == -1 || j == -1)
                blockers++; //a wall is also a blocker
            while (i >= 0 && j >= 0)
            {
                if (this.board[i, j] == val)
                    c++;
                else
                {
                    if (this.board[i, j] == otherVal) //the other marble blocked this sequence
                        blockers++;
                    i = -1;
                }
                i--;
                j--;
                if (i == -1 || j == -1)
                    blockers++; //a wall blocked this sequence
            }
            //count right and below the slot
            i = row + 1;
            j = col + 1;
            if (i == 6 || j == 6)
                blockers++;
            while (i <= 5 && j <= 5)
            {
                if (this.board[i, j] == val)
                    c++;
                else
                {
                    if (this.board[i, j] == otherVal)
                        blockers++;
                    i = 6;
                }
                i++;
                j++;
                if (i == 6 || j == 6)
                    blockers++; //a wall blocked this sequence
            }
            return c + blockers * 10;
        }

        private int countClimbSequence(int row, int col, int val)
        {
            int otherVal = (val == 1) ? 2 : 1;
            //is the sequence blocked on 1 side, both sides, or not at all
            int blockers = 0;
            //count the picked slot
            int c = 1;
            //count right and above of the slot
            int i = row - 1;
            int j = col + 1;
            if (i == -1 || j == 6)
                blockers++; //a wall is also a blocker
            while (i >= 0 && j <= 5)
            {
                if (this.board[i, j] == val)
                    c++;
                else
                {
                    if (this.board[i, j] == otherVal) //the other marble blocked this sequence
                        blockers++;
                    i = -1;
                }
                i--;
                j++;
                if (i == -1 || j == 6)
                    blockers++; //a wall blocked this sequence
            }
            //count left and below the slot
            i = row + 1;
            j = col - 1;
            if (i == 6 || j == -1)
                blockers++;
            while (i <= 5 && j >= 0)
            {
                if (this.board[i, j] == val)
                    c++;
                else
                {
                    if (this.board[i, j] == otherVal)
                        blockers++;
                    i = 6;
                }
                i++;
                j--;
                if (i == 6 || j == -1)
                    blockers++; //a wall blocked this sequence
            }
            return c + blockers * 10;
        }

        private int marbleLocations(int i, int j)
        {
            //refers the location of the marble to a block
            if (i >= 3)
                i -= 3;
            if (j >= 3)
                j -= 3;
            switch (i + j) //location of a marble in a block
            {
                case 0: //far corner
                    return 5;
                case 1: //far sides
                    return 3;
                case 2:
                    if (i == j) //middle
                        return 30;
                    return 1;
                case 3: //middle sides
                    return 10;
                default: //middle corner
                    return 20;
            }
        }

        private int[] sequences(int i, int j)
        {
            int[] sequencePoints = new int[6] { 0, 0, 0, 0, 0, 0 };
            int marble = this.board[i, j];
            //count all the different types of sequences
            int row = this.countRowSequence(i, j, marble), col = this.countColumnSequence(i, j, marble), slope = this.countSlopeSequence(i, j, marble), climb = this.countClimbSequence(i, j, marble);
            int[] seq = new int[4] { row % 10, col % 10, slope % 10, climb % 10 };
            //count how much marbles are blocking this sequence
            int[] blo = new int[4] { row / 10, col / 10, slope / 10, climb / 10 };
            for (int x = 0; x < 4; x++)
                switch (seq[x])
                {
                    case 2: //found a sequence of 2
                        if (marble == 1) //player marble
                            if (blo[x] == 0)
                                sequencePoints[0] -= 100;
                            else if(blo[x] == 2)
                                sequencePoints[0] += 100;
                        else //AI marble
                            if (blo[x] == 0) //if sequence isn't blocked
                                sequencePoints[1] += 25;
                            else if (blo[x] == 2)
                                sequencePoints[1] -= 25;
                        break;
                    case 3: //found a sequence of 3
                        if (marble == 1) //player marble
                            if (blo[x] == 0)
                                sequencePoints[2] -= 1000;
                            else if (blo[x] == 2)
                                sequencePoints[2] += 1000;
                        else //AI marble
                            if (blo[x] == 0) //if sequence isn't blocked
                                sequencePoints[3] += 250;
                            else if (blo[x] == 2)
                                sequencePoints[3] -= 250;
                        break;
                    case 4: //found a sequence of 4
                        if (marble == 1) //player marble
                            if (blo[x] == 0)
                                sequencePoints[4] -= 10000;
                            else if (blo[x] == 2)
                                sequencePoints[4] += 10000;
                        else //AI marble
                            if (blo[x] == 0) //if sequence isn't blocked
                                sequencePoints[5] += 2500;
                            else if (blo[x] == 2)
                                sequencePoints[5] -= 2500;
                        break;
                }
            return sequencePoints;
        }

        private int popularBlocks(int i, int j)
        {
            int white = 0, black = 0;
            for (int x = i; x < i + 3; x++)
                for (int y = j; y < j + 3; y++) //each marble in a block
                {
                    switch (this.board[x, y])
                    {
                        case 1: //white marble
                            white++;
                            break;
                        case 2: //black marble
                            black++;
                            break;
                    }
                }
            //removes points to the board's state according to how much more player marbles are taking a block
            if (white > black)
                return 20 * (white - black);
            return 0;
        }

        public int gapInSequence(int[]rcd, int id)
        {
            int empty = 0, points = 0;
            for (int i = 0; i < rcd.Length; i++)
                if (this.board[rcd[i] / 6, rcd[i] % 6] == 0)
                    empty++;
            if((rcd.Length==5 && empty > 2) || (rcd.Length == 6 && empty > 3) || (empty==0))
                return points;
            for (int i = 0; i < rcd.Length; i++)
                if (this.board[rcd[i] / 6, rcd[i] % 6] == 0)
                {
                    int c = -1;
                    switch(id)
                    {
                        case 0:
                            c += (this.countRowSequence(rcd[i] / 6, rcd[i] % 6, 1))%10;
                            break;
                        case 1:
                            c += (this.countColumnSequence(rcd[i] / 6, rcd[i] % 6, 1))%10;
                            break;
                        case 2:
                            c += (this.countSlopeSequence(rcd[i] / 6, rcd[i] % 6, 1))%10;
                            break;
                        default:
                            c += (this.countClimbSequence(rcd[i] / 6, rcd[i] % 6, 1))%10;
                            break;
                    }
                    if (c == 3)
                        points -= 10000;
                    if (c > 3)
                        points -= 100000;
                }
            return points;
        }

        private int evaluate(int s)
        {
            switch (s) //if the game ended, return the value of the game
            {
                case 1: //AI lost
                    return -1000000;
                case 2: //AI won
                    return 1000000;
                case 3: //tie
                    return 0;
            }
            //Pentago heuristics that help evaluate the current board's state:
            int points = 0;
            // - Marble Locations
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                    if (this.board[i, j] == 2) //AI marble
                        points += this.marbleLocations(i, j); //adds points to the board's state according to each AI marble's location found in a block
            // - Sequences
            int[] sequencePoints = new int[6] { 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                    if (this.board[i, j] != 0) //a marble
                    {
                        int[] sp = this.sequences(i, j); //gets points for the marble's sequences
                        for (int x = 0; x < 6; x++)
                            sequencePoints[x] += sp[x]; //adds up the points
                    }
            //don't count the same sequence multiple times
            //adds points to the board's state according to the amount of sequences, their type, if they are blocked, and their size
            for (int x = 0; x < 6; x++)
                points += sequencePoints[x] / ((x / 2) + 2);
            // - Popular Blocks
            for (int i = 0; i <= 3; i += 3)
                for (int j = 0; j <= 3; j += 3) //each block
                    points -= this.popularBlocks(i, j); //removes points to the board's state according to how much more player marbles are taking a block
            // - Sequence Gaps
            for(int i=0; i<=30; i+=6) //Row Seqeunces
            {
                int[] rcd = new int[6] { i, i + 1, i + 2, i + 3, i + 4, i + 5 };
                points += this.gapInSequence(rcd, 0);
            }
            for (int i=0; i<=5; i++) //Column Seqeunces
            {
                int[] rcd = new int[6] { i, i + 6, i + 12, i + 18, i + 24, i + 30 };
                points += this.gapInSequence(rcd, 1);
            }
            for (int i = 0; i <= 2; i++) //Relevant Slope Seqeunces
            {
                if (i == 2)
                    i = 6;
                int[] rcd;
                if(i==0)
                    rcd = new int[6] { i, i + 7, i + 14, i + 21, i + 28, i + 35 };
                else
                    rcd = new int[5] { i, i + 7, i + 14, i + 21, i + 28 };
                points += this.gapInSequence(rcd, 2);
            }
            for (int i = 4; i <= 6; i++) //Relevant Climb Seqeunces
            {
                if (i == 6)
                    i = 11;
                int[] rcd;
                if (i == 5)
                    rcd = new int[6] { i, i + 5, i + 10, i + 15, i + 20, i + 25 };
                else
                    rcd = new int[5] { i, i + 5, i + 10, i + 15, i + 20 };
                points += this.gapInSequence(rcd, 3);
            }
            return points;
        }

        private int minmax(int d, bool player, Move move, int t)
        {
            int s = this.winnerStatus(move, t);
            if (s > 0 || d == 0)
                return this.evaluate(s); //evalute the board if the minmax algorithm is at it's deepest or the game ended
            Move newMove;
            String[] ab = new String[1296];
            int abc = 0;
            if (!player)
            {
                int max = -1000000; //AI's turn
                for (int i = 0; i < 6; i++)
                    for (int j = 0; j < 6; j++)
                        if (this.board[i, j] == 0) //empty slot
                        {
                            this.board[i, j] = 2; //AI marble
                            for (int direction = 0; direction <= 1; direction++)
                                for (int blockI = 0; blockI <= 3; blockI += 3)
                                    for (int blockJ = 0; blockJ <= 3; blockJ += 3)
                                    {
                                        newMove = new Move(i, j, blockI, blockJ, direction);
                                        this.rotate(blockI, blockJ, direction); //each rotation
                                        ab[abc] = createAB();
                                        if(!alreadyMade(ab,abc))
                                        {
                                            newMove.score = this.minmax(d - 1, !player, newMove, t + 1); //run the minmax algorithm deeper
                                            if (newMove.score > max)
                                                max = newMove.score; //update the highest score
                                            abc++;
                                        }
                                        int otherDirection = (direction == 1) ? 0 : 1;
                                        this.rotate(blockI, blockJ, otherDirection); //rotate it back
                                    }
                            this.board[i, j] = 0; //remove the marble
                            if (max == 1000000)
                                return max;
                        }
                return max; //return the highest score
            }
            else
            {
                int min = 1000000; //player's turn
                for (int i = 0; i < 6; i++)
                    for (int j = 0; j < 6; j++)
                        if (this.board[i, j] == 0) //empty slot
                        {
                            this.board[i, j] = 1; //player marble
                            for (int direction = 0; direction <= 1; direction++)
                                for (int blockI = 0; blockI <= 3; blockI += 3)
                                    for (int blockJ = 0; blockJ <= 3; blockJ += 3)
                                    {
                                        newMove = new Move(i, j, blockI, blockJ, direction);
                                        this.rotate(blockI, blockJ, direction); //each rotation
                                        ab[abc] = createAB();
                                        if(!alreadyMade(ab,abc))
                                        {
                                            newMove.score = this.minmax(d - 1, !player, move, t + 1); //run the minmax algorithm deeper
                                            if (newMove.score < min)
                                                min = newMove.score; //update the lowest score
                                            abc++;
                                        }
                                        int otherDirection = (direction == 1) ? 0 : 1;
                                        this.rotate(blockI, blockJ, otherDirection); //rotate it back
                                    }
                            this.board[i, j] = 0; //remove the marble
                            if (min == -1000000)
                                return min;
                        }
                return min; //return the lowest score
            }
        }

        public Move AIPerformMove(int turn)
        {
            Move bestMove = new Move(-1000000), move;
            String[] ab = new String[1296];
            int abc = 0;
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                    if (this.board[i, j] == 0) //empty slot
                    {
                        this.board[i, j] = 2; //AI marble
                        for (int direction = 0; direction <= 1; direction++)
                            for (int blockI = 0; blockI <= 3; blockI += 3)
                                for (int blockJ = 0; blockJ <= 3; blockJ += 3)
                                {
                                    move = new Move(i, j, blockI, blockJ, direction);
                                    this.rotate(blockI, blockJ, direction); //each rotation
                                    ab[abc] = createAB();
                                    if(!alreadyMade(ab,abc))
                                    {
                                        move.score = this.minmax(depth - 1, true, move, turn + 1); //run the minmax algorithm
                                        if (move.score > bestMove.score)
                                            bestMove = new Move(move);
                                        abc++;
                                    }
                                    int otherDirection = (direction == 1) ? 0 : 1;
                                    this.rotate(blockI, blockJ, otherDirection); //rotate it back
                                }
                        this.board[i, j] = 0; //remove the marble
                        if(bestMove.score == 1000000)
                            return bestMove;
                    }
            return bestMove;
        }

        private String createAB()
        {
            String ab = "";
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                    ab = ab + (board[i, j].ToString());
            return ab;
        }

        private bool alreadyMade(String[]ab, int abc)
        {
            for (int i = 0; i < abc; i++)
                if (ab[i].Equals(ab[abc]))
                    return true;
            return false;
        }
    }
}