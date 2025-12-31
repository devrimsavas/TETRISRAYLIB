//BackGroundRenderer.cs
// Create BackGround 

using R = Raylib_cs.Raylib;
using Raylib_cs;
using System;
using System.Numerics;

namespace TetrisV1.models
{
    public static class BackgroundRenderer
    {
        static Texture2D bgTexture;
        static bool loaded=false;
        public static void LoadBackGround()
        {
            if (loaded) return;

            Image image = R.LoadImage("resources/images/tetrisbg.png");
            
            bgTexture = R.LoadTextureFromImage(image);
            R.UnloadImage(image);
            

            loaded = true;
        }
        public static void DrawBackGround()
        {
            if (!loaded) return;
            R.DrawTexture(bgTexture,0,0,Color.White);
            
        }
        public static void UnloadBackground()
        {
            if (!loaded) return;

            R.UnloadTexture(bgTexture);
            loaded = false;
        }


        static float time = 0f;

        public static void DrawGradient(int screenWidth, int screenHeight)
        {
            time += R.GetFrameTime() * 0.15f;

            int topR = (int)(10 + 5 * MathF.Sin(time));
            int topG = (int)(20 + 5 * MathF.Sin(time + 1f));
            int topB = (int)(30 + 5 * MathF.Sin(time + 2f));

            int botR = (int)(25 + 10 * MathF.Sin(time + 0.5f));
            int botG = (int)(30 + 10 * MathF.Sin(time + 1.5f));
            int botB = (int)(45 + 10 * MathF.Sin(time + 2.5f));

            Color topColor = new Color(topR, topG, topB, 255);
            Color bottomColor = new Color(botR, botG, botB, 255);

            R.DrawRectangleGradientV(
                0,
                0,
                screenWidth,
                screenHeight,
                topColor,
                bottomColor
            );
        }
    }
}

