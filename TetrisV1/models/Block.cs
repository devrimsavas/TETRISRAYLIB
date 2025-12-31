using R=Raylib_cs.Raylib;
using Raylib_cs;
using System.Numerics;
namespace TetrisV1.models
{
    
    public static class Block
    {
        public static List<int[,]> Shapes= [];     
        public static int[,] LShape =
        {
            {0,0,0,0},
            {1,0,0,0},
            {1,0,0,0},
            {1,1,0,0}            
        };
        public static int[,] OShape=
        {
            {0,0,0,0},
            {0,0,0,0},
            {0,1,1,0},
            {0,1,1,0}            
        };
        public static int[,] IShape=
        {
            {1,0,0,0},
            {1,0,0,0},
            {1,0,0,0},
            {1,0,0,0}            
        };
        public static int[,] SShape=
        {
            {0,0,0,0},
            {0,0,0,0},
            {0,0,1,1},
            {0,1,1,0}            
        };
        public static int[,] ZShape=
        {
            {0,0,0,0},
            {0,0,0,0},
            {0,1,1,0},
            {0,0,1,1}            
        };
        public static int[,] JShape=
        {
            {0,0,0,0},
            {0,0,0,1},
            {0,0,0,1},
            {0,0,1,1}            
        };
        public static int[,] TShape=
        {
            {0,0,0,0},
            {0,0,0,0},
            {1,1,1,0},
            {0,1,0,0}            
        };
        public static int[][,] AllShapes = [
            LShape,OShape,IShape,SShape,ZShape,JShape,TShape
        ];

        public static int[,] Rotate90CW(int[,] m)
        {
            int n=m.GetLength(0); //default 4
            int[,] r=new int[n,n];
            for (int y=0;y<n;y++)
                for (int x=0;x<n;x++)
                    r[y,x]=m[n-1-x,y];
            return r;
        }
        public static int[,] Rotate90CCW(int[,] m)
        {
            int n = m.GetLength(0);
            int[,] r = new int[n, n];

            for (int y = 0; y < n; y++)
                for (int x = 0; x < n; x++)
                    r[n - 1 - x, y] = m[y, x];
            return r;
        }



        public static readonly (int x, int y)[][] WallKicks =
        {
            new[] { (0,0), (-1,0), (-1,1), (0,-2), (-1,-2) }, // 0 -> R
            new[] { (0,0), (1,0), (1,-1), (0,2), (1,2) },     // R -> 2
            new[] { (0,0), (1,0), (1,1), (0,-2), (1,-2) },    // 2 -> L
            new[] { (0,0), (-1,0), (-1,-1), (0,2), (-1,2) }   // L -> 0
        };

        public static bool TryRotate(
            ref int[,] shape,
            ref int posX,   
            ref int posY,
            ref int rotationState,
            bool clockwise,
            Func<int[,], int, int, bool> IsValid
        )
    {
        int[,] rotated = clockwise
            ? Rotate90CW(shape)
            : Rotate90CCW(shape);

        int kickIndex = clockwise
            ? rotationState
            : (rotationState + 3) % 4; // CCW =reverse kick 

        foreach (var (dx, dy) in WallKicks[kickIndex])
            {
                if (IsValid(rotated, posX + dx, posY + dy))
                {
                    shape = rotated;
                    posX += dx;
                    posY += dy;
                    rotationState = clockwise
                        ? (rotationState + 1) % 4
                        : (rotationState + 3) % 4;
                    return true;
                }
            }
            return false;
        }       
    }
}