using R = Raylib_cs.Raylib;
using Raylib_cs;

namespace TetrisV1.models
{
    public static class MessageBoard
    {       

        public static void DisplayMessage(int x,int y,int width,int height,int fontSize,string title,string messageBody,Color? bgColor=null, Color? fgColor=null)
        {
            Color bgCol=bgColor  ?? new Color(255,0,0,255);
            Color fgCol=fgColor ?? Color.White;      
            //frame
            Rectangle frame=new Rectangle(x,y,width,height);
            R.DrawRectangleLinesEx(frame,3.1f,new Color(255,23,244));
            //background rectangle
            R.DrawRectangle(x,y,width,height,bgCol);
            //title
            int titleFontSize=fontSize+10;
            int titleWidth=R.MeasureText(title,titleFontSize);
            int titleX=x+(width-titleWidth)/2;
            int titleY=y+30;
            R.DrawText(title, titleX, titleY, titleFontSize, fgCol);

            // body
            int bodyWidth = R.MeasureText(messageBody, fontSize);

            int bodyX = x + (width - bodyWidth) / 2;
            int bodyY = titleY + titleFontSize + 20;

            R.DrawText(messageBody, bodyX, bodyY, fontSize, fgCol);
            
            
        }
        
    }
}