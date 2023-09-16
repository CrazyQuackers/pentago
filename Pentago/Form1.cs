using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pentago
{
    public partial class Form1 : Form
    {

        int[,] board = new int[6, 6]; //0 is empty, 1 is white, 2 is black
        int turn = 0; //even number is white's turn, odd number is black's turn
        int slotI = 0; //save the i of the selected slot
        int slotJ = 0; //save the j of the selected slot
        int depth = 2; //depth of the minmax algorithm
        bool isPlayer = true; //true if it's the player's turn and false if it's the AI's turn
        bool AImode = false; //true if user has chosen PvE mode and false if chosen PvP mode
        Logic logic; //logic class that holds all the logic functions
        Sounds sounds; //sounds class that plays sound effects

        public Form1()
        {
            for (int i=0; i<6; i++)
                for(int j=0; j<6; j++)
                {
                    PictureBox circle = new PictureBox();
                    circle.Name = "circle" + i + j;
                    circle.Size = new Size(100, 100);
                    //calculate location of the circle
                    int x = 38 + (128 * j);
                    int y = 38 + (128 * i);
                    if (i > 2)
                        y += 31;
                    if (j > 2)
                        x += 31;
                    circle.Location = new Point(x, y);
                    circle.SizeMode = PictureBoxSizeMode.StretchImage;
                    circle.Image = Pentago.Properties.Resources.empty;
                    circle.Cursor = Cursors.Hand;
                    circle.BackColor = Color.FromArgb(182, 31, 36);
                    circle.Visible = false;
                    circle.Click += new EventHandler(circle_Click);
                    this.Controls.Add(circle); //makes a circle
                }
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 2; j++)
                {
                    PictureBox arrow = new PictureBox();
                    arrow.Name = "arrow" + i + j;
                    int x = 57, y = 35;
                    if (j == 0)
                        arrow.Size = new Size(x, y);
                    else
                        arrow.Size = new Size(y, x);
                    //calculate location of the arrow
                    x = 251 + (288 * (i % 2));
                    y = 381 + (49 * (i / 2));
                    if (j == 1)
                    {
                        int h = x;
                        x = y;
                        y = h;
                        if (i == 1 || i == 2)
                        {
                            if (x == 381)
                            {
                                x += 49;
                                y -= 288;
                            }
                            else
                            {
                                x -= 49;
                                y += 288;
                            }
                        }
                    }
                    arrow.Location = new Point(x, y);
                    arrow.SizeMode = PictureBoxSizeMode.StretchImage;
                    Image img = Pentago.Properties.Resources.arrow;
                    //flip the image accordingly
                    if (j == 0 && i % 2 == 1)
                        img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    if (j == 1)
                        if (i / 2 == 0)
                            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        else
                            img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    arrow.Image = img;
                    arrow.Cursor = Cursors.Hand;
                    arrow.BackColor = Color.FromArgb(182, 31, 36);
                    arrow.Visible = false;
                    arrow.Click += new EventHandler(arrow_Click);
                    this.Controls.Add(arrow); //makes an arrow
                }
            InitializeComponent();
            setBoard();
            logic = new Logic(board, depth);
            sounds = new Sounds();
            MaximizeBox = false;
            MinimizeBox = false;
        }

        private void setBoard()
        {
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                    board[i, j] = 0; //set all values in board array to zero
        }

        private void circleVisible(bool check)
        {
            foreach(Control c in this.Controls)
                if(c.GetType().Name == "PictureBox")
                {
                    PictureBox circle = (PictureBox)c;
                    if (circle.Name.StartsWith("circle"))
                        circle.Visible = check; //every circle becomes visible/invisible
                }
        }

        private void circleEnabled(bool check)
        {
            foreach (Control c in this.Controls)
                if (c.GetType().Name == "PictureBox")
                {
                    PictureBox circle = (PictureBox)c;
                    if (circle.Name.StartsWith("circle"))
                    {
                        circle.Enabled = check; //every circle becomes enabled/disabled
                        if(!check) //change the cursor of the circles accordingly
                            circle.Cursor = Cursors.Default;
                        else
                            circle.Cursor = Cursors.Hand;
                    }
                }
        }

        private void arrowVisible(bool check)
        {
            foreach(Control c in this.Controls)
                if(c.GetType().Name == "PictureBox")
                {
                    PictureBox arrow = (PictureBox)c;
                    if (arrow.Name.StartsWith("arrow"))
                    {
                        arrow.Visible = check; //every arrow becomes visible/invisible
                        arrow.BringToFront();
                    }
                }
        }

        private PictureBox findCircle(int i, int j)
        {
            //returns the circle in the i,j placement
            foreach(Control c in this.Controls)
                if(c.GetType().Name == "PictureBox")
                {
                    PictureBox circle = (PictureBox)c;
                    if (circle.Name == "circle" + i + j)
                        return circle;
                }
            return null;
        }

        private PictureBox findArrow(int i, int j)
        {
            //returns the arrow in the i,j placement
            foreach(Control c in this.Controls)
                if(c.GetType().Name == "PictureBox")
                {
                    PictureBox arrow = (PictureBox)c;
                    if (arrow.Name == "arrow" + i + j)
                        return arrow;
                }
            return null;
        }

        private void putImagesOnCircles()
        {
            for(int i=0; i<6; i++)
                for(int j=0; j<6; j++)
                    switch(board[i,j])
                    {
                        case 0:
                            findCircle(i, j).Image = Pentago.Properties.Resources.empty;
                            break;
                        case 1:
                            findCircle(i, j).Image = Pentago.Properties.Resources.white;
                            break;
                        case 2:
                            findCircle(i, j).Image = Pentago.Properties.Resources.black;
                            break;
                    }
        }

        private void mode_Click(object sender, EventArgs e)
        {
            //start game
            boardBackground.Image = Pentago.Properties.Resources.board;
            pvp.Visible = false;
            pve.Visible = false;
            watermark.Visible = false;
            circleVisible(true);
            circleEnabled(true);
            PictureBox pb = (PictureBox)sender;
            if (pb.Name == "pvp") //have a visible turn counter if in PvP mode
                turnCounter.Visible = true;
            else //indicate user has chosen PvE mode
                AImode = true;
            sounds.play("click");
        }

        private void circle_Click(object sender, EventArgs e)
        {
            PictureBox circle = (PictureBox)sender;
            //get the circle's placement in the array
            int i = int.Parse(circle.Name[6].ToString());
            int j = int.Parse(circle.Name[7].ToString());
            if(board[i,j]==0)
            {
                sounds.play("marble");
                //change the image of the circle
                if (turn % 2 == 0)
                    circle.Image = Pentago.Properties.Resources.white;
                else
                    circle.Image = Pentago.Properties.Resources.black;
                board[i, j] = turn % 2 + 1; //set array placement to correct number
                slotI = i;
                slotJ = j;
                //arrow functionality
                circleEnabled(false);
                arrowVisible(true);
            }
            else
            {
                sounds.play("error");
                MessageBox.Show("This spot is already taken");
                sounds.play("click");
            }
        }

        private void arrow_Click(object sender, EventArgs e)
        {
            PictureBox arrow = (PictureBox)sender;
            //find rotation starting point
            int n = int.Parse(arrow.Name[5].ToString());
            int blockI = 3 * (n / 2);
            int blockJ = 3 * (n % 2);
            //find rotation direction, 0 = clockwise, 1 = anti-clockwise
            int direction = int.Parse(arrow.Name[6].ToString());
            if (n % 3 == 0)
                direction = (direction == 0) ? 1 : 0;
            //rotate the selected block in the chosen direction
            logic.setBoard(board);
            logic.rotate(blockI, blockJ, direction);
            board = logic.getBoard();
            putImagesOnCircles();
            if (AImode && !isPlayer) //if it's AI's turn
                arrow.Visible = false;
            else
                arrowVisible(false);
            sounds.play("arrow");
            //create the move
            Move move = new Move(slotI, slotJ, blockI, blockJ, direction);
            //check winner in the selected slot
            int winner = logic.winnerStatus(move, turn);
            if (winner != 0)
                sounds.play("winner");
            switch(winner)
            {
                case 0: //not done
                    //change the turn counter
                    if (turn % 2 == 0)
                        turnCounter.BackColor = Color.Black;
                    else
                        turnCounter.BackColor = Color.White;
                    turn++;
                    //circle functionality in PvP mode
                    if(!AImode || !isPlayer)
                        circleEnabled(true);
                    break;
                case 1: //white won (player 1)
                    MessageBox.Show("White Wins!");
                    break;
                case 2: //black won (player 2 / AI)
                    MessageBox.Show("Black Wins!");
                    break;
                case 3: //tie
                    MessageBox.Show("Tie Game!");
                    break;
            }
            if (winner != 0)
            {
                sounds.play("click");
                resetGame();
                return;
            }
            if(!AImode)
                return;
            //change turns
            isPlayer = !isPlayer;
            if (turn == 23)
                logic.setDepth(3);
            if(!isPlayer)
                AIPerformMove();
        }

        private void resetGame()
        {
            //resets all global variables to their default values
            turn = 0;
            slotI = 0;
            slotJ = 0;
            setBoard();
            circleVisible(false);
            boardBackground.Image = Pentago.Properties.Resources.menu;
            pvp.Visible = true;
            pve.Visible = true;
            watermark.Visible = true;
            turnCounter.BackColor = Color.White;
            turnCounter.Visible = false;
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                    findCircle(i, j).Image = Pentago.Properties.Resources.empty;
            isPlayer = true;
            AImode = false;
        }

        private void AIPerformMove()
        {
            Move move = logic.AIPerformMove(turn);
            //play the AI's best possible move
            PictureBox circle = findCircle(move.i, move.j);
            circle.Image = Pentago.Properties.Resources.blue_outline;
            board[move.i, move.j] = 2;
            slotI = move.i;
            slotJ = move.j;
            //find the AI's arrow
            int arrowI = 0, arrowJ = move.direction;
            if (move.blockI == 3)
                arrowI += 2;
            if (move.blockJ == 3)
                arrowI += 1;
            if (arrowI % 3 == 0)
                arrowJ = (arrowJ == 0) ? 1 : 0;
            PictureBox arrow = findArrow(arrowI, arrowJ);
            arrow.Visible = true;
        }
    }
}