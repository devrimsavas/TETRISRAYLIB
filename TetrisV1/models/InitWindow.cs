//InitWindow.cs 
//initialize Window Size and Frame Rate 
using R = Raylib_cs.Raylib;
using Raylib_cs;

namespace TetrisV1.models
{
    public class InitWindow
    {
        public int ScreenWidth { get; set; } = 1200;
        public int ScreenHeight { get; set; } = 900;
        public Color backgroundColor { get; set; }
        public int FPS { get; set; } = 144;
        public InitWindow(){}
        public void CreateWindow()
        {
            R.SetTargetFPS(FPS);
            R.InitWindow(ScreenWidth, ScreenHeight, "Tetris 1");
        }
    }

}