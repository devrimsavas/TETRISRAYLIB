//PlayerInputScreen.cs
//this class create user screen and get user name
//intro screen can be also added animation 


using R = Raylib_cs.Raylib;
using Raylib_cs;

namespace TetrisV1.models
{
    public static class PlayerInputScreen
    {
        public static Player GetPlayer(InitWindow window)
        {
            string nameInput = "";
            bool done = false;
            float xScroll=-400;
            float yScroll=100;
            float scrollSpeed=102f;
            if (StarField.Stars.Count==0)
            {
                StarField.CreateStars(2200,0,window.ScreenWidth,window.ScreenHeight,0);
                
            }
            
            

            while (!done && !R.WindowShouldClose())
            {
                // INPUT
                int key = R.GetCharPressed();
                while (key > 0)
                {
                    if (key >= 32 && key <= 125 && nameInput.Length < 12)
                        nameInput += (char)key;

                    key = R.GetCharPressed();
                }

                if (R.IsKeyPressed(KeyboardKey.Backspace) && nameInput.Length > 0)
                    nameInput = nameInput[..^1];

                if (R.IsKeyPressed(KeyboardKey.Enter) && nameInput.Length > 0)
                    done = true;

                // draw
                //intro screen with user input 
               
                R.BeginDrawing();
                
                R.ClearBackground(Color.Black);
                //frame
                Rectangle frame=new Rectangle(0,0,window.ScreenWidth,window.ScreenHeight);
                R.DrawRectangleLinesEx(frame,9.3f,Color.Gold);


                //main game title
                R.DrawText($"Tetris",240,50,200,Color.Red);
                //footnote creater
                R.DrawText($"2025 Devrim Savas Yilmaz : Created for Fun",25,window.ScreenHeight-100,50,Color.DarkBlue);


                
                //Enter Player
                MessageBoard.DisplayMessage(
                    window.ScreenWidth / 2 - 250,
                    window.ScreenHeight / 2 - 120,
                    500,
                    150,
                    30,
                    "ENTER PLAYER NAME",
                    nameInput + "_",
                    new Color(255, 0, 255, 180),
                    Color.White
                );
                //enter player label end 
                R.DrawText($"Opps again a Tetris.. in 2025",(int)xScroll,510+(int)yScroll,30,ColorPalette.PickRandomColor());
                float dt=R.GetFrameTime();
                xScroll+=scrollSpeed*dt;
                yScroll = (float)Math.Sin(xScroll * 0.15f) * 10f;                 
                if (xScroll>window.ScreenWidth) { xScroll=-300;}
                
                //StarField.DrawStars();
                StarField.UpdateStars(dt,window.ScreenHeight);
                StarField.DrawStars();
                    
                
                
                R.EndDrawing();                
            } //game loop end 

            return new Player(nameInput);

        }//main method end

    } //main class end

} //namespace end 
