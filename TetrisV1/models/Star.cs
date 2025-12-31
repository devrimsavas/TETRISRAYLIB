using System.Drawing;
using R=Raylib_cs.Raylib;
using Raylib_cs;

namespace TetrisV1.models
{
    public class Star
    {
        public int XCord {get;set;}
        public float YCord {get;set;}
        public float Speed {get;set;}
        public Raylib_cs.Color Color {get;set;}=new Raylib_cs.Color();

        public Star(int xCord,int yCord,Raylib_cs.Color color,float speed)
        {
            this.XCord=xCord;
            this.YCord=yCord;
            this.Color=color;
            this.Speed=speed;
        }
    }
}