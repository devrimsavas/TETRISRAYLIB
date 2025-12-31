using System;

namespace TetrisV1.models
{
    public class ActiveBlock
    {
        public int Index { get; private set; }          // 0–6
        public int X { get; private set; }               // board x
        public int Y { get; private set; }               // board y
        public int RotationState { get; private set; }   // 0-1-2-3
        public int[,] Shape { get; private set; }        // current (rotated) shape

        public ActiveBlock(int index, int x = 4, int y = 0)
        {
            Index = index;
            X = x;
            Y = y;
            RotationState = 0;

            // copy
            Shape = Clone(Block.AllShapes[Index]);
        }

        // ---------- MOVE ----------

        public bool MoveLeft(Func<int[,], int, int, bool> isValid)
        {
            if (isValid(Shape, X - 1, Y))
            {
                X--;
                return true;
            }
            return false;
        }

        public bool MoveRight(Func<int[,], int, int, bool> isValid)
        {
            if (isValid(Shape, X + 1, Y))
            {
                X++;
                return true;
            }
            return false;
        }

        public bool MoveDown(Func<int[,], int, int, bool> isValid)
        {
            if (isValid(Shape, X, Y + 1))
            {
                Y++;
                return true;
            }
            return false;
        }

        // ---------- ROTATE (CW / CCW) ----------

        public bool RotateCW(Func<int[,], int, int, bool> isValid)
        {
            var rotated = Block.Rotate90CW(Shape);

            // normal tetromino
            foreach (var (dx, dy) in Block.WallKicks[RotationState])
            {
                if (isValid(rotated, X + dx, Y + dy))
                {
                    Shape = rotated;
                    X += dx;
                    Y += dy;
                    RotationState = (RotationState + 1) % 4;
                    return true;
                }
            }
            return false;
        }

        public bool RotateCCW(Func<int[,], int, int, bool> isValid)
        {
            var rotated = Block.Rotate90CCW(Shape);
            int kickIndex = (RotationState + 3) % 4; // reverse kick

            foreach (var (dx, dy) in Block.WallKicks[kickIndex])
            {
                if (isValid(rotated, X + dx, Y + dy))
                {
                    Shape = rotated;
                    X += dx;
                    Y += dy;
                    RotationState = (RotationState + 3) % 4;
                    return true;
                }
            }
            return false;
        }

        // ---------- HELPERS ----------

        private static int[,] Clone(int[,] src)
        {
            int n0 = src.GetLength(0);
            int n1 = src.GetLength(1);
            var dst = new int[n0, n1];
            for (int i = 0; i < n0; i++)
                for (int j = 0; j < n1; j++)
                    dst[i, j] = src[i, j];
            return dst;
        }
    }
}
