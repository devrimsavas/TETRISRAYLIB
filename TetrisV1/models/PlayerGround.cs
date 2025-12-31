//PLAYGROUND.cs 
//draws playground and game mechanics 

using Raylib_cs;
using TetrisV1.models;
//10x20 
namespace TetrisV1.models
{
    public class PlayGround
    {
        public int Width {get;set;} //10
        public int Height {get;set;} // 22 20+2 hidden
        public int CellWidth {get;set;}
        public int CellHeight{get;set;}
        private int[,] board;
        //event informs how many lines deleted
        public event Action<int>? OnLinesCleared;
       

        public PlayGround(int width=10,int height=22,int cellWidth=40,int cellHeight=40)
        {
            this.Width=width;
            this.Height=height;
            this.CellWidth=cellHeight;
            this.CellHeight=cellHeight;
            this.board=new int[this.Width,this.Height]; //init empty board
        }
        //access board
        public int GetCell(int x,int y)
        {
            return board[x,y];
        }
        public void SetCell(int x,int y,int value)
        {
            board[x,y]=value;
        }
        //position control 
        public bool IsValidPosition(int [,] shape,int posX,int posY)
        {
            for (int j=0;j<4;j++)
            {
                for (int i=0;i<4;i++)
                {
                    if (shape[j,i]==0) 
                        continue;
                    //this go on
                    int boardX=posX+i;
                    int boardY=posY+j;
                    //wall and floor check
                    if (boardX<0 || boardX>=Width || boardY>=Height)
                        return false;
                    //top unvisible area not checked bock spawn
                    if (boardY<0)
                        continue;
                    
                    //COLLUSION with not empty cell
                    if (board[boardX,boardY] !=0)
                        return false;          
                }
            }
            return true;
        }
        //lock block to board
        public void LockBlock(int[,] shape, int posX,int posY)
        {
            for (int j=0;j<4;j++)
            {
                for (int i=0;i<4;i++)
                {
                    if (shape[j,i]==1)
                    {
                        int bx=posX+i;
                        int by=posY+j;
                        //lock
                        if (by>=0 && by<Height)
                            board[bx,by]=1;
                    }
                }
            }
        }
        //clear lines 
        //also can be used to update score 
        //use event 
        public void ClearLines()
        {
            //cleared lines init
            int clearedLines=0;

            for (int y=Height-1;y>=0;y--)
            {
                bool full=true;
                for (int x=0;x<Width;x++)
                {
                    if (board[x,y]==0)
                    {
                        full=false;
                        break;
                    }
                }
                if (full)
                {
                    //increase cleared Lines
                    clearedLines++;

                    //scroll line to down
                    for (int yy=y; yy>0;yy--)
                        for (int x=0;x<Width;x++)
                            board[x,yy]=board[x,yy-1];
                    //empty top line 
                    for(int x=0;x<Width;x++)
                        board[x,0]=0;
                    y++;
                    
                }
            }
            //throw event 
            if (clearedLines>0)
            {
                OnLinesCleared?.Invoke(clearedLines);
            }
        }
        //game over 
        public bool IsGameOver(int [,] shape, int spawnX,int spawnY)
        {
            return !IsValidPosition(shape,spawnX,spawnY);
        }



        
    }
}