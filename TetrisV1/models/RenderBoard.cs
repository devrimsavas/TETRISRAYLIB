//RenderBoard.cs
//render game board
using R=Raylib_cs.Raylib;
using Raylib_cs;
using TetrisV1.models;
using System.Numerics;
namespace TetrisV1.models
{
    public static class RenderBoard
    {
        public static Random rand=new Random();
        public static Vector3 ColorVector=new Vector3();
        public static void DrawBoard(PlayGround board,int xOffset=0,int yOffset=50,Color? color=null)
        {
            Color blockColor=color ?? Color.LightGray;
           
            
            for(int y=2;y<board.Height;y++) //ignore first 2 lines
            {
                for (int x=0;x<board.Width;x++)
                {
                    if (board.GetCell(x,y)!=0)
                    {
                        R.DrawRectangle(
                            xOffset+x*board.CellWidth,
                            yOffset+(y-2)*board.CellHeight,
                            board.CellWidth,
                            board.CellHeight,
                            blockColor
                        );
                        Rectangle r=new Rectangle(
                            xOffset+x*board.CellWidth,
                            yOffset+(y-2)*board.CellHeight,
                            board.CellWidth,
                            board.CellHeight

                        );
                        //colors
                        ColorVector.X+=10;
                        ColorVector.Y+=10;
                        ColorVector.Z+=10;
                        Raylib_cs.Color bColor=new Color(ColorVector.X,ColorVector.Y,ColorVector.Z);
                        //R.DrawRectangleRec(r,blockColor);
                        
                        R.DrawRectangleGradientEx(r,Color.Gold,Color.Gray,Color.Purple,Color.Black);
                        //R.DrawRectangleLinesEx(r,4.4f,bColor);
                        
                    }
                }
            }
        }
        //Game Board Frame 
        public static void DrawFrame(PlayGround board,int xOffset=0,int yOffset=50)
        {
            int visibleHeight = board.Height - 2;

            int left   = xOffset;
            int right  = xOffset + board.Width * board.CellWidth;
            int top    = yOffset-5;
            int bottom = yOffset + visibleHeight * board.CellHeight;

            // left
            R.DrawLine(left, top, left, bottom, Color.White);
            // right
            R.DrawLine(right, top, right, bottom, Color.White);
            // down
            R.DrawLine(left, bottom, right, bottom, Color.White);
            // up optional
            //R.DrawLine(left, top, right, top, Color.White);
            
            
            Rectangle r=new Rectangle(left,top,right-left,bottom-top);
            R.DrawRectangleLinesEx(r,4.0f,Color.Beige);
            
            R.DrawRectangleGradientEx(
                r,
                new Color(2, 2, 255, 0),
                new Color(2, 2, 255, 0),
                new Color(255, 255, 255, 0),
                new Color(255, 255, 255, 0)
            );
            

            
        }
        
    }
}