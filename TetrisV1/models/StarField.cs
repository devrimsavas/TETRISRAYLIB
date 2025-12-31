using R=Raylib_cs.Raylib;
using Raylib_cs;
using System.Numerics;
using System.Data;

namespace TetrisV1.models
{
    public static class StarField
    {
        public static List<Star> Stars=new List<Star>();
        public static Random random=new Random();

        public static void CreateStars(int amount,int left,int right,int bottom,int top)
        {
            //star coords
            for (int i=0;i<amount;i++)
            {
                //star
                int x=random.Next(left,right);
                int y=random.Next(top,bottom);
                int r=random.Next(0,255);
                int g=random.Next(0,255);
                int b=random.Next(0,255);
                int a=random.Next(10,255);
                float speed=random.Next(1,60);
                Star star=new Star(x,y,new Color(r,g,b,a),speed);       
                Stars.Add(star)        ;
               
            }
        }
        public static void DrawStars()
        {
            Stars.ForEach(s=>
            {                
                R.DrawPixel(s.XCord,(int)s.YCord,s.Color);                
                
            });
        }

        public static void UpdateStars(float delta,int screenHeight)
        {
            foreach(var s in Stars)
            {
                s.YCord+=s.Speed*delta;

                if (s.YCord>screenHeight)
                {
                    s.YCord=0;
                }
            }
            
            
        }

    }
}