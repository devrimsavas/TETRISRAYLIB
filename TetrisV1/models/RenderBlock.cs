//RenderBlock.cs
//render block 

using R=Raylib_cs.Raylib;
using Raylib_cs;
namespace TetrisV1.models
{
    public static class RenderBlock
    {
        public static InitWindow window=new InitWindow(); 
        public static void DrawBlock(int[,] matrix,int width,int height,int x, int y,int xOffset=00, int yOffset=50,Color? color=null)
        {
            Color blockColor=color ?? Color.Red;
            
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (matrix[j, i] == 1)
                    {
                        R.DrawRectangleLines(
                            xOffset + (x + i) * width,
                            yOffset + (y + j-2) * height,
                            width,
                            height,
                            blockColor
                        );
                        Rectangle r=new Rectangle(
                            xOffset + (x + i) * width,
                            yOffset + (y + j-2) * height,
                            width,
                            height
                        );
                        R.DrawRectangleRec(r,blockColor);
                        R.DrawRectangleLinesEx(r,4.4f,Color.DarkGray);
                        //R.DrawRectangleGradientEx(r,Color.Red,Color.Blue,Color.Red,Color.Blue);
                        /*
                        R.DrawRectangleGradientV(

                            xOffset + (x + i) * width,
                            yOffset + (y + j-2) * height,
                            width,
                            height,
                            Color.Blue,
                            Color.Red
                        );
                        */
                        

                    }                      
                }
            }            
        }
    }
}