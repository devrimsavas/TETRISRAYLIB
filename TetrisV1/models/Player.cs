namespace TetrisV1.models
{
    public class Player
    {
        public string Name {get;set;}
        public int Score {get;set;}

        public Player(string name,int score=0)
        {
            this.Name=name;
            this.Score=score;
        }
        //increase score
        public void IncreaseScore(int amount) //line +0100,double +300 , tetris +800
        {
            this.Score+=amount;
        }
        public void ResetScore()
        {
            this.Score=0;
        }
    }
}