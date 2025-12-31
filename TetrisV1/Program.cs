using R=Raylib_cs.Raylib;
using Raylib_cs;
using TetrisV1.models;
using System.Data.Common;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Security.Principal;
namespace TetrisV1
{
    internal class Program
    {
        //window class to create window
        public static InitWindow window=new InitWindow(); 
        public static Random random=new Random();
        
        //main method 
        public static async Task Main(string[] args)
        {
            //--GAME INITIALIZE 
            //-----level changing-----
            int level=1;
            float levelTimer=0f;
            //level changes by time, 
            const float LEVEL_DURATION=80f; //
            //pause game flag
            bool paused=false;
            //load previous players hi-score table
            await PlayersRecord.LoadFromFileAsync();
            window.CreateWindow(); 
            //background picture
            BackgroundRenderer.LoadBackGround();
            Player player=PlayerInputScreen.GetPlayer(window);
            //board test
            PlayGround board=new PlayGround();            //event binding for score 
            
            BindScoreEvent(board,player);

            //start pick a random block
            ActiveBlock active=new(random.Next(0,Block.AllShapes.Length));
            //loop shapes
            int index=Block.AllShapes.Length;
            Color randomColor=ColorPalette.PickRandomColor();
            //timer
            float fallTimer=0f;
            //speed 
            float fallInterval=0.8f;

            //---game-over variable------
            bool gameOver=false;
            float gameOverTimer=0.0f;
            //-------------------------
            //file save flag
            bool scoreSaved=false;
            //DRAW LOOP
            while (!R.WindowShouldClose())
            {
                //pause 
                if (R.IsKeyPressed(KeyboardKey.P))
                {
                    paused=!paused;
                }
                
                //animation time 
                float delta = R.GetFrameTime();
                //LEVEL 
                if (!paused)
                {
                    levelTimer+=delta;
                    if (levelTimer>=LEVEL_DURATION)
                    {
                        ChangeLevel(ref level,ref fallInterval);
                        levelTimer=0f;
                        
                    }
                }
                
                // OFFSETS
                int xOff = (window.ScreenWidth / 2) - 200;
                int yOff = 50;
                //pause 
                if (paused)
                {                    
                    R.BeginDrawing();
                    PauseGame(board,xOff,yOff);
                    R.EndDrawing();
                    continue;                    
                }                

                // ---- GAME OVER MODE ----
                if (gameOver)
                {                    
                    gameOverTimer += delta;

                    if (gameOverTimer >= 5f)
                    {
                        //5 seconds later input for new player
                        player=PlayerInputScreen.GetPlayer(window);
                        board = new PlayGround();
                        active = new ActiveBlock(random.Next(0, Block.AllShapes.Length));
                        randomColor = ColorPalette.PickRandomColor();
                        gameOver = false;
                        gameOverTimer = 0f;
                        fallTimer = 0f;
                        //level speed reset
                        level=1;
                        fallInterval=0.8f;
                        levelTimer=0f;
                    }
                    
                    //SAVE SCORE flag true
                    if (!scoreSaved)
                    {
                        scoreSaved=true;
                        PlayersRecord.AddPlayer(player);
                        _=PlayersRecord.SaveToFileAsync();
                    }
                    // draw only
                    R.BeginDrawing();
                    
                    BackgroundRenderer.DrawGradient(window.ScreenWidth, window.ScreenHeight);
                    RenderBoard.DrawBoard(board, xOff, yOff);
                    RenderBoard.DrawFrame(board, xOff, yOff); 

                    //GAME OVER MESSAGE                     
                    MessageBoard.DisplayMessage(250,240,750,250,40,"GAME OVER","Press Enter For New Game",Color.Red,Color.White);
                    
                    R.EndDrawing();

                    if (R.IsKeyPressed(KeyboardKey.Enter))
                    {
                        //create board and player after gameover
                        board = new PlayGround();
                        active = new ActiveBlock(random.Next(0, Block.AllShapes.Length));
                        player = PlayerInputScreen.GetPlayer(window); //create new player
                        gameOver = false;
                        BindScoreEvent(board,player);
                        //reset level
                        level=1;
                        fallInterval=0.8f;
                        levelTimer=0f;
                        
                    }
                    continue; // gravity must stop 
                } //END OF GAME OVER                 

                // ---- NORMAL MODE ----
                GetKeyStroke(active, board);
                fallTimer += delta;
                if (fallTimer >= fallInterval)
                {
                    if (!active.MoveDown(board.IsValidPosition))
                    {
                        board.LockBlock(active.Shape, active.X, active.Y);
                        board.ClearLines();                        

                        active = new ActiveBlock(random.Next(0, Block.AllShapes.Length));
                        randomColor = ColorPalette.PickRandomColor();

                        if (board.IsGameOver(active.Shape, active.X, active.Y))
                        {
                            gameOver = true;
                            gameOverTimer = 0f;
                        }
                    }

                    fallTimer = 0f;
                }
                
                
                //FRAME GENERATOR                  
                DrawFrame(window,xOff,yOff,active,board,randomColor);
                //Player Table 
                MessageBoard.DisplayMessage(5,5,350,200,20,$"PLAYER {player.Name}",$"SCORE:{player.Score}",Color.Black,Color.DarkBlue);    
                MessageBoard.DisplayMessage(5,100,100,20,20,"--------------------",$"Level:{level}",Color.Black,Color.Blue);  
                //starfield
                StarField.UpdateStars(delta,window.ScreenHeight);
                StarField.DrawStars();
                //----------------------END Frame Actors
                Rectangle frame=new Rectangle(0,0,window.ScreenWidth,window.ScreenHeight);
                R.DrawRectangleLinesEx(frame,5.5f,Color.DarkPurple);
                R.EndDrawing();
                //END FRAME                

            } //DRAW LOOP END 
            //unload background picture
            BackgroundRenderer.UnloadBackground();
            //close drawing window
            R.CloseWindow();         
        } //main method end

        //------------------------------METHODS--------------------------------
        //SAVE 
        public static async Task SavePlayer(Player player,bool saveFlag)
        {
            await PlayersRecord.SaveToFileAsync();
            saveFlag=false;
        }
        //DRAW FRAME
        public static void DrawFrame(InitWindow window,int xOffset,int yOffset, ActiveBlock active,PlayGround board,Color? color=null)
        {
            //Clear to draw Frame 
            R.ClearBackground(Color.Black);            
            BackgroundRenderer.DrawBackGround();
            //pause label 
            Color pauseColor=new Color(0,0,0,5);
            MessageBoard.DisplayMessage(50,window.ScreenHeight-120,200,20,20,"-------","Press P To Pause",pauseColor,Color.White);
            RenderBoard.DrawBoard(board,xOffset,yOffset);
            RenderBoard.DrawFrame(board,xOffset,yOffset);
            RenderBlock.DrawBlock(active.Shape,40,40,active.X,active.Y,xOffset,yOffset,color);
            //hiscore table 
            MessageBoard.DisplayMessage(894,5,300,400,20,"HI-Score",GetHiScoreText(),Color.Black,Color.White);
            Rectangle frame=new Rectangle(0,0,window.ScreenWidth,window.ScreenHeight);
            R.DrawRectangleLinesEx(frame,1.4f,Color.DarkBlue);
            
        }
        //hi-score table
        public static string GetHiScoreText(int topN=5)
        {
            return string.Join(
                "\n",
                PlayersRecord.Players
                .OrderByDescending(p=>p.Score)
                .Take(topN)
                .Select((p, i) => $"{i + 1}. {p.Name}  {p.Score}")
            );
        }
        //------------------
        //GET KEY AND ROTATE
        //------------------
        public static void GetKeyStroke(ActiveBlock act,PlayGround board)
        {
            if (R.IsKeyPressed(KeyboardKey.Up))
                act.RotateCW(board.IsValidPosition);

            if (R.IsKeyPressed(KeyboardKey.Left))
                act.MoveLeft(board.IsValidPosition);

            if (R.IsKeyPressed(KeyboardKey.Right))
                act.MoveRight(board.IsValidPosition);

            if (R.IsKeyPressed(KeyboardKey.Down))
                act.MoveDown(board.IsValidPosition);                    
        }

        //Bind Score for player 
        public static void BindScoreEvent(PlayGround board,Player player)
        {
            board.OnLinesCleared+=(lines)=>
            {
                int score=lines switch
                {
                    1=>100,
                    2=>300,
                    3=>500,
                    4=>800,
                    _=>0
                };
                player.IncreaseScore(score);                
            };
        }
        //pause 
        public static void PauseGame (PlayGround board,int xOff,int yOff)
        {
            BackgroundRenderer.DrawBackGround();
            RenderBoard.DrawBoard(board, xOff, yOff);
            RenderBoard.DrawFrame(board, xOff, yOff);            
            Color overlay = new Color(0, 0, 0, 120); 
            R.DrawRectangle(0,0,window.ScreenWidth,window.ScreenHeight,overlay);
            Rectangle frame=new Rectangle(10,260,window.ScreenWidth-20,window.ScreenHeight-520);
            R.DrawRectangleLinesEx(frame,14.5f,Color.Gold);

            
            MessageBoard.DisplayMessage(10,260,
            window.ScreenWidth - 20,
            window.ScreenHeight - 520,
            60,
            "PAUSE GAME","GAME PAUSED\nPRESS P TO CONTINUE",
            new Color(0, 0, 0, 160),
            Color.White
            );                    
        }

        //LEVEL UPDATE speed 
        public static void ChangeLevel(ref int level,ref float fallInterval)
        {

            level++;
            //speed increase step 0.05
            fallInterval-=0.1f;
            if (fallInterval<0.1f)
                fallInterval=0.1f;
        }
              
    } //main class end 
} //namespace end 